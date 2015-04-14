using UnityEngine;
using System.Collections;

public class LG2Door : DoorObject {

	// Use this for initialization
	void Start () {
		if (SaveLoadSystem.getInstance().atriumSceneStateArr[(int)SaveLoadSystem.AtriumSceneState.FROMLG2]){
			this.gameObject.SetActive(true);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
