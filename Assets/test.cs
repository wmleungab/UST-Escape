using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (GetComponent<CreateItem>().giveItemToPlayer ("salad"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
