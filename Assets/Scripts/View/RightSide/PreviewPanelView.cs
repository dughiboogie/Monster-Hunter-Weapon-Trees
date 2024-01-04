using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PreviewPanelView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField weaponName;

    [SerializeField]
    private RectTransform weaponImageParent;

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
        ActivateUploadImageIcon(true);
        FormatCraftingCost(string.Empty);
        ActivateAddMaterialButton(false);
    }

    public void UpdateView(Weapon weapon)
    {
        weaponName.text = weapon.name;

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

    #region ContextualUtils

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
        weaponImageParent.gameObject.SetActive(!activate);
    }

    private void UpdateWeaponImageTexture(string imagePath)
    {
        Texture2D texture = new Texture2D(256, 256);
        ImageConversion.LoadImage(texture, FileDataManager.instance.GetImageDataFromPath(imagePath));
        weaponImage.texture = texture;

        //// RESIZE IMAGE

        float w = 0, h = 0, padding = 0;
        var imageTransform = weaponImage.GetComponent<RectTransform>();

        // check if there is something to do
        // if(!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
        padding = 1 - padding;
        float ratio = weaponImage.texture.width / (float)weaponImage.texture.height;
        var bounds = new Rect(0, 0, weaponImageParent.rect.width, weaponImageParent.rect.height);
        if(Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90) {
            //Invert the bounds if the image is rotated
            bounds.size = new Vector2(bounds.height, bounds.width);
        }
        //Size by height first
        h = bounds.height * padding;
        w = h * ratio;
        if(w > bounds.width * padding) { //If it doesn't fit, fallback to width;
            w = bounds.width * padding;
            h = w / ratio;
        }
        
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
        imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        // return imageTransform.sizeDelta;
    }

    #endregion

}
