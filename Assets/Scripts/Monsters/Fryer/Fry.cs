using UnityEngine;
using System.Collections;

public class Fry : MonoBehaviour {
	public GameObject parent;

	void Start(){
	}
	void OnMouseDown(){
		Cut ();
	}

	public void Cut (){
		Destroy (parent);
		audio.Play ();
	//	StartCoroutine ("Disappearing");
			}


	IEnumerator Disappearing ()
	{
		for (float i=0; i<=1; i+=0.1f) {
			renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.b, renderer.material.color.g, 1 - i);
			yield return new WaitForSeconds (0.01f);
		}
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.b, renderer.material.color.g, 0);
		Destroy (gameObject);
	}

	

}
