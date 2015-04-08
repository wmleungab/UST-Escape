using UnityEngine;
using System.Collections;

public class EventDialogInterface : DialogInterface {

	EventScript eventObj;
	Transform keyObj;
	public DialogSystem.character[] introNameString;
	public string[] introDialogString;

	public void startDialog(Transform _keyObj, EventScript _eventObj){
	
		eventObj = _eventObj;
		keyObj = _keyObj;
	
		DialogSystem.character c=

			DialogSystem.character.SYSTEM
		;
		
		string dialog = 
			"Use " + keyObj.name + " ?";
					
		optionSelect(c,dialog,1);
	}
	
	public void startIntroDialog(){
		if(introDialogString.Length > 0){
			DialogSystem.character[] nameString = introNameString;
			string[] dialogString = introDialogString;

			conversation (nameString, dialogString, 2);
		}
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish with selection " + selection);
		if(selection==1){
			eventObj.dialogCallback(id, keyObj);
		}
	}
}
