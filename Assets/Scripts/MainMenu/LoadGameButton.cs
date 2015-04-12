using UnityEngine;
using System.Collections;

public class LoadGameButton : MonoBehaviour {

	void OnMouseUp(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		gameObject.audio.Play();
		SaveLoadSystem.getInstance ().load();
		if (SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.LAB) {
			Debug.Log ("MainMenu: Loading Lab scene");
			AutoFade.LoadLevel ("lab_stage", 0.5f, 0.5f, Color.black);
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.ATRIUM) {
			AutoFade.LoadLevel ("atrium_stage", 0.5f, 0.5f, Color.black);
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.LG2) {
			AutoFade.LoadLevel ("LG2_stage", 0.5f, 0.5f, Color.black);
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.SUNDIAL) {
			AutoFade.LoadLevel ("sundial", 0.5f, 0.5f, Color.black);
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.UNDERSUN1) {
			AutoFade.LoadLevel ("SecretChamberBefore", 0.5f, 0.5f, Color.black);
		}else if(SaveLoadSystem.getInstance ().currentSceneType==SaveLoadSystem.SceneType.UNDERSUN2) {
			AutoFade.LoadLevel ("SecretChamberAfter", 0.5f, 0.5f, Color.black);
		}
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
