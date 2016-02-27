using System.Collections;

public class Note {

	//Seconds
	public float time;
	public float holdTime;
		//If holdTime = 0, then this note is just a "hit"
		//Otherwise, holdTime represents how long the hold lasts for

	//Constructor
	public Note(float time, float holdTime){
		this.time = time;
		this.holdTime = holdTime;
	}
}
