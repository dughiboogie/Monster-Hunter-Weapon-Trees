using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class SharpnessView : MonoBehaviour
{
    [SerializeField]
    private List<TMP_InputField> sharpnessValues;

    public void ResetView()
    {
        foreach(var sharpnessValue in sharpnessValues) {
            sharpnessValue.SetTextWithoutNotify(string.Empty);
        }
    }

    public void UpdateView(List<Sharpness> sharpnesses)
    {
        for(int i = 0; i < sharpnesses.Count; i++) {
            sharpnessValues[i].SetTextWithoutNotify(sharpnesses[i].value.ToString());
        }
    }

    #region Events

    public virtual void OnRedSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Red, sharpnessValue);
    }

    public virtual void OnOrangeSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Orange, sharpnessValue);
    }

    public virtual void OnYellowSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Yellow, sharpnessValue);
    }

    public virtual void OnGreenSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Green, sharpnessValue);
    }

    public virtual void OnBlueSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Blue, sharpnessValue);
    }

    public virtual void OnWhiteSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.White, sharpnessValue);
    }

    public virtual void OnPurpleSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(SharpnessColour.Purple, sharpnessValue);
    }

    #endregion

}
