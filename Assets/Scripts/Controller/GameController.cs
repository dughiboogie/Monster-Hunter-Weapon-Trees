using System;
using UnityEngine;

/*
 * This class is responsible for taking user's requests and dispatching them to the right owner
 */
public class GameController : MonoBehaviour {
    public static GameController instance;

    [SerializeField]
    private HeaderView headerView;

    [SerializeField]
    private GameWeaponTreesView gameWeaponTreesView;

    [SerializeField]
    private DetailsView detailsView;

    [SerializeField]
    private GameObject unsavedChangesInfoPanel;

    private float currentElementalDamageMultiplier = 1.0f;

    #region Singleton
    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of GameController found!");
            return;
        }
        instance = this;
    }
    #endregion

    #region Game

    public void ResetGame()
    {
        GameModel.ResetGame();
        headerView.ResetView();
        gameWeaponTreesView.RemoveWeaponTreeViews();
        detailsView.ResetView();
        ActivateDetailsView(false);

        InputElementsLocker.instance.ResetLock();
    }

    public void CreateNewGame()
    {
        GameModel.CreateNewGame();
    }

    public void LoadGame(string gameName)
    {
        if(GameModel.GetCurrentGame() != null) {
            ResetGame();
        }

        GameModel.LoadGame(gameName);
        headerView.UpdateView(GameModel.GetCurrentGame());

        InstantiateWeaponTreeViews();
    }

    public void RefreshGame()
    {
        LoadGame(GameModel.GetCurrentGame().gameName);
    }

    public void UpdateGameName(string gameName)
    {
        GameModel.UpdateGameName(gameName);
    }

    public void SaveGame()
    {
        try {
            if(!GameModel.SaveGame()) {
                headerView.ShowSaveErrorMessage();
            }
        }
        catch(Exception e) {
            throw e;
        }
    }

    #endregion

    #region DamageMultipliers

    public void ToggleRawDamageMultiplier(bool active)
    {
        GameModel.ActivateRawDamageMultiplier(active);

        if(GameModel.GetSelectedWeapon() != null) {
            detailsView.UpdateView(GameModel.GetSelectedWeapon());
        }
    }

    public void UpdateRawDamageMultiplierValue(string value)
    {
        GameModel.UpdateRawDamageMultiplierValue(float.Parse(value, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void ToggleElementalDamageMultiplier(bool active)
    {
        GameModel.ActivateElementalDamageMultiplier(active);

        if(GameModel.GetSelectedWeapon() != null) {
            detailsView.UpdateView(GameModel.GetSelectedWeapon());
        }
    }

    public void UpdateElementalDamageMultiplierValue(string value)
    {
        GameModel.UpdateElementalDamageMultiplierValue(float.Parse(value, System.Globalization.CultureInfo.InvariantCulture));
    }

    public float GetCurrentElementalDamageMultiplier()
    {
        return currentElementalDamageMultiplier;
    }

    #endregion

    #region WeaponTree

    public void AddWeaponTree()
    {
        gameWeaponTreesView.AddWeaponTreeView(GameModel.AddWeaponTree());
    }

    public void UpdateWeaponTreePosition(UniqueID weaponTreeID, int weaponTreePosition)
    {
        GameModel.UpdateWeaponTreePosition(weaponTreeID, weaponTreePosition);
    }

    public void UpdateWeaponTreeName(UniqueID weaponTreeID, string weaponTreeName)
    {
        GameModel.UpdateWeaponTreeName(weaponTreeID, weaponTreeName);
    }

    private void InstantiateWeaponTreeViews()
    {
        gameWeaponTreesView.RemoveWeaponTreeViews();
        foreach(WeaponTree weaponTree in GameModel.GetCurrentGame().weaponTrees) {
            gameWeaponTreesView.AddWeaponTreeView(weaponTree);
        }
    }

    #endregion

    #region Weapon

    public void AddWeapon(Vector2Int weaponCoordinates)
    {
        Weapon weapon = GameModel.AddWeapon(weaponCoordinates);
        gameWeaponTreesView.AddWeapon(weapon);
        SelectWeapon(weapon.weaponID, weaponCoordinates);
    }

    /*
     * 1. Check for selected weapon and update related view if found
     * 2. Reset preview panel view
     * 3. Update GameModel with new selected weapon
     * 4. Update tree view with new selected weapon
     * 5. Update preview panel view with new selected weapon
     */
    public void SelectWeapon(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        if(GameModel.GetSelectedWeapon() != null) {
            gameWeaponTreesView.DeselectWeapon(GameModel.GetSelectedWeapon());
        }
        GameModel.SelectWeapon(weaponID, weaponCoordinates);
        gameWeaponTreesView.SelectWeapon(GameModel.GetSelectedWeapon());
        ActivateDetailsView(true);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());

        KeyboardInputManager.instance.UpdateConsoleHints(HintContext.WeaponSelected);
    }

    public void UpdateWeaponName(string weaponName)
    {
        GameModel.UpdateWeaponName(weaponName);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateWeaponCost(string weaponCostText)
    {
        GameModel.UpdateWeaponCost(uint.Parse(weaponCostText));
    }

    public void DeleteSelectedWeapon()
    {
        if(GameModel.GetSelectedWeapon() != null) {
            GameModel.DeleteSelectedWeapon();

            InstantiateWeaponTreeViews();
            ActivateDetailsView(false);
        }

        ConsolePrinter.instance.ResetConsoleView();
    }

    public void UpdateHasWeapon(bool active)
    {
        GameModel.UpdateHasWeapon(active);
        gameWeaponTreesView.UpdateSelectedWeaponOwnership(GameModel.GetSelectedWeapon());
    }

    private void ActivateDetailsView(bool active)
    {
        detailsView.gameObject.SetActive(active);
    }

    #endregion

    #region WeaponEvolution

    public void UpdateStartWeaponDrag(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        GameModel.UpdateStartWeaponDrag(weaponID, weaponCoordinates);
    }

    public void UpdateEndWeaponDrag(UniqueID weaponID, Vector2Int weaponCoordinates)
    {
        GameModel.UpdateEndWeaponDrag(weaponID, weaponCoordinates);
    }

    public void OnWeaponEvolutionUpdated(Vector2Int evolvingWeaponCoordinates, Vector2Int evolvedWeaponCoordinates)
    {
        gameWeaponTreesView.DrawEvolutionLine(evolvingWeaponCoordinates, evolvedWeaponCoordinates);
    }

    public void DeleteWeaponEvolution()
    {
        if(GameModel.GetSelectedWeapon() != null) {
            GameModel.DeleteWeaponPreviousEvolution();
            gameWeaponTreesView.CancelEvolutionLine(GameModel.GetSelectedWeapon());
        }
    }

    #endregion

    #region CraftingMaterials

    public void AddCraftingMaterial()
    {
        GameModel.AddCraftingMaterial();
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateMaterialName(string materialName, UniqueID materialID)
    {
        GameModel.UpdateMaterialName(materialName, materialID);
    }

    public void UpdateMaterialAmount(string materialAmount, UniqueID materialID)
    {
        GameModel.UpdateMaterialAmount(uint.Parse(materialAmount), materialID);
    }

    public void RemoveMaterial(UniqueID materialID)
    {
        GameModel.RemoveMaterial(materialID);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    #endregion

    #region Statistics

    public void UpdateRarity(string rarity)
    {
        GameModel.UpdateRarity(rarity);
        gameWeaponTreesView.UpdateSelectedWeaponRarity(GameModel.GetSelectedWeapon());
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateAttackValue(string attackValue)
    {
        if(attackValue == string.Empty) {
            attackValue = "0";
        }
        GameModel.UpdateAttackValue(float.Parse(attackValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateSharpnessValue(SharpnessColour sharpnessColour, string sharpnessValue) 
    {
        if(sharpnessValue == string.Empty) {
            sharpnessValue = "0";
        }
        GameModel.UpdateSharpnessValue(sharpnessColour, uint.Parse(sharpnessValue, System.Globalization.CultureInfo.InvariantCulture));

        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateSharpnessUpdateValue(SharpnessColour sharpnessColour, string sharpnessValue)
    {
        if(sharpnessValue == string.Empty) {
            sharpnessValue = "0";
        }
        GameModel.UpdateSharpnessUpdateValue(sharpnessColour, uint.Parse(sharpnessValue, System.Globalization.CultureInfo.InvariantCulture));
        
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateSharpnessMaxValue(SharpnessColour sharpnessColour, string sharpnessMaxValue)
    {
        if(sharpnessMaxValue == string.Empty) {
            sharpnessMaxValue = "0";
        }
        GameModel.UpdateSharpnessMaxValue(sharpnessColour, uint.Parse(sharpnessMaxValue, System.Globalization.CultureInfo.InvariantCulture));

        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateAffinityValue(string affinityValue)
    {
        if(affinityValue == string.Empty) {
            affinityValue = "0";
        }
        GameModel.UpdateAffinityValue(float.Parse(affinityValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void AddElement()
    {
        GameModel.AddElement();
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateElementType(string elementType, int elementIndex)
    {
        GameModel.UpdateElementType(elementType, elementIndex);
        gameWeaponTreesView.UpdateSelectedWeaponElementIcon(GameModel.GetSelectedWeapon());
    }

    public void UpdateElementValue(string elementValue, int elementIndex)
    {
        if(elementValue == string.Empty) {
            elementValue = "0";
        }
        GameModel.UpdateElementValue(float.Parse(elementValue, System.Globalization.CultureInfo.InvariantCulture), elementIndex);
    }

    public void HideElement(bool hidden, int elementIndex)
    {
        GameModel.HideElement(hidden, elementIndex);
        gameWeaponTreesView.UpdateSelectedWeaponElementIcon(GameModel.GetSelectedWeapon());
    }

    public void UpdateGemSlot(string gemSlotName, int gemSlotIndex)
    {
        GameModel.UpdateGemSlot(gemSlotName, gemSlotIndex);
    }

    public void UpdateDefenseValue(string defenseValue)
    {
        if(defenseValue == string.Empty) {
            defenseValue = "0";
        }
        GameModel.UpdateDefenseValue(uint.Parse(defenseValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateShellingType(string shellingType)
    {
        GameModel.UpdateShellingType(shellingType);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    public void UpdateShellingLevel(string shellingLevel)
    {
        GameModel.UpdateShellingLevel(shellingLevel);
    }

    public void UpdateElderseal(string eldersealValue)
    {
        GameModel.UpdateElderseal(eldersealValue);
    }

    public void UpdateSkillName(string skillName)
    {
        GameModel.UpdateSkillName(skillName);
    }

    #endregion

    #region Images

    public void UpdateSelectedWeaponImage(string imagePath)
    {
        GameModel.UpdateSelectedWeaponImage(imagePath);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    #endregion

    #region ScreenUtils

    public void OnHomeButtonPressed()
    {
        if(GameModel.IsDirty) {
            unsavedChangesInfoPanel.SetActive(true);
        }
        else {
            ResetGame();
            ScreensManager.instance.GoToMainMenu();
        }
    }

    public void GoToHomeNoSave()
    {
        unsavedChangesInfoPanel.SetActive(false);
        ResetGame();
        ScreensManager.instance.GoToMainMenu();
    }

    public void GoToHomeSave()
    {
        unsavedChangesInfoPanel.SetActive(false);

        try {
            SaveGame();
            ResetGame();
            ScreensManager.instance.GoToMainMenu();
        }
        catch(Exception e) {
            throw e;
        }
    }

    public void GoToHomeCancel()
    {
        unsavedChangesInfoPanel.SetActive(false);
    }

    public void LockChanges()
    {
        InputElementsLocker.instance.LockChanges();
    }

    #endregion
}
