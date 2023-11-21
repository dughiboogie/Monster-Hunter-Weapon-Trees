[System.Serializable]
public class WeaponElement
{
    public Element elementType;
    public uint elementValue;
    public bool hiddenElement;

    public WeaponElement(Element elementType = Element.None, uint elementValue = 0)
    {
        this.elementType = elementType;
        this.elementValue = elementValue;
    }
}

public enum Element {
    None,
    Raw,
    Fire,
    Water,
    Thunder,
    Ice,
    Dragon,
    Poison,
    Sleep,
    Paralysis
}