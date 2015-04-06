using UnityEngine;
using System.Collections;

public class DoorDialogInterface : DialogInterface
{

		public void firstTouchingDoor ()
		{

				DialogSystem.character[] nameString = {
						DialogSystem.character.PLAYER,
						DialogSystem.character.PLAYER,
						DialogSystem.character.PLAYER
				};
				string[] dialogString = new string[] {
						"Why I can’t open the door???!!!! Someone locked it?\nLet me call someone help.",
						"No! My phone is not working! Why?!... Calm down, I will be fine.",
						"…… Maybe I can hack into the university system to open the door!\nLet me to use those computers."
				};
				conversation (nameString, dialogString, 888);
		}

		public void subsequentTouchingDoor ()
		{
		
				DialogSystem.character[] nameString = {
						DialogSystem.character.PLAYER,
						DialogSystem.character.PLAYER,
						DialogSystem.character.PLAYER
				};
				string[] dialogString = new string[]{"Let me use those computers to try to hack into UST server!\nShould I use the master computer or the normal ones?"};
				conversation (nameString, dialogString, 999);
		}

		override public void  onDialogFinish (int id, int selection)
		{
				//selection -1: No selection carried out 0; false or no 1: true or yes
				Debug.Log ("DoorDialogInterface: Dialog with id " + id + "has finished with selection " + selection);
		}
}
