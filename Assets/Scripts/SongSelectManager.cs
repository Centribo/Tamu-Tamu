using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SongSelectManager : MonoBehaviour {

	public static SongSelectManager instance = null;
	public static SongSelectManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (SongSelectManager)FindObjectOfType(typeof(SongSelectManager)); //Find it
			}
			return instance; //Return it
		}
	}

	public List<string> songChoices;
	public List<string> musicFileNames;
	public List<string> notesFileNames;

	public float menuWaitTime;

	int menuOption = 0;
	float menuCycleTimer;
	float fontScale = 0.12f;

	// Use this for initialization
	void Start () {
		menuCycleTimer = 0;
		int fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) * fontScale);
		foreach(Transform child in transform){
			if(child.GetComponent<Text>() != null){
				child.GetComponent<Text>().fontSize = fontSize;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		UpdateTextBox("SongTextBox", songChoices[menuOption]);

		Debug.Log(menuOption);

		menuCycleTimer += Time.deltaTime;
		if(menuCycleTimer >= menuWaitTime){
			menuCycleTimer = 0;
			menuOption = (menuOption + 1) % songChoices.Count;
		}

		if(Input.GetButtonDown("Button")){
			GameManager.Instance.PlayLevel(musicFileNames[menuOption], notesFileNames[menuOption]);
		}
	}

	public void UpdateTextBox(string textBoxName, string text){
		Transform textBox = transform.FindChild(textBoxName);
		textBox.GetComponent<Text>().text = text;
	}
}
