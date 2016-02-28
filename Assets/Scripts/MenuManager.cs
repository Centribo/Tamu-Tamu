using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {

	string[] menuOptions = {
		"Play Game",
		"Calibrate",
		"Credits",
		"Exit"
	};

	public List<Sprite> menuSprites;
	public float menuWaitTime;

	int menuOption = 0;
	float menuCycleTimer;
	SpriteRenderer  sr;

	//Use this for getting references to components
	void Awake(){
		sr = GetComponent<SpriteRenderer>();
	}

	// Use this for initialization
	void Start () {
		menuCycleTimer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log(menuOptions[menuOption]);

		menuCycleTimer += Time.deltaTime;
		if(menuCycleTimer >= menuWaitTime){
			menuCycleTimer = 0;
			menuOption = (menuOption + 1) % menuOptions.Length;
		}

		//Change graphics
		sr.sprite = menuSprites[menuOption];

		//Check input
		if(Input.GetButtonDown("Button")){
			switch(menuOptions[menuOption]){
				case "Play Game":
					GameManager.Instance.state = GameManager.States.SongSelect;
					//GameManager.Instance.PlayLevel("adamBuilding");
					//GameManager.Instance.PlayLevel("Music/happy", "Notes/happy");
					GameManager.Instance.LoadScene("SongSelectScene");
				break;
				case "Calibrate":
					GameManager.Instance.state = GameManager.States.Calibration;
					GameManager.Instance.PlayLevel("CalibrationScene");
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
