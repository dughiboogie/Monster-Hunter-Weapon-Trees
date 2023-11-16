using UnityEngine;
using TMPro;

/*
 * This class is responsible for taking user's requests and dispatching them to the right owner
 */
public class GameController : MonoBehaviour
{
    public static GameController instance;

    [SerializeField]
    private TMP_InputField windowName;

    [SerializeField]
    private GameWeaponTreesView gameWeaponTreesView;

    [SerializeField]
    private DetailsView detailsView;

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

    public void CreateNewGame(string gameName = "", float rawDamageMultiplier = 1.0f, float elementalDamageMultiplier = 1.0f)
    {
        GameModel.CreateNewGame(gameName, rawDamageMultiplier, elementalDamageMultiplier);
    }

    public void LoadGame(string gameName)
    {
        GameModel.LoadGame(gameName);
        windowName.text = gameName;

        gameWeaponTreesView.RemoveAllWeaponTrees();
        foreach(WeaponTree weaponTree in GameModel.GetCurrentGame().weaponTrees) {
            gameWeaponTreesView.AddWeaponTreeView(weaponTree);
        }
    }

    // TODO Create new class HeaderView and move this method there?
    public void UpdateGameName(string newGameName)
    {
        if(GameModel.GetCurrentGame() == null) {
            CreateNewGame();
        }

        GameModel.UpdateGameName(newGameName);
    }

    public void SaveGame()
    {
        GameModel.SaveGame();
    }

    #endregion

    #region WeaponTree

    public void AddWeaponTree()
    {
        if(GameModel.GetCurrentGame() == null) {
            CreateNewGame();
        }

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
        detailsView.gameObject.SetActive(true);
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
            GameModel.DeleteWeaponEvolution();
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

    public void UpdateMaterialName(string materialName, int materialIndex)
    {
        GameModel.UpdateMaterialName(materialName, materialIndex);
    }

    public void UpdateMaterialAmount(string materialAmount, int materialIndex)
    {
        GameModel.UpdateMaterialAmount(uint.Parse(materialAmount), materialIndex);
    }

    public void RemoveMaterial(string materialName)
    {
        GameModel.RemoveMaterial(materialName);
        detailsView.UpdateView(GameModel.GetSelectedWeapon());
    }

    #endregion

    #region Statistics

    public void UpdateRarity(string rarity)
    {
        GameModel.UpdateRarity(rarity);
        
        // TODO Update view - Change rarity icon BG colour
    }

    public void UpdateAttackValue(string attackValue)
    {
        GameModel.UpdateAttackValue(uint.Parse(attackValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateSharpnessValue(string sharpnessValue) {
        GameModel.UpdateSharpnessValue(float.Parse(sharpnessValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateSharpnessMaxValue(string sharpnessMaxValue)
    {
        GameModel.UpdateSharpnessMaxValue(float.Parse(sharpnessMaxValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateAffinityValue(string affinityValue)
    {
        GameModel.UpdateAffinityValue(float.Parse(affinityValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateDefenseValue(string defenseValue)
    {
        GameModel.UpdateDefenseValue(uint.Parse(defenseValue, System.Globalization.CultureInfo.InvariantCulture));
    }

    public void UpdateShellingType(string shellingType)
    {

    }

    public void UpdateShellingLevel(string shellingLevel)
    {

    }

    public void UpdateElderseal(string eldersealValue)
    {

    }

    #endregion
}
