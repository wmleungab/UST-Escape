using UnityEngine;
using System.Collections;

public class Exp1 : MonoBehaviour {
	
	public AudioClip hitsound;
	void Start(){
		
		AudioSource.PlayClipAtPoint (hitsound, gameObject.transform.position);
	}
	public void Destroy (){
		Destroy (gameObject);
	}

}
