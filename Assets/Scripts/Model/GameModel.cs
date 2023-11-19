using System;
using System.Collections.Generic;
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

    #region Game

    public static void CreateNewGame(string gameName, float rawDamageMultiplier, float elementalDamageMultiplier)
    {
        currentGame = new Game();
        currentGame.gameName = gameName;
        currentGame.gameRawDamageMultiplier = rawDamageMultiplier;
        currentGame.gameElementalDamageMultiplier = elementalDamageMultiplier;

        currentGame.weaponTrees = new List<WeaponTree>();
    }

    public static void LoadGame(string gameName)
    {
        currentGame = FileDataManager.instance.GetGameData(gameName);
    }

    public static void UpdateGameName(string newGameName)
    {
        currentGame.gameName = newGameName;
    }

    public static void SaveGame()
    {
        FileDataManager.instance.WriteGameDataFile(currentGame);
    }

    #endregion

    #region WeaponTree

    public static WeaponTree AddWeaponTree()
    {
        WeaponTree weaponTree = new WeaponTree(currentGame.weaponTrees.Count);
        currentGame.weaponTrees.Add(weaponTree);
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
    }

    public static void UpdateWeaponTreeName(UniqueID weaponTreeID, string weaponTreeName)
    {
        foreach(WeaponTree weaponTree in currentGame.weaponTrees) {
            if(weaponTreeID == weaponTree.weaponTreeID) {
                weaponTree.weaponTreeName = weaponTreeName;
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
    }

    public static void UpdateWeaponCost(uint weaponCost)
    {
        selectedWeapon.craftingCosts.coinCost = weaponCost;
    }

    public static void DeleteSelectedWeapon()
    {
        RemoveWeaponNextEvolutions();

        currentGame.weaponTrees[selectedWeapon.weaponCoordinates.y].weapons.Remove(selectedWeapon);
        selectedWeapon = null;
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
    }

    #endregion

    #region CraftingMaterials

    public static CraftingMaterial AddCraftingMaterial()
    {
        CraftingMaterial craftingMaterial = new CraftingMaterial();
        selectedWeapon.craftingCosts.materials.Add(craftingMaterial);
        return craftingMaterial;
    }

    public static void UpdateMaterialName(string materialName, int materialIndex)
    {
        selectedWeapon.craftingCosts.materials[materialIndex].materialName = materialName;
    }

    public static void UpdateMaterialAmount(uint materialAmount, int materialIndex)
    {
        selectedWeapon.craftingCosts.materials[materialIndex].materialAmount = materialAmount;
    }

    public static void RemoveMaterial(string materialName)
    {
        foreach(CraftingMaterial material in selectedWeapon.craftingCosts.materials) {
            if(material.materialName == materialName) {
                selectedWeapon.craftingCosts.materials.Remove(material);
                return;
            }
        }
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
            case "Rarity X":
                selectedWeapon.weaponStats.rarity = Rarity.RarityX;
                break;
            default:
                Debug.LogError($"Trying to update rarity to an invalid value: {rarity}");
                break;
        }
    }

    public static void UpdateAttackValue(uint attackValue)
    {
        selectedWeapon.weaponStats.attackValue = attackValue;
    }

    public static void UpdateSharpnessValue(float sharpnessValue)
    {
        selectedWeapon.weaponStats.sharpness = sharpnessValue;
    }

    public static void UpdateSharpnessMaxValue(float sharpnessMaxValue)
    {
        selectedWeapon.weaponStats.sharpnessMax = sharpnessMaxValue;
    }

    public static void UpdateAffinityValue(float affinityValue)
    {
        selectedWeapon.weaponStats.affinity = affinityValue;
    }

    public static void UpdateDefenseValue(uint defenseValue)
    {
        selectedWeapon.weaponStats.defenseValue = defenseValue;
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