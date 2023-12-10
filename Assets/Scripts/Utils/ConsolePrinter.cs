using UnityEngine;
using TMPro;

public class ConsolePrinter : MonoBehaviour
{
    public static ConsolePrinter instance;

    [SerializeField]
    private TextMeshProUGUI hint1;

    [SerializeField]
    private TextMeshProUGUI hint2;

    [SerializeField]
    private TextMeshProUGUI xCoordinate;

    [SerializeField]
    private TextMeshProUGUI yCoordinate;

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of ConsolePrinter found!");
            return;
        }
        instance = this;

        ResetConsoleView();
    }

    public void ResetConsoleView()
    {
        hint1.text = string.Empty;
        hint2.text = string.Empty;
        xCoordinate.text = string.Empty;
        yCoordinate.text = string.Empty;
    }

    public void UpdateHint1(string text)
    {
        hint1.text = text;
    }

    public void UpdateHint2(string text)
    {
        hint2.text = text;
    }

    public void UpdateConsoleXCoordinates(int xCoord)
    {
       xCoordinate.text = "X: " + xCoord;
    }

    public void UpdateConsoleYCoordinates(int yCoord)
    {
        yCoordinate.text = "Y: " + yCoord;
    }

}
