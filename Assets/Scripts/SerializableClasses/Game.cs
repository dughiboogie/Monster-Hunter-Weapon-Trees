using System.Collections.Generic;

[System.Serializable]
public class Game {
    public string gameName;
    public float rawDamageMultiplier;
    public float elementalDamageMultiplier;
    public List<WeaponTree> weaponTrees;

    public Game()
    {
        gameName = string.Empty;
        rawDamageMultiplier = 1.0f;
        elementalDamageMultiplier = 1.0f;
        weaponTrees = new List<WeaponTree>();
    }
}