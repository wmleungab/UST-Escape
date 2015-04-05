using UnityEngine;
using System.Collections;

public class ChangeSceneDy : MonoBehaviour {

	public static ChangeSceneDy control;

	// Use this for initialization
	void Awake () {
		if(control == null) {
			DontDestroyOnLoad(gameObject);
			control = this;
		}
		else if (control != this) {
			Destroy(gameObject);
		}
	}
}
