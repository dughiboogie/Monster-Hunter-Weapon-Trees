using UnityEngine;

public class KeyboardInputManager : MonoBehaviour
{
    public static KeyboardInputManager instance;

    private bool leftAltDown = false;

    private void Awake()
    {
        if(instance != null) {
            Debug.LogWarning("Multiple instances of KeyboardInputManager found!");
            return;
        }
        instance = this;
    }

    private void Update()
    {
        if(InputElementsLocker.instance.LockActive)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.LeftAlt)) {
            Debug.Log("Left alt key is pressed.");
            leftAltDown = true;
        }

        if(Input.GetKeyDown(KeyCode.Delete)) {
            Debug.Log("Delete key is pressed.");

            if(leftAltDown) {
                GameController.instance.DeleteWeaponEvolution();
            }
            else {
                GameController.instance.DeleteSelectedWeapon();
            }
        }
    }

    public void UpdateConsoleHints(HintContext context)
    {
        ConsolePrinter.instance.UpdateHint1("Delete: Cancel weapon");
        ConsolePrinter.instance.UpdateHint2("LAlt+Delete: Cancel previous weapon");
    }
}

public enum HintContext {
    WeaponSelected
}