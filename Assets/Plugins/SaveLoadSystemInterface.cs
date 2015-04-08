using UnityEngine;
using System.Collections;

public abstract class  SaveLoadSystemInterface : MonoBehaviour {

	/*
	 * Save with call back example

			GameObject.Find("SLSystem").GetComponent<SaveLoadSystem>().
			saveWithCallBack(this);

	 */

	//parameter is the scene that saved by sl system
	public abstract void onSaveComplete(SaveLoadSystem.SceneType x);


}
