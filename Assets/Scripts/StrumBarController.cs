using UnityEngine;
using System.Collections;

public class StrumBarController : MonoBehaviour {
	public static StrumBarController instance = null;
	public static StrumBarController Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (StrumBarController)FindObjectOfType(typeof(StrumBarController)); //Find it
			}
			return instance; //Return it
		}
	}
}
