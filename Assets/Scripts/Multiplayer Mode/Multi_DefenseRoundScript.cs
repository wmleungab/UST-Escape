using UnityEngine;
using System.Collections;

public class Multi_DefenseRoundScript : MonoBehaviour
{
	public Multi_Fields myFields;
	
	// Use this for initialization
	void Start ()
	{
		myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
		StartCoroutine ("startanim");
		
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		
		
	}
	
	IEnumerator startanim ()
	{
		
		
		yield return new WaitForSeconds (1.5f);
		//Round start
	
		if(Network.isClient)
			myFields.syncState (Multi_Fields.States.CLIENT_READY_TO_START_ROUND, true);
		else if(Network.isServer) myFields.syncState (Multi_Fields.States.SERVER_READY_TO_START_ROUND, true);
		Destroy (gameObject);
	}
}
