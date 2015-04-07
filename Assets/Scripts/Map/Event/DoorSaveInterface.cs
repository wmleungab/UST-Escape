using UnityEngine;
using System.Collections;

public class DoorSaveInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void trackleSaveAndDialog(){
		if(!SaveLoadSystem.getInstance ().labSceneState [(int)SaveLoadSystem.LabSceneState.DOORIVD]){
			this.GetComponent<DoorDialogInterface>().firstTouchingDoor();
			
			SaveLoadSystem.getInstance ().labSceneState [(int)SaveLoadSystem.LabSceneState.DOORIVD] = true;
			SaveLoadSystem.getInstance ().save ();
		}else{
			this.GetComponent<DoorDialogInterface>().subsequentTouchingDoor();
		}
		}
}
