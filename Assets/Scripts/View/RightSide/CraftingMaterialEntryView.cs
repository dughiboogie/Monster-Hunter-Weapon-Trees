using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingMaterialEntryView : MonoBehaviour {

    [SerializeField]
    private TMP_InputField materialName;

    [SerializeField]
    private Image materialIcon;

    [SerializeField]
    private TMP_InputField materialAmount;

    private UniqueID materialID;

    public void Initialise(CraftingMaterial material)
    {
        materialName.text = material.materialName;
        FormatMaterialAmount(material.materialAmount.ToString());
        materialID = material.materialID;
    }

    private void FormatMaterialAmount(string materialAmount)
    {
        this.materialAmount.text = "X " + materialAmount;
    }

    #region Events

    public void OnMaterialNameChange(string newName)
    {
        GameController.instance.UpdateMaterialName(newName, materialID);
    }

    public void OnMaterialAmountChange(string newAmount)
    {
        GameController.instance.UpdateMaterialAmount(newAmount, materialID);
    }

    public void OnMaterialDelete()
    {
        GameController.instance.RemoveMaterial(materialID);
    }

    #endregion
}