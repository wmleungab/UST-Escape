using UnityEngine;
using System.Collections;

[RequireComponent (typeof (sceneInterface))]
public class DoorScript : MonoBehaviour {

	public string next_stage = "atrium_stage";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter() {
		
		goToNextScene();

	}
	void goToNextScene() {
		Debug.Log("go to next scene");	
		gameObject.SendMessage("changeScene",next_stage);
	}


}
