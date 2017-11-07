using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour {

	public void Load(int levelNumber)
	{
		Application.LoadLevel(levelNumber);
	}
}