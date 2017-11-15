using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	// Reusable method to load Scenes / Game Levels
	public void Load(int levelNumber)
	{
		Application.LoadLevel(levelNumber);
	}
}