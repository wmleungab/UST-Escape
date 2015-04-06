using UnityEngine;
using System.Collections;

public abstract class DialogInterface : MonoBehaviour
{

		DialogSystem dsObj;
		//If there are 3 people in a conservation. Main Character will not appear in any picture.
		public void conversation (DialogSystem.character[] c, string[] dialogue, int id)
		{
				dsObj = GameObject.Find ("DialogSystem").GetComponent<DialogSystem> ();
				dsObj.startDialogs (c, dialogue, this, id);
		}


		//This function display only one statement talked by one character only
		//And provide options for player to choose
		public void optionSelect (DialogSystem.character c, string dialogue, int id)
		{
				dsObj = GameObject.Find ("DialogSystem").GetComponent<DialogSystem> ();
				dsObj.startOptionDialog (c, dialogue, this, id);
		}

		//This function allows you to show a big picture (eg. ascII table) on the dialog system
		//No character pitcures will not shown no matter what you input in "DialogSystem.character c"
		//However, you can still provide the character who is speaking and its content
		public void showBigIcon (DialogSystem.character[] c, string[] dialogue, int id, Sprite s)
		{
				dsObj = GameObject.Find ("DialogSystem").GetComponent<DialogSystem> ();
				dsObj.startShowBigIcon (c, dialogue, this, id, s);
		}

		//selection -1: No selection carried out 0; false or no 1: true or yes
		abstract public void onDialogFinish (int id, int selection);

}
