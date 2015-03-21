using UnityEngine;
using System.Collections;

public class Multi_StarterScript : MonoBehaviour
{
	
		public GameObject Ready;
		public GameObject Go;
		private bool done = false;
		private Multi_Fields myFields;
		// Use this for initialization
		void Update ()
		{
		if (myFields.stateInfo [(int)Multi_Fields.States.CLIENT_READY_TO_START] && myFields.stateInfo [(int)Multi_Fields.States.SERVER_READY_TO_START] && !done) {
						StartCoroutine ("startanim");
						
						done = true;
				}
		}

		void Start ()
		{
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 0);
				Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 0);
		}

		IEnumerator startanim ()
		{

				float ReadyYPos = Ready.transform.position.y;

				for (float i=0; i<=1; i+=0.1f) {
						Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, i);
						Ready.transform.position = new Vector3 (Ready.transform.position.x, ReadyYPos - (1 - i), Ready.transform.position.z);
			
						yield return new WaitForSeconds (0.01f);
				}
				yield return new WaitForSeconds (1.5f);
				for (float i=0; i<=1; i+=0.05f) {
						Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 1 - i);
						Ready.transform.position = new Vector3 (Ready.transform.position.x, ReadyYPos + i, Ready.transform.position.z);
			
						Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, i);
						Go.transform.position = new Vector3 (Go.transform.position.x, ReadyYPos - (1 - i), Go.transform.position.z);
			
						yield return new WaitForSeconds (0.01f);
				}
				Ready.GetComponent<SpriteRenderer> ().sprite = null;
				yield return new WaitForSeconds (1.5f);
				for (float i=0; i<=1; i+=0.05f) {
						Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 1 - i);
						Go.transform.position = new Vector3 (Go.transform.position.x, ReadyYPos + i, Go.transform.position.z);
			
						yield return new WaitForSeconds (0.01f);
				}
				Go.GetComponent<SpriteRenderer> ().sprite = null;
			//myFields.syncState(Multi_Fields.States.ROUND_STARTS,true);
		myFields.changeState(Multi_Fields.States.ROUND_STARTS , true);
		myFields.changeState(Multi_Fields.States.ATTACK_ROUND , true);
		if (Network.isServer)
						myFields.syncQTEMode (Random.Range (0, 2));
		}
}
