using UnityEngine;
using System.Collections;

public class DialogBtn : MonoBehaviour {
	DialogSystem parent;

	// Use this for initialization
	void Start () {
		parent = GameObject.Find("DialogSystem").GetComponent<DialogSystem> () as DialogSystem;
	}

	void OnMouseUp(){

		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		parent.onChildrenTouched (gameObject.name);
			
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}

}
