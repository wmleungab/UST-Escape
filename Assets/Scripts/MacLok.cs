using UnityEngine;
using System.Collections;

public class MacLok : MonoBehaviour {
	public int damage;
	public GameObject  exp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void destroyMacLok (){
		
		playerdamage ();
		Instantiate (exp, new Vector3 (transform.position.x, transform.position.y-0.7f, -50),Quaternion.identity);
		Destroy (transform.parent.gameObject);

	}
	
	void playerdamage(){
		GameObject.Find ("Player").GetComponent<HealthBar> ().HP -=damage;
	}
}
