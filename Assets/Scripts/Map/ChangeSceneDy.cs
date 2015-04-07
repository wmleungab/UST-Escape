using UnityEngine;
using System.Collections;

public class ChangeSceneDy : MonoBehaviour {

	public static ChangeSceneDy control;

	// Use this for initialization
	void Awake () {
		if(control == null) {
			control = this;
		}
		else if (control != this && control.gameObject.name == this.gameObject.name) {
			Destroy(gameObject);
		}
	}
}
