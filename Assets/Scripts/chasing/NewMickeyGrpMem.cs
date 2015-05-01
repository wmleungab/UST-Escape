using UnityEngine;
using System.Collections;

public class NewMickeyGrpMem : MonoBehaviour {

	// Use this for initialization
	void gotoBattle () {
		
		transform.parent.SendMessage("groupBattle");
		
	}
	
	void sendAlert(){
		transform.parent.SendMessage("AlertPlayer");
	}
}
