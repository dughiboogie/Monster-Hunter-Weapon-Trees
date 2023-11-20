using UnityEngine;
using TMPro;

public class ElementEntryView : StatEntryView
{
    [SerializeField]
    private TMP_Dropdown elementType;

    [SerializeField]
    private TMP_InputField elementValue;

    private int elementIndex;

    public void UpdateElementEntryIndex(int index)
    {
        elementIndex = index;
    }

    public void UpdateElementEntry(Element element, uint value)
    {
        elementType.SetValueWithoutNotify((int)element);
        elementValue.SetTextWithoutNotify(value.ToString());
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

        #endregion
}
