using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public static Player _instance;

	// Use this for initialization
	void Awake () {
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else if (_instance != this) {
			Destroy(gameObject);
		}
	}
	
	public static Player getInstance ()
	{
			return _instance;
	}

	
}
