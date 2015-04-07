using UnityEngine;
using System.Collections;

[RequireComponent (typeof (sceneInterface))]
public class DoorObject : MonoBehaviour {

	public string next_stage = "atrium_stage";
	public Vector3 player_pos;

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
		GameObject.FindGameObjectWithTag("Player").transform.position = player_pos;
		gameObject.SendMessage("changeScene",next_stage);
	}


}
