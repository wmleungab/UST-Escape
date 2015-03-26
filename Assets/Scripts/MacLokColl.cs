using UnityEngine;
using System.Collections;

public class MacLokColl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		Cut ();
	}
	
	public void Cut (){
		Destroy (transform.parent.transform.parent.gameObject);
		//	StartCoroutine ("Disappearing");
	}
}
