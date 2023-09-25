using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerPrefsDelete : EditorWindow {

	[MenuItem("Timuz/PlayerPrefs/Delete %Q")]
    public static void DeletePrefs() {
        PlayerPrefs.DeleteAll();
        EditorUtility.DisplayDialog("Timuz","PlayerPrefs deleted successfully","Ok");
    }

}
