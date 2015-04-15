using UnityEngine;
using System.Collections;

public class MickeyGroup : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (SaveLoadSystem.getInstance ().lg2SceneStateArr [(int)SaveLoadSystem.Lg2SceneState.SAVEPRED]){
			Destroy (this.gameObject);
		}
	}

	
	void groupBattle(){
		GetComponent<sceneInterface>().gotoBattle();
		Destroy(this.gameObject);
		SaveLoadSystem.getInstance ().lg2SceneStateArr [(int)SaveLoadSystem.Lg2SceneState.BEGANDIALOG] = true;
		SaveLoadSystem.getInstance ().save ();
	}
	
}
