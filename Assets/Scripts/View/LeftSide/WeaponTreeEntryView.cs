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
            ActivateCellBackground(isSelected);
        }
    }

    public bool isWeaponPresent = false;

    [SerializeField]
    private Image cellBackground;

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
        
        // Do other VIEW POPULATION (e.g. element, rarity, etc)
    }

    public void DeleteWeapon()
    {
        weaponID.value = string.Empty;
        isWeaponPresent = false;
        isSelected = false;

        weaponIcon.gameObject.SetActive(false);
        CancelEvolutionLine();
        ActivateCellBackground(false);
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
            ActivateCellBackground(true);
        }

        if(isDragging) {
            // GameController.instance.DrawConnectionLine(WeaponCoordinates);
        }

        ConsolePrinter.instance.UpdateConsoleXCoordinates(WeaponCoordinates.x);
    }

    public void OnPointerExit()
    {
        if(!isSelected) {
            ActivateCellBackground(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isWeaponPresent) {
            GameController.instance.SelectWeapon(weaponID, WeaponCoordinates);
            return;
        }
        
        if(eventData.clickCount == 2) {
            if(isWeaponPresent) {
                Debug.LogWarning("This should never happen");
            }
            GameController.instance.AddWeapon(WeaponCoordinates);
        }
    }

    public void OnDragBegin()
    {
        GameController.instance.UpdateStartWeaponDrag(weaponID, WeaponCoordinates);
        isDragging = true;
    }

    public void OnDragEnd()
    {
        GameController.instance.UpdateEndWeaponDrag(weaponID, WeaponCoordinates);
        isDragging = false;
    }

    #endregion

    private void ActivateCellBackground(bool activate)
    {
        cellBackground.gameObject.SetActive(activate);
    }
}
