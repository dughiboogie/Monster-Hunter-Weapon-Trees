using System.Collections.Generic;

[System.Serializable]
public class WeaponStats {
    public Rarity rarity;
    public uint attackValue;
    public float sharpness;     // Change to ENUM?
    public float sharpnessMax;  // Change to ENUM?
    public float affinity;
    public List<WeaponElement> weaponElements;
    public bool hiddenElement;
    public List<GemSlot> gemSlots;
    public uint defenseValue;
    public ShellingType shellingType;
    public ShellingLevel shellingLevel;
    public ElderSeal elderseal;
    public Skill skill;

    public WeaponStats()
    {
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
    Square,
    Why
}

public enum Skill {
    None,
    TestSkill
}

public enum ElderSeal {
    Low,
    Average,
    High
}