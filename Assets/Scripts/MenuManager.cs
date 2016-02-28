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
		}

		if(Input.GetButtonDown("Button")){
			switch(menuOptions[menuOption]){
				case "Play Game":
					GameManager.Instance.state = GameManager.States.SongSelect;
					//GameManager.Instance.PlayLevel("adamBuilding");
					GameManager.Instance.PlayLevel("Music/happy", "Notes/happy");
				break;
				case "Calibrate":
					GameManager.Instance.state = GameManager.States.Calibration;
				break;
				case "Credits":
					GameManager.Instance.state = GameManager.States.Credits;
				break;
				case "Exit":
					Application.Quit();
					Debug.Log("Attempt to quit in editor");
				break;
			}
		}
	}
}
