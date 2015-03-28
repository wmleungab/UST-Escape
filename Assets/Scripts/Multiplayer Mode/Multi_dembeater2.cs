using UnityEngine;
using System.Collections;

public class Multi_dembeater2 : MonoBehaviour
{
		public GameObject explosion;
		private Multi_Fields myFields;
		public bool startFlag = false;
		public bool dieFlag = true;
		public float readyTime;

		float time;
		Animator anim;
		Vector3 pos;
	
		// Use this for initialization
		void Start ()
		{
		startFlag = false;
				name = "Enemy";
				myFields = GameObject.Find ("SharedData").GetComponent<Multi_Fields> ();
				anim = GetComponent<Animator> ();
				pos = gameObject.transform.position;
		}
	
		void  	init ()
		{
		}
	
		void readyTofight ()
		{
				time = readyTime;
				anim.SetTrigger ("ReadyToFight");
				InvokeRepeating ("counting", 1, 1f);

		}
	
		void counting ()
		{
		
				time--;
				if (time == 0) {
						CancelInvoke ("counting");
						fight ();
				}
		}
	
		void fight ()
		{
				anim.SetTrigger ("ToFight");
				GameObject exp = Instantiate (explosion, new Vector3 (pos.x - 0.2f, pos.y - 2.5f, pos.z), Quaternion.identity)as GameObject;
				exp.transform.localScale = new Vector3 (1.5f, 1.5f, 1);
				GameObject exp2 = Instantiate (explosion, new Vector3 (pos.x, pos.y - 2.6f, pos.z), Quaternion.identity)as GameObject;
				exp2.transform.localScale = new Vector3 (1.5f, 1.5f, 1);
		}

		void invokeReadyToFight ()
		{
				/*if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
			float r = Random.value;
			Invoke ("readyTofight", frequency * (min + factor * r));
		}*/
		}
	
		void Update ()
		{
	
				if (startFlag) {
						readyTofight ();
						
				}	
				/*if (GetComponent<Multi_HealthBar> ().HP <= 0 && dieFlag) {
				die ();
				dieFlag = false;
			}*/
		}

		void playerdamage ()
		{
				//GameObject.Find ("Player").GetComponent<HealthBar> ().HP -= damage;
		startFlag = false;
		}
	
		void die ()
		{
				StartCoroutine ("dieAnim");
		}
	
		IEnumerator dieAnim ()
		{
				Renderer [] component = gameObject.GetComponentsInChildren<Renderer> ();
		
				for (float i=0; i<=1f; i+=0.05f) {
						foreach (Renderer r in component) {
								r.material.color = new Vector4 (r.material.color.r, r.material.color.g, r.material.color.b, 1 - i);
								yield return new WaitForSeconds (0.01f);
						}
				}
		
		
				yield return new WaitForSeconds (1f);
				Destroy (gameObject);
		}
}
