using System.Collections.Generic;
using Vector2Int = UnityEngine.Vector2Int;

[System.Serializable]
public class Weapon
{
    public UniqueID weaponID;
    public bool hasWeapon;
    public Vector2Int weaponCoordinates;
    public string name;
    public string imagePath;
    public CraftingCosts craftingCosts;
    public WeaponStats weaponStats;
    public UniqueID previousWeaponEvolutionID;

    public Weapon(Vector2Int weaponCoordinates)
    {
        this.weaponCoordinates = weaponCoordinates;
        
        weaponID = new UniqueID();
        weaponID.InitialiseUniqueID();
        craftingCosts = new CraftingCosts();
        weaponStats = new WeaponStats();
        previousWeaponEvolutionID = new UniqueID();
    }
}
