using UnityEngine;
using System.Collections;

public class Voidermor : MonoBehaviour {

	// Use this for initialization
	void gotoBattle () {

		Debug.Log ("changeScene called");
		GetComponent<sceneInterface> ().changeScene ("sundial");
	
	}

}
