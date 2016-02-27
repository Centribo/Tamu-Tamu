using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	const float PERFECT = 0.06f;
	const float GOOD = 0.07f;
	const float BAD = 0.1f;

	public Note note;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(MusicController.Instance.GetSongTime());
		
		UpdatePosition();
		if(Input.GetButtonDown("Fire2")){
			CheckHit();
		}
	}

	public void SetNote(Note n){
		note = n;
	}

	private void UpdatePosition(){
		if(note != null){
			float y = StrumBarController.Instance.gameObject.transform.position.y - 5*(MusicController.Instance.GetSongTime() - note.time) + MusicController.Instance.visualLatency;
			transform.position = new Vector2(0, y);	
		}
	}

	private void CheckHit(){
		float delta = note.time - MusicController.Instance.GetSongTime();
		delta = Mathf.Abs(delta);
		if(delta <= PERFECT){
			GetComponent<SpriteRenderer>().color = Color.green;
		} else if(delta <= GOOD){
			GetComponent<SpriteRenderer>().color = Color.yellow;
		} else if(delta <= BAD){
			GetComponent<SpriteRenderer>().color = Color.red;
		} else {
			GetComponent<SpriteRenderer>().color = Color.black;
		}
	}
}
