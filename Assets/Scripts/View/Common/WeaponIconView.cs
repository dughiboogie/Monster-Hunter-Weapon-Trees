using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconView : MonoBehaviour
{
    [SerializeField]
    private Image weaponIcon;

    [SerializeField]
    public List<RawImage> elementIcons;

    [SerializeField]
    private Texture2D fireElementIcon;
    [SerializeField]
    private Texture2D waterElementIcon;
    [SerializeField]
    private Texture2D thunderElementIcon;
    [SerializeField]
    private Texture2D iceElementIcon;
    [SerializeField]
    private Texture2D dragonElementIcon;
    [SerializeField]
    private Texture2D poisonElementIcon;
    [SerializeField]
    private Texture2D sleepElementIcon;
    [SerializeField]
    private Texture2D paralysisElementIcon;
    [SerializeField]
    private Texture2D blastElementIcon;

    private Color32 rarityColor1 = new Color32(232, 232, 232, 100);
    private Color32 rarityColor2 = new Color32(176,148, 248, 100);
    private Color32 rarityColor3 = new Color32(224, 216, 96, 100);
    private Color32 rarityColor4 = new Color32(232, 144, 160, 100);
    private Color32 rarityColor5 = new Color32(112, 200, 120, 100);
    private Color32 rarityColor6 = new Color32(112, 144, 248, 100);
    private Color32 rarityColor7 = new Color32(224, 88, 96, 100);
    private Color32 rarityColor8 = new Color32(80, 160, 176, 100);
    private Color32 rarityColor9 = new Color32(240, 140, 96, 100);
    private Color32 rarityColor10 = new Color32(248, 72, 192, 100);
    private Color32 rarityColor11 = new Color32(255, 255, 0, 100);
    private Color32 rarityColor12 = new Color32(255, 67, 93, 100);
    private Color32 rarityColorX = new Color32(128, 0, 248, 100);

    public void UpdateRarityColour(Rarity rarity)
    {
        switch(rarity) {
            case Rarity.Rarity1:
                weaponIcon.color = rarityColor1;
                break;
            case Rarity.Rarity2:
                weaponIcon.color = rarityColor2;
                break;
            case Rarity.Rarity3:
                weaponIcon.color = rarityColor3;
                break;
            case Rarity.Rarity4:
                weaponIcon.color = rarityColor4;
                break;
            case Rarity.Rarity5:
                weaponIcon.color = rarityColor5;
                break;
            case Rarity.Rarity6:
                weaponIcon.color = rarityColor6;
                break;
            case Rarity.Rarity7:
                weaponIcon.color = rarityColor7;
                break;
            case Rarity.Rarity8:
                weaponIcon.color = rarityColor8;
                break;
            case Rarity.Rarity9:
                weaponIcon.color = rarityColor9;
                break;
            case Rarity.Rarity10:
                weaponIcon.color = rarityColor10;
                break;
            case Rarity.Rarity11:
                weaponIcon.color = rarityColor11;
                break;
            case Rarity.Rarity12:
                weaponIcon.color = rarityColor12;
                break;
            case Rarity.RarityX:
                weaponIcon.color = rarityColorX;
                break;
            default:
                Debug.LogError($"Trying to update rarity colour to an invalid value: {rarity}");
                break;
        }
    }

    public void UpdateElementIcon(int elementIndex, Element element, bool hiddenElement)
    {
        if(elementIndex < 3) {
            Texture2D texture = new Texture2D(256, 256);

            // Set Sprite based on Element
            switch(element) {
                case Element.None:
                    texture = null;
                    break;
                case Element.Raw:
                    texture = null;
                    break;
                case Element.Fire:
                    texture = fireElementIcon;
                    break;
                case Element.Water:
                    texture = waterElementIcon;
                    break;
                case Element.Thunder:
                    texture = thunderElementIcon;
                    break;
                case Element.Ice:
                    texture = iceElementIcon;
                    break;
                case Element.Dragon:
                    texture = dragonElementIcon;
                    break;
                case Element.Poison:
                    texture = poisonElementIcon;
                    break;
                case Element.Sleep:
                    texture = sleepElementIcon;
                    break;
                case Element.Paralysis:
                    texture = paralysisElementIcon;
                    break;
                case Element.Blast:
                    texture = blastElementIcon;
                    break;
                default:
                    Debug.LogError($"Invalid weapon element icon: {element}");
                    break;
            }

            elementIcons[elementIndex].gameObject.SetActive(texture != null);
            elementIcons[elementIndex].texture = texture;

            if(hiddenElement) {
                elementIcons[elementIndex].color = Color.gray;
            }
            else {
                elementIcons[elementIndex].color = Color.white;
            }
        } 
    }

}
