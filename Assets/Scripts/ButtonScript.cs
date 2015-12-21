using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {

	/*
	 * This script is ment to be used together
	 * with the UI buttons, where you define
	 * the appropriate method to be called
	 * when the button is clicked.
	 */

	public void RestartLevel() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void ExitGame() {
#if UNITY_EDITOR
		// Compiler runs this if played inside the editor
		UnityEditor.EditorApplication.isPlaying = false;
#else
		// Compiler runs this on standalone builds
		Application.Quit();
#endif	
	}

	public void Test() {
		print ("Testing, testing, " + Random.Range(0,10) + " " + Random.Range(0,10) + " " + Random.Range(0,10)); 
	}

}
