public class SharpnessMaxView : SharpnessView
{
    #region Events

    public override void OnRedSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Red, sharpnessValue);
    }

    public override void OnOrangeSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Orange, sharpnessValue);
    }

    public override void OnYellowSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Yellow, sharpnessValue);
    }

    public override void OnGreenSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Green, sharpnessValue);
    }

    public override void OnBlueSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Blue, sharpnessValue);
    }

    public override void OnWhiteSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.White, sharpnessValue);
    }

    public override void OnPurpleSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(SharpnessColour.Purple, sharpnessValue);
    }

    #endregion

}
