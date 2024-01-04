public class SharpnessUpdateView : SharpnessView
{
    #region Events

    public override void OnRedSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Red, sharpnessValue);
    }

    public override void OnOrangeSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Orange, sharpnessValue);
    }

    public override void OnYellowSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Yellow, sharpnessValue);
    }

    public override void OnGreenSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Green, sharpnessValue);
    }

    public override void OnBlueSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Blue, sharpnessValue);
    }

    public override void OnWhiteSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.White, sharpnessValue);
    }

    public override void OnPurpleSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.Purple, sharpnessValue);
    }

    public override void OnSkyBlueSharpnessChange(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessUpdateValue(SharpnessColour.SkyBlue, sharpnessValue);
    }

    #endregion

}
