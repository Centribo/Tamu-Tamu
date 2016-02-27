using UnityEngine;
using System.Collections;

public class NoteManager : MonoBehaviour {

	Queue<Note> loadedNotes;

	// Use this for initialization
	void Start () {
		LoadNotes("test.txt");
		SpawnNotes();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadNotes(string fileName){

	}

	public void SpawnNotes(){

	}
}
