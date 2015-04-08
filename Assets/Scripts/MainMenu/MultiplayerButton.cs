using UnityEngine;
using System.Collections;

public class MultiplayerButton : MonoBehaviour {

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		gameObject.audio.Play();
		Application.LoadLevel ("multiplayerBattle"); 
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
