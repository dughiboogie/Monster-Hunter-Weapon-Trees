using System.Collections.Generic;

[System.Serializable]
public class CraftingCosts {
    public List<CraftingMaterial> materials = new List<CraftingMaterial>();
    public uint coinCost;
}