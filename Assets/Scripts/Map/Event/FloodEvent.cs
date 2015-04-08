using UnityEngine;
using System.Collections;

public class FloodEvent : EventScript {

	public string getItemName = "";
	static int eventCount = 0;

	override public void startEvent (Transform keyObj) {
		
		StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(getItemName));
		eventCount++;
		
		if(eventCount>=1){
			gameObject.SendMessage("gotoBattle");
		}
		
	}

}
