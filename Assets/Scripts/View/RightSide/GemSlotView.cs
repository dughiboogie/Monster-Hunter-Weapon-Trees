using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemSlotView : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown gemSlotDropdown;

    public void ResetGemSlotView()
    {
        gemSlotDropdown.SetValueWithoutNotify(0);
    }

    public void UpdateGemSlotView(GemSlot gemSlot)
    {
        gemSlotDropdown.SetValueWithoutNotify((int)gemSlot);
    }

    public void OnGemSlotChange(TMP_Dropdown change)
    {
        GameController.instance.UpdateGemSlot(change.captionImage.sprite.name, transform.GetSiblingIndex());
    }

}
