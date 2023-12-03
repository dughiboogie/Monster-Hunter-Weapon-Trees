using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HeaderView : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField gameName;

    [SerializeField]
    private Toggle rawDamageMultToggle;

    [SerializeField]
    private TMP_InputField rawDamageMultValue;

    [SerializeField]
    private Toggle elementDamageMultToggle;

    [SerializeField]
    private TMP_InputField elementDamageMultValue;

    [SerializeField]
    private GameObject errorMessagePanel;

    public void ResetView()
    {
        gameName.text = string.Empty;
        rawDamageMultToggle.SetIsOnWithoutNotify(false);
        rawDamageMultValue.SetTextWithoutNotify("x 1.0");
        elementDamageMultToggle.SetIsOnWithoutNotify(false);
        elementDamageMultValue.SetTextWithoutNotify("x 1.0");
    }

    public void UpdateGameNameView(string gameName)
    {
        this.gameName.text = gameName;
    }

    public void ShowSaveErrorMessage()
    {
        StartCoroutine(errorMessageShower());
    }

    private IEnumerator errorMessageShower()
    {
        errorMessagePanel.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        errorMessagePanel.SetActive(false);
    }

    #region Events

    public void OnGameNameChange(string gameName)
    {
        GameController.instance.UpdateGameName(gameName);
    }

    public void OnRawDamageMultToggled(bool active)
    {
        GameController.instance.ToggleRawDamageMultiplier(active);
    }

    public void OnRawDamageMultChanged(string value)
    {
        if(value.StartsWith("x ")) {
            value = value.Substring(2);
        }

        if(value == string.Empty) {
            value = "1.0";
        }

        GameController.instance.UpdateRawDamageMultiplierValue(value);

        rawDamageMultValue.text = "x " + value;
    }

    public void OnElementalDamageMultToggled(bool active)
    {
        GameController.instance.ToggleElementalDamageMultiplier(active);
    }

    public void OnElementalDamageMultChanged(string value)
    {
        if(value.StartsWith("x ")) {
            value = value.Substring(2);
        }

        if(value == string.Empty) {
            value = "1.0";
        }

        GameController.instance.UpdateElementalDamageMultiplierValue(value);

        elementDamageMultValue.text = "x " + value;
    }

    #endregion

}
