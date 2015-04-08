using UnityEngine;
using System.Collections;

public abstract class  SaveLoadSystemInterface : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//parameter is the scene that saved by sl system
	public abstract void onSaveComplete(SaveLoadSystem.SceneType x);

	//parameter is the scene that loaded by sl system
	public abstract void onLoadComplete(SaveLoadSystem.SceneType x);
}
