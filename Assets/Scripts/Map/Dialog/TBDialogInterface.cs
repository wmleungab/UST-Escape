using UnityEngine;
using System.Collections;

public class TBDialogInterface : DialogInterface {

	treasureBoxScript tbObj;
	Transform keyObj;

	public void startDialog(Transform _keyObj, treasureBoxScript _tbObj){
	
		tbObj = _tbObj;
		keyObj = _keyObj;
	
		DialogSystem.character c=

			DialogSystem.character.SYSTEM
		;
		
		string dialog = 
			"Use " + keyObj.name + " ?";
			
		int dialogID = 0;
		if(keyObj.name=="beautifulKey")
			dialogID = 2;
		else
			dialogID = 1;
		
		optionSelect(c,dialog,dialogID);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish with selection " + selection);
		if(selection==1){
			tbObj.dialogCallback(id, keyObj);
		}
	}
}
