using UnityEngine;
using System.Collections;

public class InventoryDialogInterface : DialogInterface {

	// Use this for initialization
	void  dropItemDialog(string itemName){

		DialogSystem.character c=

			DialogSystem.character.SYSTEM
		;
		
		string dialog = 
			"Dropping item " + itemName + " ?";

		
		optionSelect(c,dialog,1);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish with selection " + selection);
		if(id==1 && selection==1){
			
			GetComponent("Inventory").SendMessage("fireDropping");
		}
	}
	
}
