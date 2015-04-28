using UnityEngine;
using System.Collections;

public class FloodEvent : EventScript {

	public string getItemName = "";
	public int eventNumber = 3;
	static int eventCount = 0;

	override public void startEvent (Transform keyObj) {
		
		StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer(getItemName));
		eventCount++;

		if (eventCount == 1) {
			GetComponent<FloodDialogInterface>().startDialog();
		}
		if (eventCount == 3) {
			GetComponent<FloodDialogInterface>().startDialog2();
		}
		if(eventCount>=(eventNumber*2+1)){
			GetComponent<FloodDialogInterface>().startBattleDialog();
		}
		
	}

	public void battleDialogCallback(){
		SaveLoadSystem.getInstance ().currentSceneType = SaveLoadSystem.SceneType.ATRIUM;
		SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.FROMLG2] = true;
		SaveLoadSystem.getInstance ().save ();		
		gameObject.SendMessage("changeScene", "LG2_stage");

	}

}
