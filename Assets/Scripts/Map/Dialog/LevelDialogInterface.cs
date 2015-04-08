using UnityEngine;
using System.Collections;

public class LevelDialogInterface : DialogInterface {

	DoorObject doorObj;

	public void startDialog(DoorObject _doorObj, string levelName){
		doorObj = _doorObj;

		DialogSystem.character nameString = DialogSystem.character.SYSTEM;
		string dialogString = "Go to " + levelName + " ?";

		//dsObj.startDialog(nameString, dialogString);
		optionSelect (nameString, dialogString, 1);
	}
	
		override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		if(doorObj!=null && selection==1) doorObj.dialogCallBack();
	}

}
