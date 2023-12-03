using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class StatsPanelView : MonoBehaviour
{
    [Header("Header")]
    [SerializeField]
    private WeaponIconView weaponIcon;

    [SerializeField]
    private Toggle hasWeaponButton;

    [Header("Simple statistics")]
    [SerializeField]
    private TMP_Dropdown rarityValue;

    [SerializeField]
    private TMP_InputField attackValue;

    [SerializeField]
    private TMP_InputField affinity;

    [SerializeField]
    private TMP_InputField defenseBonus;

    [SerializeField]
    private TMP_Dropdown shellingType;

    [SerializeField]
    private TMP_Dropdown shellingLevel;

    [SerializeField]
    private TMP_Dropdown elderseal;

    [SerializeField]
    private TMP_InputField skillName;

    [Header("Elements objects")]
    [SerializeField]
    private Transform statsContentParent;

    [SerializeField]
    private ElementEntryView elementEntryViewPrefab;

    [SerializeField]
    private List<ElementEntryView> elementEntryViews;

    private const int elementViewStartingSiblingIndex = 5;

    [Header("Gem slots")]
    [SerializeField]
    private List<GemSlotView> gemSlotViews;

    [Header("Sharpness")]
    [SerializeField]
    private SharpnessView sharpnessView;

    [SerializeField]
    private SharpnessUpdateView sharpnessUpdateView;

    [SerializeField]
    private SharpnessMaxView sharpnessMaxView;

    public void ResetView()
    {
        hasWeaponButton.SetIsOnWithoutNotify(false);
        rarityValue.SetValueWithoutNotify(0);
        weaponIcon.UpdateRarityColour(0);
        attackValue.SetTextWithoutNotify(string.Empty);
        sharpnessView.ResetView();
        sharpnessUpdateView.ResetView();
        sharpnessMaxView.ResetView();
        affinity.SetTextWithoutNotify(string.Empty);

        ResetElementList();
        ResetGemSlotViews();

        defenseBonus.SetTextWithoutNotify(string.Empty);
        shellingType.SetValueWithoutNotify(0);
        shellingLevel.SetValueWithoutNotify(0);
        elderseal.SetValueWithoutNotify(0);
        skillName.SetTextWithoutNotify(string.Empty);
    }

    public void UpdateView(Weapon weapon)
    {
        hasWeaponButton.SetIsOnWithoutNotify(weapon.hasWeapon);
        rarityValue.SetValueWithoutNotify((int)weapon.weaponStats.rarity);
        weaponIcon.UpdateRarityColour(weapon.weaponStats.rarity);
        attackValue.SetTextWithoutNotify((weapon.weaponStats.attackValue).ToString());
        sharpnessView.UpdateView(weapon.weaponStats.sharpnesses);
        sharpnessUpdateView.UpdateView(weapon.weaponStats.sharpnessesUpdate);
        sharpnessMaxView.UpdateView(weapon.weaponStats.sharpnessesMax);
        affinity.SetTextWithoutNotify(weapon.weaponStats.affinity.ToString() + "%");

        UpdateElementList(weapon);
        UpdateGemSlotViews(weapon.weaponStats.gemSlots);

        defenseBonus.SetTextWithoutNotify(weapon.weaponStats.defenseValue.ToString());
        shellingType.SetValueWithoutNotify((int)weapon.weaponStats.shellingType);
        shellingLevel.SetValueWithoutNotify((int)weapon.weaponStats.shellingLevel);
        elderseal.SetValueWithoutNotify((int)weapon.weaponStats.elderseal);
        skillName.SetTextWithoutNotify(weapon.weaponStats.skill);
    }

    #region Events

    public void OnHasWeaponToggled(bool active)
    {
        GameController.instance.UpdateHasWeapon(active);
    }

    public void OnRarityChanged(TMP_Dropdown change)
    {
        GameController.instance.UpdateRarity(change.captionText.text);
    }

    public void OnAttackValueChanged(string attackValue)
    {
        GameController.instance.UpdateAttackValue(attackValue);
    }

    public void OnAffinityValueChanged(string affinityValue)
    {
        GameController.instance.UpdateAffinityValue(affinityValue);
        affinity.SetTextWithoutNotify(affinityValue + "%");
    }

    public void OnElementAdded()
    {
        GameController.instance.AddElement();
    }

    public void OnDefenseValueChanged(string defenseValue)
    {
        GameController.instance.UpdateDefenseValue(defenseValue);
    }

    public void OnShellingTypeChanged(TMP_Dropdown change)
    {
        GameController.instance.UpdateShellingType(change.captionText.text);
    }

    public void OnShellingLevelChanged(TMP_Dropdown change)
    {
        GameController.instance.UpdateShellingLevel(change.captionText.text);
    }

    public void OnEldersealChanged(TMP_Dropdown change)
    {
        GameController.instance.UpdateElderseal(change.captionText.text);
    }

    public void OnSkillNameChange(string skillName)
    {
        GameController.instance.UpdateSkillName(skillName);
    }

    #endregion

    #region Elements

    private void ResetElementList()
    {
        elementEntryViews[0].ResetView();

        for(int i = 1; i < elementEntryViews.Count;) {
            Destroy(elementEntryViews[i].gameObject);
            elementEntryViews.RemoveAt(i);
        }
    }

    private void UpdateElementList(Weapon weapon)
    {
        // Loop starts from 1 because the first element is always instantiated
        for(int i = 0; i < weapon.weaponStats.weaponElements.Count; i++) {
            if(i != 0) {
                // Instantiate graphic elements
                ElementEntryView elementEntryView = Instantiate(elementEntryViewPrefab, statsContentParent);
                elementEntryView.transform.SetSiblingIndex(elementViewStartingSiblingIndex + i);
                elementEntryViews.Add(elementEntryView);
            }

            Element elementType = weapon.weaponStats.weaponElements[i].elementType;
            var elementValue = weapon.weaponStats.weaponElements[i].elementValue * GameController.instance.GetCurrentElementalDamageMultiplier();
            elementEntryViews[i].UpdateElementEntry(elementType, elementValue);
            
            elementEntryViews[i].UpdateElementEntryIndex(i);
            elementEntryViews[i].UpdateElementHiddenText(weapon.weaponStats.weaponElements[i].hiddenElement);
        }
    }

    #endregion

    #region Gem Slots

    private void ResetGemSlotViews()
    {
        foreach(var gemSlotView in gemSlotViews) {
            gemSlotView.ResetGemSlotView();
        }
    }

    private void UpdateGemSlotViews(List<GemSlot> gemSlots)
    {
        for(int i = 0; i < gemSlots.Count; i++) {
            gemSlotViews[i].UpdateGemSlotView(gemSlots[i]);
        }
    }

    #endregion

}
