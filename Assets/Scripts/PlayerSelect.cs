using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour {

	public GameObject playerMenuPanel;

	public void openPlayerMenu(){
		playerMenuPanel.SetActive (true);
	}
}
