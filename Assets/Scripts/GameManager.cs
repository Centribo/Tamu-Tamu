using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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

	//Public variables
	public enum States { MainMenu, SongSelect, Calibration, Credits, PlayingSong, EndScreen };
	public States state = States.MainMenu;
	public float Global_Offset = 0;
	public float Total_Offset = 0;
	public int Offset_Samples = 0;
	public int score = 0;

	//Private variables
	float fontScale = 0.12f;
	string songFileName;
	string notesFileName;

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
		
		Global_Offset += Input.GetAxis("Horizontal")*0.001f;
		UpdateTextBox("GlobalOffsetTextBox", "Offset: " + Global_Offset);
	}

	public void PlayLevel(string levelName){
		SceneManager.LoadScene(levelName);

		Invoke ("LoadNotes", 1);
	}

	public void LoadNotes(){
		//Forces NoteManager to LoadNotes. Usually used after level loading with a call by Invoke()
		NoteManager.Instance.LoadNotes();
	}

	public void PlayLevel(string songFileName, string notesFileName){
		SceneManager.LoadScene("PlayingScene");

		this.songFileName = songFileName;
		this.notesFileName = notesFileName;
	
		Invoke("LoadData", 1);
	}

	public void LoadData(){
		MusicController.Instance.GetComponent<AudioSource>().clip = Resources.Load(songFileName) as AudioClip;
		NoteManager.Instance.LoadNotes(notesFileName);
	}

	public void UpdateScoreTextBox(int score){
		UpdateTextBox("ScoreTextBox", "Score: " + score);
	}

	public void UpdateTextBox(string textBoxName, string text){
		Transform textBox = transform.FindChild(textBoxName);
		textBox.GetComponent<Text>().text = text;
	}
}
