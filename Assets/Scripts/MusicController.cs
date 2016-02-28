using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public static MusicController instance = null;
	public static MusicController Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (MusicController)FindObjectOfType(typeof(MusicController)); //Find it
			}
			return instance; //Return it
		}
	}

	//Private variables
	AudioSource song;
	float previousFrameTime;
	float songTime;
	float lastReportedPlayheadPosition;

	//Use this for getting references to our components
	void Awake(){
		song = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		UpdateSongTime();
		if(Input.GetButtonUp("Horizontal")){
			StartSong();
		}
	}

	private void UpdateSongTime(){
		songTime += Time.time - previousFrameTime;
		previousFrameTime = Time.time;
		if(song.time != lastReportedPlayheadPosition){
			songTime = (songTime + song.time)/2;
			lastReportedPlayheadPosition = song.time;
		}
	}

	public void StartSong(){
		previousFrameTime = Time.time;
		lastReportedPlayheadPosition = 0;
		song.Play();
	}

	public float GetSongTime(){
		return songTime;
	}
}
