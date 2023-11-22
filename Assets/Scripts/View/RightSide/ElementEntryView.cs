using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ElementEntryView : StatEntryView
{
    [SerializeField]
    private Toggle hiddenElementToggle;

    [SerializeField]
    private TMP_Dropdown elementType;

    [SerializeField]
    private TMP_InputField elementValue;

    private int elementIndex;

    public void ResetView()
    {
        UpdateElementEntry(Element.None, 0);
        UpdateElementHiddenText(false);
        hiddenElementToggle.SetIsOnWithoutNotify(false);
    }

    public void UpdateElementEntryIndex(int index)
    {
        elementIndex = index;
    }

    public void UpdateElementEntry(Element element, float value)
    {
        elementType.SetValueWithoutNotify((int)element);
        elementValue.SetTextWithoutNotify(value.ToString());
    }

    public void UpdateElementHiddenText(bool hidden)
    {
        if(hidden) {
            elementType.captionText.color = Color.gray;
            elementValue.textComponent.color = Color.gray;
        }
        else {
            elementType.captionText.color = Color.white;
            elementValue.textComponent.color = Color.white;
        }
        hiddenElementToggle.SetIsOnWithoutNotify(hidden);
    }

    #region Events

    public void OnElementTypeChange(TMP_Dropdown change)
    {
        GameController.instance.UpdateElementType(change.captionText.text, elementIndex);
    }

    public void OnElementValueChange(string value)
    {
        GameController.instance.UpdateElementValue(value, elementIndex);
    }

    public void OnElementHidden(bool hidden)
    {
        GameController.instance.HideElement(hidden, elementIndex);
        UpdateElementHiddenText(hidden);
    }

    #endregion
}
