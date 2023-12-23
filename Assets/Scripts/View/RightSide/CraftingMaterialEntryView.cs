using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingMaterialEntryView : MonoBehaviour {

    [SerializeField]
    private TMP_InputField materialName;

    [SerializeField]
    private TMP_InputField materialAmount;

    [SerializeField]
    private Button removeMaterialButton;

    [SerializeField]
    private Image rowBackground;

    private UniqueID materialID;

    public void Initialise(CraftingMaterial material)
    {
        materialName.text = material.materialName;
        FormatMaterialAmount(material.materialAmount.ToString());
        materialID = material.materialID;
        SetMaterialRowBackgroundColor();

        InputElementsLocker.instance.AddLockable(materialID, materialName);
        InputElementsLocker.instance.AddLockable(materialID, materialAmount);
        InputElementsLocker.instance.AddLockable(materialID, removeMaterialButton);
    }

    private void FormatMaterialAmount(string materialAmount)
    {
        this.materialAmount.text = "X " + materialAmount;
    }

    private void SetMaterialRowBackgroundColor()
    {
        if(transform.GetSiblingIndex() % 2 != 0) {
            rowBackground.CrossFadeAlpha(5, .1f, true);
        }
        else {
            rowBackground.CrossFadeAlpha(0, .1f, true);
        }
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

    private void OnDestroy()
    {
        InputElementsLocker.instance.RemoveLockable(materialID);
    }

    #endregion
}