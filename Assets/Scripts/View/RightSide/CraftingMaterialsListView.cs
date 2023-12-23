using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CraftingMaterialsListView : MonoBehaviour
{
    [SerializeField]
    private CraftingMaterialEntryView craftingMaterialPrefab;

    [SerializeField]
    private Transform listParent;

    private List<CraftingMaterialEntryView> craftingMaterialEntryViews = new List<CraftingMaterialEntryView>();

    public void AddCraftingMaterials(List<CraftingMaterial> craftingMaterials)
    {
        RemoveAllCraftingMaterials();

        foreach(CraftingMaterial craftingMaterial in craftingMaterials) {
            CraftingMaterialEntryView newCraftingMaterial = Instantiate(craftingMaterialPrefab, listParent);
            newCraftingMaterial.Initialise(craftingMaterial);
            craftingMaterialEntryViews.Add(newCraftingMaterial);
        }
    }

    public void RemoveAllCraftingMaterials()
    {
        foreach(CraftingMaterialEntryView craftingMaterialEntryView in craftingMaterialEntryViews) {
            DestroyImmediate(craftingMaterialEntryView.gameObject);
        }

        craftingMaterialEntryViews.Clear();
    }
}
