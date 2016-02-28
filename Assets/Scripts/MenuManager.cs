using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	string[] menuOptions = {
		"Play Game",
		"Calibrate",
		"Credits",
		"Exit"
	};

	public float menuWaitTime;

	int menuOption = 0;
	float menuCycleTimer;

	// Use this for initialization
	void Start () {
		menuCycleTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		menuCycleTimer += Time.deltaTime;
		if(menuCycleTimer >= menuWaitTime){
			menuCycleTimer = 0;
			menuOption = (menuOption + 1) % menuOptions.Length;
			Debug.Log(menuOptions[menuOption]);
		}
	}
}
