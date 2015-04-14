using UnityEngine;
using System.Collections;

public class FloodEvent : EventScript {

	public string getItemName = "";
	public int eventNumber = 3;
	static int eventCount = 0;

	override public void startEvent (Transform keyObj) {
		
		StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(getItemName));
		eventCount++;
		
		if(eventCount>=(eventNumber*2+1)){
			SaveLoadSystem slObj = SaveLoadSystem.getInstance ();
			if(slObj != null){
				slObj.atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.FROMLG2] = true;
				slObj.save ();		
			}
			gameObject.SendMessage("changeScene", "LG2_stage");
		}
		
	}

}
