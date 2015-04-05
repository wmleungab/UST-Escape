using UnityEngine;
using System.Collections;

public abstract class DialogInterface : MonoBehaviour {

	DialogSystem dsObj;

	public void conversation(DialogSystem.character[] c, string[] dialogue, int id, bool selection){
		dsObj = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();
		dsObj.startDialog (c, dialogue,this, id, selection);
		}

	//selection -1: No selection carried out 0; false or no 1: true or yes
	abstract public void onDialogFinish(int id, int selection);

}
