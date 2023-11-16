using System.Collections.Generic;

[System.Serializable]
public class WeaponTree
{
    public UniqueID weaponTreeID;
    public string weaponTreeName;
    public int weaponTreePosition;
    public List<Weapon> weapons;

    public WeaponTree()
    {
        weaponTreeID = new UniqueID();
        weapons = new List<Weapon>();
    }
}