using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Text;
using System.Linq;
using System.IO;
using System;


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
	public string notesFileName;
	public float spawnOverhead; //How much time, in seconds, between when the notes should be spawned

	//Private variables
	Queue<Note> loadedNotes = new Queue<Note>(); //Queue to hold loaded notes

	// Use this for initialization
	void Start () {	
		loadedNotes = new Queue<Note>();
		SpawnNotes();
	}

	// Update is called once per frame
	void Update () {
		SpawnNotes();
	}

	public void LoadNotes(string fileName){

		bool debugging = false;
		int linecount = 1;
		float interval = 0;
		float starttime = 0;
		float bpm = 0;
		float currentTime = 0;

		TextAsset f = Resources.Load(fileName) as TextAsset;

		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			MemoryStream stream = new MemoryStream();
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(f.text);
			writer.Flush();
			stream.Position = 0;

			StreamReader theReader = new StreamReader(stream);

			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					//Debug.Log(line);

					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						//tring[] entries = line.Split(',');
						//if (entries.Length > 0)
						//	DoStuff(entries);
						if( linecount == 1 ){
							if( debugging){		Debug.Log("Start time:\t"+line);	}
							starttime = Convert.ToSingle(line) + GameManager.Instance.Global_Offset;
							currentTime = starttime;
						}
						else if( linecount == 2 ){
							if( debugging ){	Debug.Log("BPM:\t\t"+ line);	}
							bpm = Convert.ToSingle(line);
							interval = 60/bpm;
							if(debugging){ Debug.Log("note every " + interval + "s"); }
						}
						else{
							if( line.Equals("q") ){
								if(debugging){Debug.Log(currentTime + "\tquarter note!");}
								loadedNotes.Enqueue(new Note(currentTime,0));
								currentTime += interval*1.0f;
							}
							else if( line.Equals("h") ){
								if(debugging){Debug.Log(currentTime + "\thalf note!");}
								loadedNotes.Enqueue(new Note(currentTime,0));
								currentTime += interval*2.0f;
							}
							else if( line.Equals("w") ){
								if(debugging){Debug.Log(currentTime + "\twhole note!");}
								loadedNotes.Enqueue(new Note(currentTime,0));
								currentTime += interval*4*1.0f;
							}
							else if( line.Equals("e") ){
								if(debugging){Debug.Log(currentTime + "\teight note!");}
								loadedNotes.Enqueue(new Note(currentTime,0));
								currentTime += interval*0.5f;
							}
							else if( line.Equals("er") ){
								if(debugging){Debug.Log(currentTime + "\teigth rest!");}
								currentTime += interval*0.5f;
							}
							else if( line.Equals("qr") ){
								if(debugging){Debug.Log(currentTime + "\tquarter rest!");}
								currentTime += interval*1.0f;
							}
							else if( line.Equals("hr") ){
								if(debugging){Debug.Log(currentTime + "\thalf rest!");}
								currentTime += interval*2.0f;
							}
							else if( line.Equals("wr") ){
								if(debugging){Debug.Log(currentTime + "\twhole rest!");}
								currentTime += interval*4.0f;
							}
							else if( line.Equals("eh") ){
								if(debugging){Debug.Log(currentTime + "\teigth hold!");}
								loadedNotes.Enqueue(new Note(currentTime,interval*0.5f));
								currentTime += interval*0.5f;
							}
							else if( line.Equals("qh") ){
								if(debugging){Debug.Log(currentTime + "\tquarter hold!");}
								loadedNotes.Enqueue(new Note(currentTime,interval*1.0f));
								currentTime += interval*1.0f;
							}
							else if( line.Equals("hh") ){
								if(debugging){Debug.Log(currentTime + "\thalf hold!");}
								loadedNotes.Enqueue(new Note(currentTime,interval*2.0f));
								currentTime += interval*2.0f;
							}
							else if( line.Equals("wh") ){
								if(debugging){Debug.Log(currentTime + "\twhole hold!");}
								loadedNotes.Enqueue(new Note(currentTime,interval*4.0f));
								currentTime += interval*4.0f;
							}

						}
					}
					linecount ++;
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
				//return true;
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			//Console.WriteLine("{0}\n", e.Message);
			Debug.Log(e.Message);
			//return false;
		}

	}

	public void SpawnNotes(){
		//If there is a next note, and its within the spawn overhead time (ie, it's time to spawn it)
		if(loadedNotes.Count > 0 && (loadedNotes.Peek().time - MusicController.Instance.GetSongTime()) <= spawnOverhead){			
			Note note = loadedNotes.Dequeue();
			GameObject noteGO = Instantiate(notePrefab, Camera.main.transform.position + Vector3.up*10000, Quaternion.identity) as GameObject;
			NoteController noteC = noteGO.GetComponent<NoteController>();
			noteC.SetNote(note);
		}
	}
}
