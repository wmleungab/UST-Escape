using UnityEngine;
using System.Collections;

public class atriumPlayerSave : MonoBehaviour {

	public Vector3 fromLg2Pos;

	// Use this for initialization
	void Start () {
		if (SaveLoadSystem.getInstance ().atriumSceneStateArr [(int)SaveLoadSystem.AtriumSceneState.FROMLG2]){
			Debug.Log("Player Moving");
			gameObject.SetActive(false);
			gameObject.transform.localPosition = fromLg2Pos;
			gameObject.SetActive(true);
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
