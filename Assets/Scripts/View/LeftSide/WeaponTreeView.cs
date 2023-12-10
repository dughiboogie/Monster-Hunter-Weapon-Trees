using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponTreeView : MonoBehaviour
{
    public Image rowBackground;

    [SerializeField]
    private Transform gridParentTransform;

    [SerializeField]
    private WeaponTreeEntryView weaponTreeEntryViewPrefab;

    [SerializeField]
    private TMP_InputField weaponTreeName;

    private List<WeaponTreeEntryView> weaponTreeEntryViews = new List<WeaponTreeEntryView>();

    private const int MAX_COLUMN_COUNT = 12;

    private UniqueID weaponTreeID;

    private int weaponTreePosition;

    public void InitialiseWeaponTreeView(WeaponTree weaponTree)
    {
        weaponTreeName.text = weaponTree.weaponTreeName;
        weaponTreeID = weaponTree.weaponTreeID;
        weaponTreePosition = weaponTree.weaponTreePosition;

        SetWeaponTreeRowBackgroundColor();
        SetupWeaponTreeEntryViewsGrid();
        UpdateWeaponEntryViews(weaponTree.weapons);
    }

    private void SetWeaponTreeRowBackgroundColor()
    {
        if(weaponTreePosition % 2 != 0) {
            rowBackground.CrossFadeAlpha(5, .1f, true);
        }
        else {
            rowBackground.CrossFadeAlpha(0, .1f, true);
        }
    }

    private void SetupWeaponTreeEntryViewsGrid()
    {
        for(int i = 0; i < MAX_COLUMN_COUNT; i++) {
            WeaponTreeEntryView weaponTreeEntryView = Instantiate(weaponTreeEntryViewPrefab, gridParentTransform);
            weaponTreeEntryView.InitialisePosition(weaponTreePosition);
            weaponTreeEntryViews.Add(weaponTreeEntryView);
        }
    }

    private void UpdateWeaponEntryViews(List<Weapon> weapons)
    {
        foreach(Weapon weapon in weapons) {
            weaponTreeEntryViews[weapon.weaponCoordinates.x].InitialiseWeapon(weapon);
        }
    }

    #region Events

    public void OnWeaponTreeNameChange(string newName)
    {
        GameController.instance.UpdateWeaponTreeName(weaponTreeID, newName);
    }

    public void OnPointerEnter()
    {
        ConsolePrinter.instance.UpdateConsoleYCoordinates(weaponTreePosition);
    }

    #endregion

    public void AddWeapon(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].InitialiseWeapon(weapon);
    }

    public void SelectWeapon(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].IsSelected = true;
    }

    public void DeselectWeapon(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].IsSelected = false;
    }

    public void UpdateWeaponRarity(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].UpdateWeaponRarity(weapon);
    }

    public void UpdateWeaponOwnership(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].UpdateWeaponOwnershipView(weapon.hasWeapon);
    }

    public void UpdateWeaponElementIcon(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].UpdateWeaponElementIcons(weapon);
    }

    public void DeleteWeapon(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].DeleteWeapon();
    }

    public void CancelEvolutionLine(Weapon weapon)
    {
        weaponTreeEntryViews[weapon.weaponCoordinates.x].CancelEvolutionLine();
    }

    public List<WeaponTreeEntryView> GetWeaponTreeEntryViews()
    {
        return weaponTreeEntryViews;
    }
}
