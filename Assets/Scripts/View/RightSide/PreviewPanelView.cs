using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreviewPanelView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField weaponName;

    [SerializeField]
    private Image weaponImage;

    [SerializeField]
    private CraftingMaterialsListView craftingMaterialsView;

    [SerializeField]
    private TMP_InputField requiredCost;

    [SerializeField]
    private Button AddMaterialButton;

    public void ResetView()
    {
        weaponName.text = string.Empty;
        // Reset weapon image
        craftingMaterialsView.RemoveAllCraftingMaterials();
        FormatCraftingCost(string.Empty);
        ActivateAddMaterialButton(false);
    }

    public void UpdateView(Weapon weapon)
    {
        weaponName.text = weapon.name;

        // Load weapon image

        if(weapon.craftingCosts == null) {
            Debug.LogWarning($"This should not happen! CraftingCosts of weapon {weapon.weaponID} is null.");
        }
        craftingMaterialsView.AddCraftingMaterials(weapon.craftingCosts.materials);
        FormatCraftingCost(weapon.craftingCosts.coinCost.ToString());
        ActivateAddMaterialButton(true);
    }

    #region Events

    public void OnWeaponNameChange(string weaponName)
    {
        GameController.instance.UpdateWeaponName(weaponName);
    }

    public void OnWeaponCostChange(string weaponCostText)
    {
        FormatCraftingCost(weaponCostText);
        GameController.instance.UpdateWeaponCost(weaponCostText);
    }

    public void OnAddCraftingMaterial()
    {
        GameController.instance.AddCraftingMaterial();
    }

    #endregion

    private void FormatCraftingCost(string craftingCost)
    {
        requiredCost.text = craftingCost + " z";
    }

    private void ActivateAddMaterialButton(bool activate)
    {
        AddMaterialButton.interactable = activate;
    }
}
