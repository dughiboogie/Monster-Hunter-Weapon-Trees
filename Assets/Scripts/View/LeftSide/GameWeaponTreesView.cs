using System.Collections.Generic;
using UnityEngine;

public class GameWeaponTreesView : MonoBehaviour
{
    [SerializeField]
    private WeaponTreeView weaponTreeViewPrefab;

    private List<WeaponTreeView> weaponTreeViews = new List<WeaponTreeView>();

    public void AddWeaponTreeView(WeaponTree weaponTree)
    {
        WeaponTreeView weaponTreeView = Instantiate(weaponTreeViewPrefab, transform);
        weaponTreeView.InitialiseWeaponTreeView(weaponTree);

        weaponTreeViews.Add(weaponTreeView);
    }

    public void RemoveWeaponTreeViews()
    {
        foreach(WeaponTreeView weaponTreeView in weaponTreeViews) {
            Destroy(weaponTreeView.gameObject);
        }
        weaponTreeViews.Clear();
    }

    public void AddWeapon(Weapon weapon)
    {
        weaponTreeViews[weapon.weaponCoordinates.y].AddWeapon(weapon);
    }

    public void SelectWeapon(Weapon weapon)
    {
        weaponTreeViews[weapon.weaponCoordinates.y].SelectWeapon(weapon);
    }

    public void DeselectWeapon(Weapon weapon)
    {
        weaponTreeViews[weapon.weaponCoordinates.y].DeselectWeapon(weapon);
    }

    public void DeleteWeapon(Weapon weapon)
    {
        weaponTreeViews[weapon.weaponCoordinates.y].DeleteWeapon(weapon);
    }

    public void DrawEvolutionLine(Vector2Int evolvingWeaponCoordinates, Vector2Int evolvedWeaponCoordinates)
    {
        weaponTreeViews[evolvedWeaponCoordinates.y].GetWeaponTreeEntryViews()[evolvedWeaponCoordinates.x].DrawEvolutionLine(evolvingWeaponCoordinates);
    }

    public void CancelEvolutionLine(Weapon weapon)
    {
        weaponTreeViews[weapon.weaponCoordinates.y].CancelEvolutionLine(weapon);
    }

    /*
  
    public void RemoveWeaponTree(WeaponTreeView weaponTree)
    {
        Destroy(weaponTree.gameObject);
        weaponTreeViews.Remove(weaponTree);
    }
    */

}
