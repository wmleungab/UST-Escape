using UnityEngine;
using System.Collections;

public class FloodEvent : EventScript {

	public string getItemName = "";

	override public void startEvent (Transform keyObj) {
		
		StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(getItemName));
		
	}

}
