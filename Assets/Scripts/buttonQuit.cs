using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonQuit : MonoBehaviour {

	// Quitting the game
	public void quitGame(){
		// Conditional Statement is only necessary to test it within Unity as Application.Quit(); would normally suffit
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
