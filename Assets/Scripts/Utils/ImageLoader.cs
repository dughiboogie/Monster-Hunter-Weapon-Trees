using UnityEngine;
using SimpleFileBrowser;
using System.Collections;
using System.IO;

public class ImageLoader : MonoBehaviour {

    private void Start()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Images", ".jpg", ".png"));
        FileBrowser.SetDefaultFilter(".jpg");
    }

    public void OnLoadImageButtonPressed()
    {
		StartCoroutine(ShowLoadDialogCoroutine());
    }

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select weapon picture", "Select");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log(FileBrowser.Success);

		if(FileBrowser.Success) {
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			// Debug.Log(FileBrowser.Result[0]);
				
			// Or, copy the first file to persistentDataPath
            Directory.CreateDirectory($"{Application.persistentDataPath}/Images");
			
			string destinationPath = Path.Combine(Application.persistentDataPath, "Images", FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
			FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
            GameController.instance.UpdateSelectedWeaponImage(destinationPath);

        }
	}
}
