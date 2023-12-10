using UnityEngine;
using System.Collections.Generic;

public class LoadGameScreen : MonoBehaviour {

    [SerializeField]
    private GameEntryButton gameEntryLoadPrefab;

    [SerializeField]
    private Transform gamesListParent;

    private List<GameEntryButton> gamesListObjects = new List<GameEntryButton>();

    private void OnEnable()
    {
        LoadGamesList();
    }

    private void OnDisable()
    {
        ResetGameList();
    }

    private void ResetGameList()
    {
        foreach(var item in gamesListObjects) {
            Destroy(item.gameObject);
        }

        gamesListObjects.Clear();
    }

    public void LoadGamesList()
    {
        List<Game> gamesList = FileDataManager.instance.GetGamesDataFromFolder();

        // Instantiate list entries
        foreach(Game game in gamesList) {
            GameEntryButton gameEntryButton = Instantiate(gameEntryLoadPrefab, gamesListParent);
            gameEntryButton.SetupGameButton(game.gameName);

            gamesListObjects.Add(gameEntryButton);
        }
    }

}