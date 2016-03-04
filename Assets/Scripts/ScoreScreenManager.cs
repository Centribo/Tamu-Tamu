using UnityEngine;
using System.Collections;

public class ScoreScreenManager : MonoBehaviour {

	public static ScoreScreenManager instance = null;
	public static ScoreScreenManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (ScoreScreenManager)FindObjectOfType(typeof(ScoreScreenManager)); //Find it
			}
			return instance; //Return it
		}
	}

	
}
