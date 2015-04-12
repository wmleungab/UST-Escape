using UnityEngine;
using System.Collections;

public class AtriumDialogInterface : DialogInterface {

	// Use this for initialization
	void Start () {
		if (!SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.BEGANDIALOG])
			StartCoroutine (startBeginningDialog());
		else {
			Destroy (this.gameObject);
		}
	}
	
	IEnumerator  startBeginningDialog ()
	{
		yield return new WaitForSeconds (0.3f);
		
		DialogSystem.character[] cArr = {
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"ha...ha..\n Finally get away from that crazy cleaning lady",
			"Let's go back home and sleep"
		};
		
		
		conversation (cArr, dialog, 1);
	}

	override public void  onDialogFinish (int id, int selection)
	{
			//selection -1: No selection carried out 0; false or no 1: true or yes
			Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
			if (id == 1) {
					SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.BEGANDIALOG] = true;
					SaveLoadSystem.getInstance ().save ();
					GameObject president = GameObject.Find ("president");
					if (president) {
							president.SendMessage ("colliderOnClick");
					}
				Destroy (this.gameObject);
			}
	}


}
