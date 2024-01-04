using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputElementsLocker : MonoBehaviour
{
    public static InputElementsLocker instance;

    [SerializeField]
    private Toggle lockToggle;

    [SerializeField]
    private List<Selectable> staticLockableElements = new List<Selectable>();

    private Dictionary<UniqueID, List<Selectable>> dynamicLockableElements = new Dictionary<UniqueID, List<Selectable>>();

    public bool LockActive { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Multiple instances of InputElementsLocker found!");
            return;
        }
        instance = this;
    }

    public void LockChanges()
    {
        foreach (Selectable selectable in staticLockableElements)
        {
            selectable.interactable = LockActive;
        }

        foreach (KeyValuePair<UniqueID, List<Selectable>> dynamicSelectable in dynamicLockableElements)
        { 
            foreach(Selectable selectable in dynamicSelectable.Value)
            {
                selectable.interactable = LockActive;
            }
        }

        LockActive = !LockActive;
    }

    public void LockDynamicElementsChangesWithoutUpdate()
    {
        foreach(KeyValuePair<UniqueID, List<Selectable>> dynamicSelectable in dynamicLockableElements)
        {
            foreach(Selectable selectable in dynamicSelectable.Value)
            {
                selectable.interactable = !LockActive;
            }
        }
    }

    public void AddLockable(UniqueID id, Selectable lockable)
    {
        if(dynamicLockableElements.TryGetValue(id, out List<Selectable> selectables))
        {
            selectables.Add(lockable);
        }
        else
        {
            dynamicLockableElements.Add(id, new List<Selectable> { lockable });
        }
    }

    public void RemoveLockable(UniqueID id) 
    {  
        dynamicLockableElements.Remove(id);
    }

    public void ResetLock()
    {
        lockToggle.SetIsOnWithoutNotify(false);
        dynamicLockableElements.Clear();
    }
}