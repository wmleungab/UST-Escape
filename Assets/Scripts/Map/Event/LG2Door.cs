using UnityEngine;
using System.Collections;

public class LG2Door : DoorObject {

	// Use this for initialization
	void Start () {
		//SaveLoadSystem.getInstance ().load ();
		Debug.Log ("From LG2 Door = " + SaveLoadSystem.getInstance().atriumSceneStateArr[(int)SaveLoadSystem.AtriumSceneState.FROMLG2]);
		if (SaveLoadSystem.getInstance().atriumSceneStateArr[(int)SaveLoadSystem.AtriumSceneState.FROMLG2]){
			this.gameObject.SetActive(true);
		}
		else 
			this.gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
