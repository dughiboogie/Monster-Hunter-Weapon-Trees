using System.Collections.Generic;

[System.Serializable]
public class WeaponTree
{
    public UniqueID weaponTreeID;
    public string weaponTreeName;
    public int weaponTreePosition;
    public List<Weapon> weapons;

    public WeaponTree(int weaponTreePosition)
    {
        weaponTreeID = new UniqueID();
        weaponTreeID.InitialiseUniqueID();
        weapons = new List<Weapon>();

        this.weaponTreePosition = weaponTreePosition;
    }
}