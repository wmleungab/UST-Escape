using UnityEngine;
using System.Collections;

public class LoadGameButton : MonoBehaviour {

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		gameObject.audio.Play();
		SaveLoadSystem.getInstance ().load();
		if (SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.LAB) {
			Debug.Log ("MainMenu: Loading Lab scene");
			Application.LoadLevel ("lab_stage");
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.ATRIUM) {
			Application.LoadLevel ("atrium_stage"); 
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.LG2) {
			Application.LoadLevel ("LG2_stage"); 
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.SUNDIAL) {
			Application.LoadLevel ("sundial"); 
		}
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
