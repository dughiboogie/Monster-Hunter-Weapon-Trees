using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEntryButton : MonoBehaviour
{
    [SerializeField]
    private Button gameButton;

    [SerializeField]
    private TextMeshProUGUI gameName;

    public void SetupGameButton(string gameName)
    {
        this.gameName.text = gameName;
        gameButton.onClick.AddListener(() => {
            ScreensManager.instance.GoToGameScreen();
            GameController.instance.LoadGame(gameName);
            });
    }
}
