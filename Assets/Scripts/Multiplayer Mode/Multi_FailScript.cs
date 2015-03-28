using UnityEngine;
using System.Collections;

public class Multi_FailScript : MonoBehaviour {
	public Multi_Fields myFields;
	// Use this for initialization
	void Start () {
		myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
		StartCoroutine ("startanim");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator startanim ()
	{	yield return new WaitForSeconds (2.75f);
		myFields.changeState (Multi_Fields.States.ROUND_STARTS, true);
		yield return new WaitForSeconds (0.25f);
		myFields.changeState (Multi_Fields.States.ATTACK_ANI_READY, true);

		Destroy (gameObject);
	}
	
}
