[System.Serializable]
public class Sharpness
{
    public SharpnessColour colour;
    public uint value;

    public Sharpness(SharpnessColour sharpnessColour)
    {
        colour = sharpnessColour;
        value = 0;
    }
}

public enum SharpnessColour {
    Red,
    Orange,
    Yellow,
    Green,
    Blue,
    White,
    Purple
}
