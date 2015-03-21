using UnityEngine;
using System.Collections;

public class Fryer : MonoBehaviour {
	public GameObject defendSheildpf;
	
	 GameObject defendSheild;
	public bool defendState=false;
	public float defendmin=2;
	public float defendfactor=2;
	public float defendfrequency=1;
	public float defendlength=2;
	public float defendTimer;
	public float sheildsize=0.5f;

	public bool startFlag=true;
	public bool dieFlag=true;

	public GameObject fry;

	public bool attacking=true;
	public float frequency=1f;
	public float min=2f;
	public float factor=4f;
	Vector3 pos;


	// Use this for initialization
	void Start () {
	
	}
	void init(){
		pos = gameObject.transform.position;
		
		invokeAttack ();

		float ran = Random.value;
		Invoke ("Defend", defendfrequency * (defendmin + ran * defendfactor));
	}

	void Defend(){
		 defendSheild = Instantiate (defendSheildpf, new Vector3 (pos.x , pos.y, pos.z-1),Quaternion.identity)as GameObject;
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

	void Attack(){
		GameObject s = Instantiate (fry, new Vector3 (pos.x + 0.43f, pos.y + 0.78f, pos.z),Quaternion.identity)as GameObject;
		s.transform.parent = 	GameObject.Find("Weapons").transform;
		//continue attack
		if (attacking) {
			invokeAttack();
				}

	}
	void invokeAttack(){
		
		if (dieFlag && BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING) {
						float ran = Random.value;
						Invoke ("Attack", frequency * (min + ran * factor));
				}
	}

	void Update(){
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING ) {
			if(startFlag)			{
			init ();
			startFlag=false;

			}
			if (GetComponent<HealthBar> ().HP <= 0 && dieFlag) {
				die();
				dieFlag=false;
			}
				}
	}

	void OnMouseDown(){
		if (BattleController.currentBattleState == BattleState.BATTLE_PROGRESSING && dieFlag && !defendState) {
						gameObject.GetComponent<HealthBar> ().HP--;
		}


	}

	void die()
	{
		StartCoroutine ("dieAnim");
	}
	
	IEnumerator dieAnim ()
	{
		Renderer [] component = gameObject.GetComponentsInChildren<Renderer>();
		
		for (float i=0; i<=1f; i+=0.05f) {
		foreach (Renderer r in component) {
				r.material.color = new Vector4 (r.material.color.r, r.material.color.g, r.material.color.b, 1-i);
				yield return new WaitForSeconds (0.01f);}
		}


		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}

}
