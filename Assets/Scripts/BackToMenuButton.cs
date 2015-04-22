using UnityEngine;
using System.Collections;

public class BackToMenuButton : MonoBehaviour {

	
	void OnMouseUp(){
		SaveLoadSystem.getInstance ().resetSave ();
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 1f);
		AutoFade.LoadLevel("mainmenu",0.5f,0.5f,Color.black);
	}
	void OnMouseDown(){
		renderer.material.color = new Color (renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);
	}
}
