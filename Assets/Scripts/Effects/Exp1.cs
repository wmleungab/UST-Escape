using UnityEngine;
using System.Collections;

public class Exp1 : MonoBehaviour {

	void Start(){

		audio.Play ();
	}
	public void Destroy (){
		Destroy (gameObject);
	}

}
