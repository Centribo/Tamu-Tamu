using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StickManager : MonoBehaviour {

	public static StickManager instance = null;
	public static StickManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (StickManager)FindObjectOfType(typeof(StickManager)); //Find it
			}
			return instance; //Return it
		}
	}

	public SpriteRenderer stickRenderer;
	SpriteRenderer sr;
	public List<Sprite> stickSprites;
	int bgAmount = 0;

	// Use this for initialization
	void Start () {

		sr = stickRenderer;
		// load all of the textures


		// create all of the sprites and set defaults
	}

	// Update is called once per frame
	void Update () {

	}

	void Awake(){
		stickRenderer = GetComponent<SpriteRenderer>();
	}

	// swaps the image on the background
	public void moveBG() {


		bgAmount++;
		Debug.Log ("shifting animation" + bgAmount);

		stickRenderer.sprite = stickSprites [bgAmount%2];

	}

	// draws a note and plays the sound based off the type of note
	// also swaps the image on the notes
	public void drawNote() {

	}

	// plays a snare hit
	public void playSnare() {

	}
}
