using UnityEngine;
using UnityEngine.UI;

public class ModalToolboxView : MonoBehaviour
{
    [SerializeField]
    private GameObject toolbox;

    [SerializeField]
    private Button maskPanel;


    public void OnToolboxShow()
    {
        toolbox.SetActive(!toolbox.activeSelf);
        maskPanel.gameObject.SetActive(!maskPanel.gameObject.activeSelf);
    }
}
