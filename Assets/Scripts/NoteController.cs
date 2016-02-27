using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	const float PERFECT = 0.06f;
	const float GOOD = 0.07f;
	const float BAD = 0.1f;

	public Note note;
	public float time;

	// Use this for initialization
	void Start () {
		note = new Note (0, 0);
		note.time = time;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(MusicController.Instance.GetSongTime());
		
		UpdatePosition();
		CheckHit();
		if(Input.GetButtonDown("Fire2")){
			CheckHit();
		}
	}

	private void UpdatePosition(){
		float y = StrumBarController.Instance.gameObject.transform.position.y - 3*(MusicController.Instance.GetSongTime() - note.time) + MusicController.Instance.visualLatency;
		transform.position = new Vector2(0, y);
	}

	private void CheckHit(){
		float delta = note.time - MusicController.Instance.GetSongTime();
		delta = Mathf.Abs(delta);
		/*Debug.Log("Checking for note hit! Delta: " + delta);
		if(delta <= PERFECT){
			Debug.Log("Perfect hit!");
		} else if(delta <= GOOD){
			Debug.Log("Good hit!");
		} else if(delta <= BAD){
			Debug.Log("Bad hit!");
		} else {
			Debug.Log("Miss!");
		}*/
	}
}
