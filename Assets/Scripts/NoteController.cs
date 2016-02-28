using UnityEngine;
using System.Collections;

public class NoteController : MonoBehaviour {

	const float PERFECT = 0.04f;
	const float GOOD = 0.06f;
	const float BAD = 0.08f;
	const float MISS = 0.15f;

	//Public variables
	public Note note;

	//Private variables
	LineRenderer lr;
	float stretchFactor = 7.5f;
	float holdFactor = 0.8f;
	CalibrationManager CM;

	bool isBeingHeld = false;
	bool isMissed = false;
	bool isHit = false;

	//Used for getting references to objects
	void Awake(){
		lr = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start () {
		CM = FindObjectOfType<CalibrationManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePosition();
		UpdateVisuals();
		CheckInput();
	}

	private void CheckInput(){
		if(Input.GetButtonDown("Button")){
			CheckHit();
		}

		if(isBeingHeld){
			if(Input.GetButton("Button")){
				GameManager.Instance.score += 10;
				float delta = (note.time + (note.holdTime*holdFactor)) - MusicController.Instance.GetSongTime();
				if(delta <= 0){
					isBeingHeld = false;
					isMissed = false;
					isHit = true;
				}
			} else {
				isHit = true;
				isMissed = true;
				isBeingHeld = false;
			}
		}
	}

	public void SetNote(Note n){
		note = n;
		if(note.holdTime != 0){
			lr.enabled = true;
		} else {
			lr.enabled = false;
		}
	}

	private void UpdatePosition(){
		if(note != null){
			float y = StrumBarController.Instance.gameObject.transform.position.y - stretchFactor*(MusicController.Instance.GetSongTime() - note.time);
			transform.position = new Vector2(0, y);	
		}
	}

	private void UpdateVisuals(){
		if(note.holdTime != 0){
			lr.SetPosition(0, transform.position);
			float y = StrumBarController.Instance.gameObject.transform.position.y - stretchFactor*(MusicController.Instance.GetSongTime() - note.time - (note.holdTime*holdFactor));
			lr.SetPosition(1, new Vector3(0, y, 0));
			
			if(!isHit && !isBeingHeld){
				lr.SetColors(Color.white, Color.white);	
			} else if(isHit && !isBeingHeld){
				lr.SetColors(Color.grey, Color.grey);
			} else if(isHit && isBeingHeld){
				lr.SetColors(Color.green, Color.green);
			}
		}

	}

	private void CheckHit(){
		if(!isHit && !isMissed){
			float delta = note.time - MusicController.Instance.GetSongTime();

			//if (EffectManager.Instance != null) {
			//EffectManager.Instance.moveBG ();
			//Debug.Log ("hit a note");
			//}

			if(delta <= PERFECT && delta >= -PERFECT){
				GetComponent<SpriteRenderer>().color = Color.green;
				GameManager.Instance.score += 100;
				isHit = true;
				isMissed = false;
				EffectManager.Instance.moveBG ();
				StickManager.Instance.moveBG ();

			} else if(delta <= GOOD && delta >= -GOOD){
				GetComponent<SpriteRenderer>().color = Color.yellow;
				GameManager.Instance.score += 50;
				isHit = true;
				isMissed = false;
				EffectManager.Instance.moveBG ();
				StickManager.Instance.moveBG ();


			} else if(delta <= BAD && delta >= -BAD){
				GetComponent<SpriteRenderer>().color = Color.red;
				GameManager.Instance.score += 10;
				isHit = true;
				isMissed = false;
				EffectManager.Instance.moveBG ();
				StickManager.Instance.moveBG ();


			} else if(delta <= MISS && delta >= -MISS){
				GetComponent<SpriteRenderer>().color = Color.black;
				lr.SetColors(Color.black, Color.black);
				GameManager.Instance.score += 0;
				isHit = false;
				isMissed = true;
				EffectManager.Instance.moveBG ();
				StickManager.Instance.moveBG ();


			} else {
				GetComponent<SpriteRenderer>().color = Color.white;
			}

			if (isHit && !isMissed && CM != null) {
				CM.TakeDelta (delta);
			}

			if(note.holdTime != 0 && isHit && !isMissed){
				isBeingHeld = true;
			}
		}
	}
}
