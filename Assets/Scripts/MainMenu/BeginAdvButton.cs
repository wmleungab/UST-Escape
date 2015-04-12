using UnityEngine;
using System.Collections;

public class BeginAdvButton : MonoBehaviour {

	public string startStageName = "lab_stage";

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		gameObject.audio.Play();
		SaveLoadSystem.getInstance ().resetSave ();
		AutoFade.LoadLevel (startStageName, 0.5f, 0.5f, Color.black);
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
