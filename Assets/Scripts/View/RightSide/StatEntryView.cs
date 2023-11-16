using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatEntryView : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown statName;

    [SerializeField]
    private TMP_Dropdown statEnumValue;

    [SerializeField]
    private TextMeshProUGUI statStringValue;

    public void OnStatNameChange(TMP_Dropdown change)
    {
        Debug.Log($"Dropdown value changed to {change.captionText.text}");
    }

    /*
    private void PopulateStatNameDropdown()
    {
        statName.ClearOptions();
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        for(int i = 0; i < GameModel.weaponStats.Length; i++) {
            TMP_Dropdown.OptionData newOption = new TMP_Dropdown.OptionData();
            newOption.text = GameModel.weaponStats[i].Name;
            options.Add(newOption);
        }

        statName.AddOptions(options);
    }
    */
}
