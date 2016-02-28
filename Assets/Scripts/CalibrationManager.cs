using UnityEngine;
using System.Collections;

public class CalibrationManager : MonoBehaviour {

	public static CalibrationManager instance = null;
	public static CalibrationManager Instance { //Singleton pattern instance
		get { //Getter
			if(instance == null){ //If its null,
				instance = (CalibrationManager)FindObjectOfType(typeof(CalibrationManager)); //Find it
			}
			return instance; //Return it
		}
	}
		
	float runningTotal = 0;
	int samples = 0;

	public void TakeDelta(float delta){
		runningTotal += delta;
		samples++;
	
		//GameManager.Instance.Global_Offset = runningTotal / (float)samples;
		GameManager.Instance.Total_Offset += delta;
		GameManager.Instance.Offset_Samples++;
	}
}
