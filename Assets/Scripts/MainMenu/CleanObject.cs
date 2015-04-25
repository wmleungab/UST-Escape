using UnityEngine;
using System.Collections;

public class CleanObject : MonoBehaviour {
	bool done =false;
	// Use this for initialization
	void Start () {
		GamePause.continueGame ();
	}
	
	// Update is called once per frame
	void Update () {
	if (!done) {

			if(GameObject.FindGameObjectWithTag("Player")!=null)Destroy(GameObject.FindGameObjectWithTag("Player"));
			if(GameObject.Find("Dynamic")!=null)Destroy(GameObject.Find("Dynamic"));
			if(GameObject.Find("AutoFade")!=null)Destroy(GameObject.Find("AutoFade"));
			done=true;
			}
	}
}
