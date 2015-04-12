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
			Destroy(GameObject.FindGameObjectWithTag("Player"));
			Destroy(GameObject.Find("Dynamic"));
			done=true;
				}
	}
}
