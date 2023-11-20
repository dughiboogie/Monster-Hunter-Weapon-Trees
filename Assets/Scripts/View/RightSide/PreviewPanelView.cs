using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class PreviewPanelView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField weaponName;

    [SerializeField]
    private RawImage weaponImage;

    [SerializeField]
    private Image uploadImageIcon;

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
        ActivateUploadImageIcon(true);

        craftingMaterialsView.RemoveAllCraftingMaterials();
        FormatCraftingCost(string.Empty);
        ActivateAddMaterialButton(false);
    }

    public void UpdateView(Weapon weapon)
    {
        weaponName.text = weapon.name;

        // Load weapon image
        if(weapon.imagePath != string.Empty) {
            ActivateUploadImageIcon(false);
            UpdateWeaponImageTexture(weapon.imagePath);
        }

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

    private void ActivateUploadImageIcon(bool activate)
    {
        uploadImageIcon.gameObject.SetActive(activate);
        weaponImage.gameObject.SetActive(!activate);
    }

    private void UpdateWeaponImageTexture(string imagePath)
    {
        Texture2D texture = new Texture2D(256, 256);
        ImageConversion.LoadImage(texture, FileDataManager.instance.GetImageDataFromPath(imagePath));
        weaponImage.texture = texture;
    }

}
