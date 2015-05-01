using UnityEngine;
using System.Collections;

public class NewMickeyGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}

	
	void groupBattle(){
		GetComponent<sceneInterface>().gotoBattle();
		Destroy(this.gameObject);
	}
	
	void AlertPlayer(){
		gameObject.BroadcastMessage("detectPlayer");
	}
	
}
