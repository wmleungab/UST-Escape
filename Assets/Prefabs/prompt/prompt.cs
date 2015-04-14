using UnityEngine;
using System.Collections;

public class prompt : MonoBehaviour {

	// Use this for initialization

	void Start(){
		Invoke ("destroyprompt", 2.5f);
	}
	void destroyprompt(){
		Destroy (gameObject);
	}
}
