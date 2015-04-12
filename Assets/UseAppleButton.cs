using UnityEngine;
using System.Collections;

public class UseAppleButton : MonoBehaviour {

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		//gameObject.SendMessage ("findKey", "greenapple");
	}

	void findKeyCallback(Transform item){
		if (item != null) {
			gameObject.SendMessage("removeItem","greenapple");
			AutoFade.LoadLevel("battle",0.5f,0.5f,Color.black);
		}
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
