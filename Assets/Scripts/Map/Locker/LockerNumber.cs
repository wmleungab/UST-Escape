using UnityEngine;
using System.Collections;

public class LockerNumber : MonoBehaviour {
	LockerScript parent;

	// Use this for initialization
	void Start () {
		parent = gameObject.transform.parent.gameObject.GetComponent<LockerScript> () as LockerScript;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp(){

		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		parent.onChildrenTouched (gameObject.name);
			
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
