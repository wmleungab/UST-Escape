using UnityEngine;
using System.Collections;

public class PresidDialogInterface : DialogInterface {

	President pObj;

	public void startGiveItemDialog(President _pObj){
		pObj = _pObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER,
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER
			};
		string[] dialogString = new string[]{
			"Hey, that guy over there!!",
			"......?",
			"Don't you recongise me?",
			"(Familiar face but can't remember who...)",
			"Do you work hard on your studies? \nGive you an APPLE and keep it on!",
			"Thank you...\n(what a strange man...)"
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 1);
	}

	public void startNormalDialog(President _pObj){
		pObj = _pObj;

		DialogSystem.character[] nameString ={
			DialogSystem.character.PRINCIPAL,
			DialogSystem.character.PLAYER
		};
		string[] dialogString = new string[]{
			"Work hard for your exams!",
			"...Thanks"
		};
		//dsObj.startDialog(nameString, dialogString);
		conversation (nameString, dialogString, 2);
	}
	
	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
		if(pObj!=null && id==1) pObj.dialogCallBack();
	}
	
}
