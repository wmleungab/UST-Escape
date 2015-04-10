using UnityEngine;
using System.Collections;

public class MickeyGrpMem : MonoBehaviour {

	// Use this for initialization
	void gotoBattle () {
		
		transform.parent.SendMessage("groupBattle");
		
	}
}
