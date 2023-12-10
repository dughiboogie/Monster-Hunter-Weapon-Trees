using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    public static ScreensManager instance;

    [SerializeField]
    private GameObject mainScreen;

    [SerializeField]
    private GameObject gameScreen;

    [SerializeField]
    private LoadGameScreen loadGameScreen;

    #region Singleton

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of ScreensManager found!");
            return;
        }
        instance = this;
    }

    #endregion

    public void CreateNewGame()
    {
        GameController.instance.CreateNewGame();
        GoToGameScreen();
    }

    public void GoToGameScreen()
    {
        mainScreen.SetActive(false);
        loadGameScreen.gameObject.SetActive(false);
        gameScreen.SetActive(true);
    }

    public void GoToMainMenu()
    {
        gameScreen.SetActive(false);
        loadGameScreen.gameObject.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void GoToLoadGameScreen()
    {
        gameScreen.SetActive(false);
        loadGameScreen.gameObject.SetActive(true);
        mainScreen.SetActive(false);
    }

}
