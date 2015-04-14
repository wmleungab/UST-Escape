using UnityEngine;
using System.Collections;

public class ItemDialogInterface : DialogInterface {

	static int dialogID = 0;
	public static bool showDialog = true;

	void getItemDialog(){
		if (showDialog) {
						string temp = "Item " + this.name + " x1 get";
						DialogSystem.character[] nameString = {DialogSystem.character.SYSTEM};
						string[] dialogString = new string[]{temp};
						//dsObj.startDialog(nameString, dialogString);
						conversation (nameString, dialogString, ++dialogID);
				}
	}
	void dropItemDialog(){
		string temp = "Item " + this.name + " x1 dropped";
		DialogSystem.character[] nameString ={DialogSystem.character.SYSTEM};
		string[] dialogString = new string[]{temp};
		conversation (nameString, dialogString, ++dialogID);
	}

	override public void  onDialogFinish(int id, int selection){
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finish");
	}

}
