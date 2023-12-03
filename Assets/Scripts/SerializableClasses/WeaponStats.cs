using System.Collections.Generic;

[System.Serializable]
public class WeaponStats {
    public Rarity rarity;
    public float attackValue;
    public List<Sharpness> sharpnesses;
    public List<Sharpness> sharpnessesUpdate;
    public List<Sharpness> sharpnessesMax;
    public float affinity;
    public List<WeaponElement> weaponElements;
    public List<GemSlot> gemSlots;
    public uint defenseValue;
    public ShellingType shellingType;
    public ShellingLevel shellingLevel;
    public ElderSeal elderseal;
    public string skill;

    public WeaponStats()
    {
        sharpnesses = new List<Sharpness>() {
            new Sharpness(SharpnessColour.Red),
            new Sharpness(SharpnessColour.Orange),
            new Sharpness(SharpnessColour.Yellow),
            new Sharpness(SharpnessColour.Green),
            new Sharpness(SharpnessColour.Blue),
            new Sharpness(SharpnessColour.White),
            new Sharpness(SharpnessColour.Purple)
        };

        sharpnessesUpdate = new List<Sharpness>() {
            new Sharpness(SharpnessColour.Red),
            new Sharpness(SharpnessColour.Orange),
            new Sharpness(SharpnessColour.Yellow),
            new Sharpness(SharpnessColour.Green),
            new Sharpness(SharpnessColour.Blue),
            new Sharpness(SharpnessColour.White),
            new Sharpness(SharpnessColour.Purple)
        };

        sharpnessesMax = new List<Sharpness>() {
            new Sharpness(SharpnessColour.Red),
            new Sharpness(SharpnessColour.Orange),
            new Sharpness(SharpnessColour.Yellow),
            new Sharpness(SharpnessColour.Green),
            new Sharpness(SharpnessColour.Blue),
            new Sharpness(SharpnessColour.White),
            new Sharpness(SharpnessColour.Purple)
        };

        weaponElements = new List<WeaponElement>();
        weaponElements.Add(new WeaponElement());
        gemSlots = new List<GemSlot>() { GemSlot.None, GemSlot.None, GemSlot.None };
    }
}

public enum Rarity {
    Rarity1,
    Rarity2,
    Rarity3,
    Rarity4,
    Rarity5,
    Rarity6,
    Rarity7,
    Rarity8,
    Rarity9,
    Rarity10,
    Rarity11,
    Rarity12,
    RarityX
}

public enum ShellingLevel {
    Lv1,
    Lv2,
    Lv3,
    Lv4,
    Lv5,
    Lv6,
    Lv7
}

public enum ShellingType {
    Normal,
    Wide,
    Long
}

public enum GemSlot {
    None,
    Simple,
    Small,
    Medium,
    Big,
    MAX,
    GRank,
    Exotic
}

public enum ElderSeal {
    None,
    Low,
    Average,
    High
}