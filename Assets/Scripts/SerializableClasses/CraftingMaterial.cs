[System.Serializable]
public class CraftingMaterial {
    public UniqueID materialID;
    public string materialName;
    public string materialIconPath;
    public uint materialAmount;

    public CraftingMaterial()
    {
        materialID = new UniqueID();
        materialID.InitialiseUniqueID();
    }
}