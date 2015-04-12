using UnityEngine;
using System.Collections;

public class FreeMemory : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Resources.UnloadUnusedAssets ();
	}

}
