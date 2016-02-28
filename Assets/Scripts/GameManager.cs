using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	public static GameManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (GameManager)FindObjectOfType(typeof(GameManager)); //Find it
			}
			return instance; //Return it
		}
	}

	//Static variables
	public float Global_Offset = 0;
	public int score = 0;
	

	//Private variables
	float fontScale = 0.16f;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		int fontSize = Mathf.RoundToInt(Mathf.Min(Screen.width, Screen.height) * fontScale);
		Transform textBox = transform.FindChild("GlobalOffsetTextBox");
		foreach(Transform child in transform){
			if(child.GetComponent<Text>() != null){
				child.GetComponent<Text>().fontSize = fontSize;
			}
		}
		textBox.GetComponent<Text>().fontSize = fontSize;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateScoreTextBox(score);
		Global_Offset += Input.GetAxis("Horizontal")*0.0001f;
		UpdateTextBox("GlobalOffsetTextBox", "Offset: " + Global_Offset);

		if(Input.GetButtonDown("Fire1")){
			Application.LoadLevel("adamBuilding");
		}
	}

	public void UpdateScoreTextBox(int score){
		UpdateTextBox("ScoreTextBox", "Score: " + score);
	}

	public void UpdateTextBox(string textBoxName, string text){
		Transform textBox = transform.FindChild(textBoxName);
		textBox.GetComponent<Text>().text = text;
	}
}
