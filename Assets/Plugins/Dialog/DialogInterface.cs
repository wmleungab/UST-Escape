using UnityEngine;
using System.Collections;

public abstract class DialogInterface : MonoBehaviour {

	DialogSystem dsObj;

	public void conversation(DialogSystem.character[] c, string[] dialogue, int id){
		dsObj = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();
		dsObj.startDialogs (c, dialogue,this, id);
		}


	//This function display only one statement talked by one character only
	//And provide options for player to choose
	public void optionSelect(DialogSystem.character c, string dialogue, int id){
		dsObj = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();
		dsObj.startOptionDialog (c, dialogue,this, id);
	}

	//selection -1: No selection carried out 0; false or no 1: true or yes
	abstract public void onDialogFinish(int id, int selection);

}
