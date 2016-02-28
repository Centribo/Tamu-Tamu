using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour {

	public static EffectManager instance = null;
	public static EffectManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (EffectManager)FindObjectOfType(typeof(EffectManager)); //Find it
			}
			return instance; //Return it
		}
	}

	public SpriteRenderer backgroundRenderer;
	SpriteRenderer sr;
	public List<Sprite> backgroundSprites;
	int bgAmount = 0;

	// Use this for initialization
	void Start () {
	
		sr = backgroundRenderer;
		// load all of the textures


		// create all of the sprites and set defaults
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake(){
		backgroundRenderer = GetComponent<SpriteRenderer>();
	}

	// swaps the image on the background
	public void moveBG() {


		bgAmount++;
		Debug.Log ("shifting animation" + bgAmount);

		backgroundRenderer.sprite = backgroundSprites [bgAmount%2];

	}

	// draws a note and plays the sound based off the type of note
	// also swaps the image on the notes
	public void drawNote() {

	}

	// plays a snare hit
	public void playSnare() {

	}
}
