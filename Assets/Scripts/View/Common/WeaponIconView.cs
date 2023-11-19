using UnityEngine;
using UnityEngine.UI;

public class WeaponIconView : MonoBehaviour
{
    [SerializeField]
    private Image weaponIcon;

    public void UpdateRarityColour(Rarity rarity)
    {
        switch(rarity) {
            case Rarity.Rarity1:
                weaponIcon.color = Color.white;
                break;
            case Rarity.Rarity2:
                weaponIcon.color = Color.yellow;
                break;
            case Rarity.Rarity3:
                weaponIcon.color = Color.magenta;
                break;
            case Rarity.Rarity4:
                weaponIcon.color = Color.green;
                break;
            case Rarity.Rarity5:
                weaponIcon.color = Color.cyan;
                break;
            case Rarity.Rarity6:
                weaponIcon.color = Color.blue;
                break;
            case Rarity.Rarity7:
                weaponIcon.color = Color.red;
                break;
            case Rarity.Rarity8:
                weaponIcon.color = Color.white;
                break;
            case Rarity.Rarity9:
                weaponIcon.color = Color.white;
                break;
            case Rarity.Rarity10:
                weaponIcon.color = Color.white;
                break;
            case Rarity.RarityX:
                weaponIcon.color = Color.white;
                break;
            default:
                Debug.LogError($"Trying to update rarity colour to an invalid value: {rarity}");
                break;
        }
    }

}
