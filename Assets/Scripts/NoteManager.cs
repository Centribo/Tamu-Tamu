using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoteManager : MonoBehaviour {

	public static NoteManager instance = null;
	public static NoteManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (NoteManager)FindObjectOfType(typeof(NoteManager)); //Find it
			}
			return instance; //Return it
		}
	}

	//Public variables
	public GameObject notePrefab;
	public float spawnOverhead; //How much time, in seconds, between when the notes should be spawned

	//Private variables
	Queue<Note> loadedNotes = new Queue<Note>(); //Queue to hold loaded notes

	// Use this for initialization
	void Start () {
		LoadNotes("test.txt");
		SpawnNotes();
	}
	
	// Update is called once per frame
	void Update () {
		SpawnNotes();
	}

	public void LoadNotes(string fileName){
		
	}

	public void SpawnNotes(){
		//If there is a next note, and its within the spawn overhead time (ie, it's time to spawn it)
		if(loadedNotes.Count > 0 && (loadedNotes.Peek().time - MusicController.Instance.GetSongTime()) <= spawnOverhead){
			if(true){
				Note note = loadedNotes.Dequeue();
				if(note.holdTime == 0){
					GameObject noteGO = Instantiate(notePrefab, Camera.main.transform.position + Vector3.up*10000, Quaternion.identity) as GameObject;
					NoteController noteC = noteGO.GetComponent<NoteController>();
					noteC.SetNote(note);
				}
			}
		}
	}
}
