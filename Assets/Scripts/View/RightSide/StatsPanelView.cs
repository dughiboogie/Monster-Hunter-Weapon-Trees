using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsPanelView : MonoBehaviour
{
    [SerializeField]
    private WeaponIconView weaponIcon;

    [SerializeField]
    private Toggle hasWeaponButton;

    [SerializeField]
    private TMP_Dropdown rarityValue;

    [SerializeField]
    private TMP_InputField attackValue;

    [SerializeField]
    private TMP_InputField sharpness;

    [SerializeField]
    private TMP_InputField sharpnessMax;

    [SerializeField]
    private TMP_InputField affinity;

    // TODO Create new classes
    /*
    [SerializeField]
    private TextMeshProUGUI element;

    [SerializeField]
    private TextMeshProUGUI slots;

    [SerializeField]
    private TextMeshProUGUI skill;

    */

    [SerializeField]
    private TMP_InputField defenseBonus;

    [SerializeField]
    private TMP_Dropdown shellingType;

    [SerializeField]
    private TMP_Dropdown shellingLevel;

    [SerializeField]
    private TMP_Dropdown elderseal;


    public void ResetView()
    {
        hasWeaponButton.isOn = false;
        rarityValue.SetValueWithoutNotify(0);
        weaponIcon.UpdateRarityColour(0);
        attackValue.SetTextWithoutNotify(string.Empty);

        defenseBonus.SetTextWithoutNotify(string.Empty);
        shellingType.SetValueWithoutNotify(0);
        shellingLevel.SetValueWithoutNotify(0);
    }

    public void UpdateView(Weapon weapon)
    {
        hasWeaponButton.isOn = weapon.hasWeapon;
        rarityValue.SetValueWithoutNotify((int)weapon.weaponStats.rarity);
        weaponIcon.UpdateRarityColour(weapon.weaponStats.rarity);
        attackValue.SetTextWithoutNotify(weapon.weaponStats.attackValue.ToString());

        defenseBonus.SetTextWithoutNotify(weapon.weaponStats.defenseValue.ToString());

        shellingType.SetValueWithoutNotify((int)weapon.weaponStats.shellingType);
        shellingLevel.SetValueWithoutNotify((int)weapon.weaponStats.shellingLevel);
    }

    #region Events

    public void OnRarityChanged(TMP_Dropdown change)
    {
        GameController.instance.UpdateRarity(change.captionText.text);
    }

    public void OnAttackValueChanged(string attackValue)
    {
        GameController.instance.UpdateAttackValue(attackValue);
    }

    public void OnSharpnessValueChanged(string sharpnessValue)
    {
        GameController.instance.UpdateSharpnessValue(sharpnessValue);
    }

    public void OnSharpnessMaxValueChanged(string sharpnessMaxValue)
    {
        GameController.instance.UpdateSharpnessMaxValue(sharpnessMaxValue);
    }

    public void OnAffinityValueChanged(string affinityValue)
    {
        GameController.instance.UpdateAffinityValue(affinityValue);
    }

    public void OnElementChanged(TMP_Dropdown change)
    {
        // GameController.instance.UpdateElement(change.captionText.text);
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

    #endregion

}
