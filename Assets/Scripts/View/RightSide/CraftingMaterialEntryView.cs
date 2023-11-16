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

    public void Initialise(CraftingMaterial material)
    {
        materialName.text = material.materialName;
        FormatMaterialAmount(material.materialAmount.ToString());
    }

    private void FormatMaterialAmount(string materialAmount)
    {
        this.materialAmount.text = "X " + materialAmount;
    }

    #region Events

    public void OnMaterialNameChange(string newName)
    {
        GameController.instance.UpdateMaterialName(newName, transform.GetSiblingIndex());
    }

    public void OnMaterialAmountChange(string newAmount)
    {
        GameController.instance.UpdateMaterialAmount(newAmount, transform.GetSiblingIndex());
    }

    #endregion
}