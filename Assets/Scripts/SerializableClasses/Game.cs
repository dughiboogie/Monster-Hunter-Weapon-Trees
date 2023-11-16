using System.Collections.Generic;

[System.Serializable]
public class Game {
    public string gameName;
    public float gameRawDamageMultiplier;
    public float gameElementalDamageMultiplier;
    public List<WeaponTree> weaponTrees;
}