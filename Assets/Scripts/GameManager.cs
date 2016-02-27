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

	//Private variables
	public int score = 0;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		UpdateScoreTextBox(score);
		Global_Offset += Input.GetAxis("Horizontal")*0.0001f;
		UpdateTextBox("GlobalOffsetTextBox", "Offset: " + Global_Offset);

		if(Input.GetButtonDown("Jump")){
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
