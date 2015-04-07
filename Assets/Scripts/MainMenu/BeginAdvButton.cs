using UnityEngine;
using System.Collections;

public class BeginAdvButton : MonoBehaviour {

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		SaveLoadSystem.getInstance ().resetSave ();
		Application.LoadLevel ("lab_stage"); 
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
