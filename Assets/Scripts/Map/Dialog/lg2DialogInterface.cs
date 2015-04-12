using UnityEngine;
using System.Collections;

public class lg2DialogInterface : DialogInterface {
	
	// Use this for initialization
	void Start () {
		if (!SaveLoadSystem.getInstance ().lg2SceneStateArr [(int)SaveLoadSystem.Lg2SceneState.BEGANDIALOG])
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
			DialogSystem.character.PLAYER,
			DialogSystem.character.PLAYER
		};
		
		string[] dialog = new string[]{
			"Where is here?!",
			"It seems I fall from Atrium to this place... It is so dark here",
			"Oh, I grap the banner from the students just now\n Let me burn it to light up the place"
		};
		
		
		conversation (cArr, dialog, 1);
	}
	
	override public void  onDialogFinish (int id, int selection)
	{
		//selection -1: No selection carried out 0; false or no 1: true or yes
		Debug.Log ("Dialog with id " + id + "has finished with selection result " + selection);
		if (id == 1) {
			SaveLoadSystem.getInstance ().lg2SceneStateArr [(int)SaveLoadSystem.Lg2SceneState.BEGANDIALOG] = true;
			SaveLoadSystem.getInstance ().save ();
			StartCoroutine(GetComponent<CreateItem>().giveItemToPlayer("banner"));
			//Destroy (this.gameObject);
		}
	}
	
	
}
