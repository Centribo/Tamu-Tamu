using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
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

		state = States.PlayingSong;

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
	
		state = States.PlayingSong;

		Invoke("LoadData", 1);
	}

	public void LoadScene(string scene){
		SceneManager.LoadScene(scene);
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


	public bool loadCalibration(){
		Debug.Log ("Loading calibration");
		/*try{
			TextAsset f = Resources.Load(Application.persistentDataPath + "/calib.txt") as TextAsset;
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(f.text);
			writer.Flush();
			stream.Position = 0;

			//Debug.Log("------Successfully Loaded Calibration file");


			StreamReader theReader = new StreamReader(stream);

			using (theReader) {
				string line = theReader.ReadLine();
				this.Global_Offset = Convert.ToSingle(line);
			}
		}
		catch(Exception e){
			Debug.Log ("bad calibration check");
			Debug.Log (e); 	
		}*/
		// Read the file and display it line by line.
		try {
			System.IO.StreamReader file = 
			new System.IO.StreamReader(Application.persistentDataPath + "/calib.txt");
			string line = file.ReadLine ();
			this.Global_Offset = Convert.ToSingle (line);
			file.Close();
		} catch (Exception e) {
			Debug.Log (e);
			return false;
		}
		return true;
	}

	public void saveCalibration(){
		//Debug.Log ("trying to save out calibration");
		Debug.Log (Application.persistentDataPath + "/calib.txt");
		System.IO.File.WriteAllText (Application.persistentDataPath + "/calib.txt", Convert.ToString (this.Global_Offset));
		//System.IO.File.WriteAllText("Assets/Resources/calib.txt", Convert.ToString(this.Global_Offset));
	}
}
