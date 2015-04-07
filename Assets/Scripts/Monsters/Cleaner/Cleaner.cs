using UnityEngine;
using System.Collections;

public class Cleaner : MonoBehaviour
{
	public AudioClip warningsound;
	public AudioClip hitsound;
	public AudioClip defendsound;
	public GameObject defendSheildpf;
	
	GameObject defendSheild;
	public bool defendState=false;
	public float defendmin=2;
	public float defendfactor=2;
	public float defendfrequency=1;
	public float defendlength=2;
	public float defendTimer;
	public float sheildsize=0.5f;
	
	public GameObject prompt;

		public bool startFlag = true;
		public bool dieFlag = true;
		public float readyTime;
		public float min;
		public  float factor;
		public  float frequency;
		public float threshold;
		public int damage;
		float time;
		Animator anim;
		Vector3 mouseStart;
		Vector3 mouseCurrent;
		Vector3 ms;
		Vector3 mc;
		bool allowSlide = false;
	
	public float lastAttack=0;
	public float canAttackRange=0.5f;

		// Use this for initialization
		void Start ()
		{

		}

		void  	init ()
		{
				anim = GetComponent<Animator> ();
				invokeReadyToFight ();
		
				mouseStart = Vector3.zero;
				mouseCurrent = Vector3.zero;
				allowSlide = false;

		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
		}

	void Defend(){
		defendSheild = Instantiate (defendSheildpf, new Vector3 (transform.position.x , transform.position.y, transform.position.z-1),Quaternion.identity)as GameObject;
		defendSheild.transform.localScale = new Vector3 (sheildsize, sheildsize, 1);
		defendSheild.transform.parent = gameObject.transform;
		defendState = true;
		Invoke ("DefendDisappear", defendlength);
	}
	
	void DefendDisappear(){
		Destroy (defendSheild);
		defendState = false;
		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}

		void readyTofight ()
		{
		GameObject p = Instantiate (prompt, new Vector3 (transform.position.x+0.3f , transform.position.y+3.7f, transform.position.z-1),Quaternion.identity)as GameObject;

		time = readyTime;
		AudioSource.PlayClipAtPoint (warningsound, gameObject.transform.position);
				anim.SetTrigger ("ReadyToFight");
				InvokeRepeating ("counting", 1, 1f);
				allowSlide = true;
				mouseStart = Vector3.zero;
				mouseCurrent = Vector3.zero;

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
		AudioSource.PlayClipAtPoint (hitsound, gameObject.transform.position);
				anim.SetTrigger ("ToFight");
				resetVar ();
		}

		void invokeReadyToFight ()
		{
				if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
						float r = Random.value;
						Invoke ("readyTofight", frequency * (min + factor * r));
				}
		}
		// Update is called once per frame
		void defendSucceed ()
		{
				anim.SetTrigger ("slideSucceed");
				resetVar ();
				CancelInvoke ("counting");
				invokeReadyToFight ();

		}

		void resetVar ()
		{
				allowSlide = false;
				mouseStart = Vector3.zero;
				mouseCurrent = Vector3.zero;
		}

		void Update ()
		{
		
				//cursor
				if (allowSlide) {
						if (Input.touchCount > 0) {
								if (Input.GetTouch (0).phase == TouchPhase.Began) {	
										mouseStart = Input.GetTouch (0).position;
										mouseCurrent = Input.GetTouch (0).position;
								} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {	
										mouseCurrent = Input.GetTouch (0).position;
								} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {	
										mouseCurrent = Input.GetTouch (0).position;
								}
						}
		
						//transfering screen to world
						ms = Camera.main.ScreenToWorldPoint (new Vector3 (mouseStart.x, mouseStart.y, 0));
						mc = Camera.main.ScreenToWorldPoint (new Vector3 (mouseCurrent.x, mouseCurrent.y, 0));
		
		
		
						if (Vector3.Distance (mc, ms) > threshold) {
								if (Mathf.Abs (ms.x - gameObject.transform.position.x) < 1.5f && Mathf.Abs (ms.y - gameObject.transform.position.y) < 2f)
										defendSucceed ();
						}
				}
				if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
						if (startFlag) {
								init ();
								startFlag = false;
						}

						if (GetComponent<HealthBar> ().HP <= 0 && dieFlag) {
								die ();
								dieFlag = false;
						}
				}


	

		}

		void OnMouseDown ()
		{

		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag&& !defendState && Time.time-lastAttack>canAttackRange) {

			gameObject.GetComponent<HealthBar> ().HP--;
			audio.Play();
			lastAttack=Time.time;
				}
		if (defendState) {
			AudioSource.PlayClipAtPoint (defendsound, transform.position);
		}

		}

		void playerdamage ()
		{
				GameObject.Find ("Player").GetComponent<HealthBar> ().HP -= damage;
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
