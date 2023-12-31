using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponTreeEntryView : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int WeaponCoordinates { get; set; }
    
    public bool IsSelected { 
        get { return isSelected; }
        set
        {
            isSelected = value;
            ActivateWeaponSelectedBackground(isSelected);
        }
    }

    public bool isWeaponPresent = false;

    [SerializeField]
    private Image weaponSelectedBackground;

    [SerializeField]
    private Image weaponOwnedBackground;

    [SerializeField]
    private WeaponIconView weaponIcon;

    [SerializeField]
    private WeaponEvolutionLine previousEvolutionLine;

    private UniqueID weaponID;

    private bool isSelected;

    private static bool isDragging = false;

    public void InitialisePosition(int weaponTreePosition)
    {
        WeaponCoordinates = new Vector2Int(transform.GetSiblingIndex(), weaponTreePosition);
    }

    public void InitialiseWeapon(Weapon weapon)
    {
        weaponID = weapon.weaponID;
        isWeaponPresent = true;

        weaponIcon.gameObject.SetActive(true);
        
        if(weapon.previousWeaponEvolutionID.value != string.Empty) {
            DrawEvolutionLine(GameModel.GetWeaponByID(weapon.previousWeaponEvolutionID).weaponCoordinates);
        }

        UpdateWeaponRarity(weapon);
        UpdateWeaponElementIcons(weapon);
        ActivateWeaponOwnedBackground(weapon.hasWeapon);

        // Do other VIEW POPULATION (e.g. element, etc)
    }

    public void DeleteWeapon()
    {
        weaponID.value = string.Empty;
        isWeaponPresent = false;
        isSelected = false;

        weaponIcon.gameObject.SetActive(false);
        CancelEvolutionLine();
        ActivateWeaponSelectedBackground(false);
        ActivateWeaponOwnedBackground(false);
    }

    public void UpdateWeaponRarity(Weapon weapon)
    {
        weaponIcon.UpdateRarityColour(weapon.weaponStats.rarity);
    }

    public void UpdateWeaponElementIcons(Weapon weapon)
    {
        int weaponElementCount = weapon.weaponStats.weaponElements.Count < 3 ? weapon.weaponStats.weaponElements.Count : 3;

        for(int i = 0; i < weaponElementCount; i++) {
            weaponIcon.UpdateElementIcon(i, weapon.weaponStats.weaponElements[i].elementType, weapon.weaponStats.weaponElements[i].hiddenElement);
        }
    }

    public void UpdateWeaponOwnershipView(bool owns)
    {
        ActivateWeaponOwnedBackground(owns);
    }

    #region EvolutionLines

    public void DrawEvolutionLine(Vector2Int previousWeaponCoordinates)
    {
        Vector2 endingPosition = new Vector2(
            (previousWeaponCoordinates.x - WeaponCoordinates.x) * 40,
            (WeaponCoordinates.y - previousWeaponCoordinates.y) * 42);

        previousEvolutionLine.Draw(endingPosition);
    }

    public void CancelEvolutionLine()
    {
        previousEvolutionLine.Reset();
    }

    #endregion

    #region Events

    public void OnPointerEnter()
    {
        if(!isSelected) {
            ActivateWeaponSelectedBackground(true);
        }

        if(isDragging) {
            // GameController.instance.DrawConnectionLine(WeaponCoordinates);
        }

        ConsolePrinter.instance.UpdateConsoleXCoordinates(WeaponCoordinates.x);
    }

    public void OnPointerExit()
    {
        if(!isSelected) {
            ActivateWeaponSelectedBackground(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isWeaponPresent) {
            GameController.instance.SelectWeapon(weaponID, WeaponCoordinates);
            return;
        }
        
        if(eventData.clickCount == 2 && !InputElementsLocker.instance.LockActive) {
            if(isWeaponPresent) {
                Debug.LogWarning("This should never happen");
            }
            GameController.instance.AddWeapon(WeaponCoordinates);
        }
    }

    public void OnDragBegin()
    {
        if(!InputElementsLocker.instance.LockActive)
        {
            GameController.instance.UpdateStartWeaponDrag(weaponID, WeaponCoordinates);
            isDragging = true;
        }
    }

    public void OnDragEnd()
    {
        if(!InputElementsLocker.instance.LockActive)
        {
            GameController.instance.UpdateEndWeaponDrag(weaponID, WeaponCoordinates);
            isDragging = false;
        }
    }

    #endregion

    private void ActivateWeaponSelectedBackground(bool activate)
    {
        weaponSelectedBackground.gameObject.SetActive(activate);
    }

    private void ActivateWeaponOwnedBackground(bool activate)
    {
        weaponOwnedBackground.gameObject.SetActive(activate);
    }
}
