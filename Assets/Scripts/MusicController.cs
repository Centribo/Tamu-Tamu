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
	bool isPlaying = false;
	//public float interval;

	//Use this for getting references to our components
	void Awake(){
		song = GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		Invoke("StartSong", 3);
	}

	// Update is called once per frame
	void Update () {
		UpdateSongTime();
	}

	private void UpdateSongTime(){
		if(isPlaying){
			songTime += Time.time - previousFrameTime;
			previousFrameTime = Time.time;
			//if(song.time != lastReportedPlayheadPosition){
			//	songTime = (songTime + song.time)/2;
			//	lastReportedPlayheadPosition = song.time;
			//}
			//if( Convert.ToInt32(songTime*1000) % Convert.ToInt32() < 10 )
		}
	}

	public void StartSong(){
		previousFrameTime = Time.time;
		lastReportedPlayheadPosition = 0;
		song.Play();
		isPlaying = true;
	}

	public float GetSongTime(){
		return songTime;
	}
}
