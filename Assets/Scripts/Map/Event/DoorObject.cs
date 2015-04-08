using UnityEngine;
using System.Collections;

[RequireComponent (typeof (sceneInterface))]
public class DoorObject : MonoBehaviour {

	public string next_stage = "atrium_stage";
	public string next_stage_name = "Atrium";
	public Vector3 player_pos = new Vector3(0,0,0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter() {
		
		GetComponent<LevelDialogInterface>().startDialog(this, next_stage_name);

	}
	
	void goToNextScene() {
		Debug.Log("go to next scene");	
		GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
		playerObj.GetComponent<NavMeshAgent>().enabled = false;
		playerObj.transform.localPosition = player_pos;
		playerObj.GetComponent<NavMeshAgent>().enabled = true;
		gameObject.SendMessage("changeScene",next_stage);
	}
	
	public void dialogCallBack(){
		goToNextScene();
	}


}
