﻿using UnityEngine;
using System.Collections;

public class Multi_ResultPrepare : MonoBehaviour
{
	public AudioClip otherClip;
		public GameObject success;
		public GameObject fail;
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

		yield return new WaitForSeconds (0.65f);
		gameObject.audio.clip = otherClip;
		gameObject.audio.Play ();
		yield return new WaitForSeconds (1.85f);
				if (myFields.stateInfo [(int)Multi_Fields.States.SERVER_SUCCESS]) {
		
						if (Network.isServer) {
			
								Instantiate (success, new Vector3 (0, 0, 0), Quaternion.identity);
						} else
								Instantiate (fail, new Vector3 (0, 0, 0), Quaternion.identity);
				} else {
						if (Network.isClient)
								Instantiate (success, new Vector3 (0, 0, 0), Quaternion.identity);
						else
								Instantiate (fail, new Vector3 (0, 0, 0), Quaternion.identity);		
				}
				Destroy (gameObject);
		}
}