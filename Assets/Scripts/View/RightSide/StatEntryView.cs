using UnityEngine;
using UnityEngine.UI;

public class StatEntryView : MonoBehaviour
{
    [SerializeField]
    private Image statRowBackground;

    private void Start()
    {
        SetStatRowBackgroundColor();
    }

    private void SetStatRowBackgroundColor()
    {
        if(transform.GetSiblingIndex() % 2 != 0) {
            statRowBackground.CrossFadeAlpha(5, .1f, true);
        }
        else {
            statRowBackground.CrossFadeAlpha(0, .1f, true);
        }
    }
}