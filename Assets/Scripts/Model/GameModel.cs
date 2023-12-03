using System;
using UnityEngine;

/*
 * This class is responsible for keeping track of data changes in the current game
 */
public static class GameModel
{
    private static Game currentGame;
    private static Weapon selectedWeapon;

    private static Weapon dragStartWeapon;
    private static Weapon dragEndWeapon;

    public static bool IsDirty { get; set; }

    #region Game

    public static void ResetGame()
    {
        currentGame = null;
    }

    public static void CreateNewGame()
    {
        currentGame = new Game();
    }

    public static void LoadGame(string gameName)
    {
        if(IsDirty) {
            FileDataManager.instance.GetGamesDataFromFolder();
            IsDirty = false;
        }
        currentGame = FileDataManager.instance.GetGameData(gameName);
    }

    public static void UpdateGameName(string gameName)
    {
        currentGame.gameName = gameName;
        IsDirty = true;
    }

    public static bool SaveGame()
    {
        if(currentGame.gameName == string.Empty) {
            return false;
        }
        else {
            try {
                FileDataManager.instance.WriteGameDataFile(currentGame);
                IsDirty = false;
                return true;
            }
            catch(Exception e) {
                Debug.LogError($"Couldn't save current game file: {e.Message}");
                throw;
            }
        }
    }

    #endregion

    #region DamageMultipiers

    public static void UpdateRawDamageMultiplierValue(float value)
    {
        currentGame.rawDamageMultiplier = value;
        IsDirty = true;
    }

    public static void UpdateElementalDamageMultiplierValue(float value)
    {
        currentGame.elementalDamageMultiplier = value;
        IsDirty = true;
    }

    #endregion

    #region WeaponTree

    public static WeaponTree AddWeaponTree()
    {
        WeaponTree weaponTree = new WeaponTree(currentGame.weaponTrees.Count);
        currentGame.weaponTrees.Add(weaponTree);
        IsDirty = true;
        return weaponTree;
    }

    public static void UpdateWeaponTreePosition(UniqueID weaponTreeID, int weaponTreePosition)
    {
        foreach(WeaponTree weaponTree in currentGame.weaponTrees) {
            if(weaponTreeID == weaponTree.weaponTreeID) {
                weaponTree.weaponTreePosition = weaponTreePosition;
                return;
            }
        }
        IsDirty = true;
    }

    public static void UpdateWeaponTreeName(UniqueID weaponTreeID, string weaponTreeName)
    {
        foreach(WeaponTree weaponTree in currentGame.weaponTrees) {
            if(weaponTreeID == weaponTree.weaponTreeID) {
                weaponTree.weaponTreeName = weaponTreeName;
                IsDirty = true;
                return;
            }
        }
    }

    #endregion

    #region Weapon

    public static Weapon AddWeapon(Vector2Int weaponCoordinates)
    {
        Weapon weapon = new Weapon(weaponCoordinates);
        currentGame.weaponTrees[weaponCoordinates.y].weapons.Add(weapon);
        IsDirty = true;
        return weapon;
    }

    public static Weapon SelectWeapon(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        Weapon weapon = currentGame.weaponTrees[weaponCoordinates.y].weapons.Find(x => weaponID == x.weaponID);

        if(weapon != null) {
            selectedWeapon = weapon;
            return selectedWeapon;
        }
        throw new DataMisalignedException($"Selected weapon at coordinates {weaponCoordinates} with ID {weaponID} was not found in Model.");
    }

    public static void UpdateWeaponName(string weaponName)
    {
        selectedWeapon.name = weaponName;
        IsDirty = true;
    }

    public static void UpdateWeaponCost(uint weaponCost)
    {
        selectedWeapon.craftingCosts.coinCost = weaponCost;
        IsDirty = true;
    }

    public static void DeleteSelectedWeapon()
    {
        RemoveWeaponNextEvolutions();

        currentGame.weaponTrees[selectedWeapon.weaponCoordinates.y].weapons.Remove(selectedWeapon);
        selectedWeapon = null;
        IsDirty = true;
    }

    public static void UpdateHasWeapon(bool active)
    {
        selectedWeapon.hasWeapon = active;
        IsDirty = true;
    }

    #endregion

    #region WeaponEvolution

    public static void ResetDragWeapons()
    {
        dragStartWeapon = null;
        dragEndWeapon = null;
    }

    public static void UpdateStartWeaponDrag(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        Weapon weapon = currentGame.weaponTrees[weaponCoordinates.y].weapons.Find(x => weaponID == x.weaponID);

        if(weapon != null) {
            dragStartWeapon = weapon;
        }
        else {
            Debug.LogWarning($"Drag begin weapon at coordinates {weaponCoordinates} with ID {weaponID} was not found in Model.");
        }
    }

    public static void UpdateEndWeaponDrag(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        if(dragStartWeapon == null) {
            return;
        }

        Weapon weapon = currentGame.weaponTrees[weaponCoordinates.y].weapons.Find(x => weaponID == x.weaponID);

        if(weapon != null) {
            dragEndWeapon = weapon;
            UpdateWeaponPreviousEvolution();
        }
        else {
            Debug.LogWarning($"Drag end weapon at coordinates {weaponCoordinates} with ID {weaponID} was not found in Model.");
        }

        ResetDragWeapons();
    }

    private static void UpdateWeaponPreviousEvolution()
    {
        // Means that starting weapon is the previous evolution
        if(dragStartWeapon.weaponCoordinates.x < dragEndWeapon.weaponCoordinates.x) {
            dragEndWeapon.previousWeaponEvolutionID.value = dragStartWeapon.weaponID.value;
            GameController.instance.OnWeaponEvolutionUpdated(dragStartWeapon.weaponCoordinates, dragEndWeapon.weaponCoordinates);
        }
        // Means that ending weapon is the previous evolution
        else {
            dragStartWeapon.previousWeaponEvolutionID.value = dragEndWeapon.weaponID.value;
            GameController.instance.OnWeaponEvolutionUpdated(dragEndWeapon.weaponCoordinates, dragStartWeapon.weaponCoordinates);
        }
        IsDirty = true;
    }

    private static void RemoveWeaponNextEvolutions()
    {
        foreach(WeaponTree weaponTree in currentGame.weaponTrees) {
            foreach(Weapon weapon in weaponTree.weapons) {
                if(weapon.previousWeaponEvolutionID.value != string.Empty &&
                    weapon.previousWeaponEvolutionID.value == selectedWeapon.weaponID.value) {
                    weapon.previousWeaponEvolutionID.value = string.Empty;
                }
            }
        }
    }

    public static void DeleteWeaponPreviousEvolution()
    {
        selectedWeapon.previousWeaponEvolutionID.value = string.Empty;
        IsDirty = true;
    }

    #endregion

    #region CraftingMaterials

    public static CraftingMaterial AddCraftingMaterial()
    {
        CraftingMaterial craftingMaterial = new CraftingMaterial();
        selectedWeapon.craftingCosts.materials.Add(craftingMaterial);
        IsDirty = true;
        return craftingMaterial;
    }

    public static void UpdateMaterialName(string materialName, UniqueID materialID)
    {
        selectedWeapon.craftingCosts.materials.Find(x => x.materialID.value == materialID.value).materialName = materialName;
        IsDirty = true;
    }

    public static void UpdateMaterialAmount(uint materialAmount, UniqueID materialID)
    {
        selectedWeapon.craftingCosts.materials.Find(x => x.materialID.value == materialID.value).materialAmount = materialAmount;
        IsDirty = true;
    }

    public static void RemoveMaterial(UniqueID materialID)
    {
        foreach(CraftingMaterial material in selectedWeapon.craftingCosts.materials) {
            if(material.materialID.value == materialID.value) {
                selectedWeapon.craftingCosts.materials.Remove(material);
                return;
            }
        }
        IsDirty = true;
    }

    #endregion

    #region Statistics

    public static void UpdateRarity(string rarity)
    {
        switch(rarity) {
            case "Rarity 1":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity1;
                break;
            case "Rarity 2":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity2;
                break;
            case "Rarity 3":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity3;
                break;
            case "Rarity 4":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity4;
                break;
            case "Rarity 5":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity5;
                break;
            case "Rarity 6":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity6;
                break;
            case "Rarity 7":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity7;
                break;
            case "Rarity 8":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity8;
                break;
            case "Rarity 9":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity9;
                break;
            case "Rarity 10":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity10;
                break;
            case "Rarity 11":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity11;
                break;
            case "Rarity 12":
                selectedWeapon.weaponStats.rarity = Rarity.Rarity12;
                break;
            case "Rarity X":
                selectedWeapon.weaponStats.rarity = Rarity.RarityX;
                break;
            default:
                Debug.LogError($"Trying to update rarity to an invalid value: {rarity}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateAttackValue(uint attackValue)
    {
        selectedWeapon.weaponStats.attackValue = attackValue;
        IsDirty = true;
    }

    public static void UpdateSharpnessValue(SharpnessColour sharpnessColour, uint sharpnessValue)
    {
        selectedWeapon.weaponStats.sharpnesses.Find(x => x.colour == sharpnessColour).value = sharpnessValue;
        IsDirty = true;
    }

    public static void UpdateSharpnessUpdateValue(SharpnessColour sharpnessColour, uint sharpnessValue)
    {
        selectedWeapon.weaponStats.sharpnessesUpdate.Find(x => x.colour == sharpnessColour).value = sharpnessValue;
        IsDirty = true;
    }

    public static void UpdateSharpnessMaxValue(SharpnessColour sharpnessColour, uint sharpnessMaxValue)
    {
        selectedWeapon.weaponStats.sharpnessesMax.Find(x => x.colour == sharpnessColour).value = sharpnessMaxValue;
        IsDirty = true;
    }

    public static void UpdateAffinityValue(float affinityValue)
    {
        selectedWeapon.weaponStats.affinity = affinityValue;
        IsDirty = true;
    }

    public static void AddElement()
    {
        int elementsEnumLenght = Enum.GetValues(typeof(Element)).Length;

        for(int i = 0; i < elementsEnumLenght; i++) {
            bool elementAlreadyPresent = false;

            for(int j = 0; j < selectedWeapon.weaponStats.weaponElements.Count && !elementAlreadyPresent; j++) {
                if((Element)i == selectedWeapon.weaponStats.weaponElements[j].elementType) {
                    elementAlreadyPresent = true;
                }
            }

            if(!elementAlreadyPresent) {
                WeaponElement weaponElement = new WeaponElement((Element)i);
                selectedWeapon.weaponStats.weaponElements.Add(weaponElement);
                IsDirty = true;
                return;
            }
        }

        Debug.LogWarning("No more elements available to add!");
    }

    public static void UpdateElementType(string elementType, int elementIndex)
    {
        if(selectedWeapon.weaponStats.weaponElements.Count < elementIndex) {
            Debug.LogWarning($"Trying to update element type at index {elementIndex} but selected weapon has too little elements!");
            return;
        }

        switch(elementType) {
            case "None":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.None;
                break;
            case "Raw":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Raw;
                break;
            case "Fire":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Fire;
                break;
            case "Water":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Water;
                break;
            case "Thunder":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Thunder;
                break;
            case "Ice":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Ice;
                break;
            case "Dragon":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Dragon;
                break;
            case "Poison":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Poison;
                break;
            case "Sleep":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Sleep;
                break;
            case "Paralysis":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Paralysis;
                break;
            case "Blast":
                selectedWeapon.weaponStats.weaponElements[elementIndex].elementType = Element.Blast;
                break;
            default:
                Debug.LogError($"Trying to update element at index {elementIndex} to an invalid value: {elementType}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateElementValue(uint elementValue, int elementIndex)
    {
        if(selectedWeapon.weaponStats.weaponElements.Count < elementIndex) {
            Debug.LogWarning($"Trying to update element value at index {elementIndex} but selected weapon has too little elements!");
            return;
        }

        selectedWeapon.weaponStats.weaponElements[elementIndex].elementValue = elementValue;
        IsDirty = true;
    }

    public static void HideElement(bool hidden, int elementIndex)
    {
        selectedWeapon.weaponStats.weaponElements[elementIndex].hiddenElement = hidden;
        IsDirty = true;
    }

    public static void UpdateGemSlot(string gemSlotName, int gemSlotIndex)
    {
        switch(gemSlotName) {
            case "gem_level_0":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.None;
                break;
            case "gem_level_1":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.Simple;
                break;
            case "gem_level_2":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.Small;
                break;
            case "gem_level_3":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.Medium;
                break;
            case "gem_level_4":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.Big;
                break;
            case "gem_level_5":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.MAX;
                break;
            case "gem_level_6":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.GRank;
                break;
            case "gem_level_7":
                selectedWeapon.weaponStats.gemSlots[gemSlotIndex] = GemSlot.Exotic;
                break;
            default:
                Debug.LogError($"Trying to update gem slot at index {gemSlotIndex} to an invalid value: {gemSlotName}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateDefenseValue(uint defenseValue)
    {
        selectedWeapon.weaponStats.defenseValue = defenseValue;
        IsDirty = true;
    }

    public static void UpdateShellingType(string shellingType)
    {
        switch(shellingType) {
            case "Normal":
                selectedWeapon.weaponStats.shellingType = ShellingType.Normal;
                break;
            case "Wide":
                selectedWeapon.weaponStats.shellingType = ShellingType.Wide;
                break;
            case "Long":
                selectedWeapon.weaponStats.shellingType = ShellingType.Long;
                break;
            default:
                Debug.LogError($"Trying to update shelling type to an invalid value: {shellingType}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateShellingLevel(string shellingLevel)
    {
        switch(shellingLevel) {
            case "Lv1":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv1;
                break;
            case "Lv2":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv2;
                break;
            case "Lv3":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv3;
                break;
            case "Lv4":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv4;
                break;
            case "Lv5":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv5;
                break;
            case "Lv6":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv6;
                break;
            case "Lv7":
                selectedWeapon.weaponStats.shellingLevel = ShellingLevel.Lv7;
                break;
            default:
                Debug.LogError($"Trying to update shelling level to an invalid value: {shellingLevel}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateElderseal(string eldersealValue)
    {
        switch(eldersealValue) {
            case "None":
                selectedWeapon.weaponStats.elderseal = ElderSeal.None;
                break;
            case "Low":
                selectedWeapon.weaponStats.elderseal = ElderSeal.Low;
                break;
            case "Average":
                selectedWeapon.weaponStats.elderseal = ElderSeal.Average;
                break;
            case "High":
                selectedWeapon.weaponStats.elderseal = ElderSeal.High;
                break;
            default:
                Debug.LogError($"Trying to update elderseal to an invalid value: {eldersealValue}");
                break;
        }
        IsDirty = true;
    }

    public static void UpdateSkillName(string skillName)
    {
        selectedWeapon.weaponStats.skill = skillName;
        IsDirty = true;
    }

    #endregion

    #region Images

    public static void UpdateSelectedWeaponImage(string imagePath)
    {
        selectedWeapon.imagePath = imagePath;
        IsDirty = true;
    }

    #endregion

    #region Getters

    public static Game GetCurrentGame()
    {
        return currentGame;
    }

    public static Weapon GetSelectedWeapon()
    {
        return selectedWeapon;
    }

    public static Weapon GetWeaponByID(UniqueID weaponID)
    {
        foreach(var weaponTree in currentGame.weaponTrees) {
            foreach(var weapon in weaponTree.weapons) {
                if(weapon.weaponID.value == weaponID.value) {
                    return weapon;
                }
            }
        }

        Debug.LogWarning($"Weapon with ID {weaponID.value} was not found!");
        return null;
    }

    public static Weapon GetDragStartWeapon()
    {
        return dragStartWeapon;
    }

    public static Weapon GetDragEndWeapon()
    {
        return dragEndWeapon;
    }

    #endregion
}
