using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("changeScene", 6f);
	}

	void changeScene(){
		AutoFade.LoadLevel ("mainmenu", 1f, 1f, Color.black);

	}
	// Update is called once per frame
	void Update () {
	
	}
}
