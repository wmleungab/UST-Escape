using UnityEngine;
using System.Collections;

public class boxa : MonoBehaviour {
	
	public int damage;
	public GameObject  exp;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void destroyBox (){
		playerdamage ();
	GameObject s= Instantiate (exp, new Vector3 (transform.position.x, transform.position.y, -50),Quaternion.identity) as GameObject;
		s.transform.localScale = new Vector3 (5, 5, 1);
		Destroy (transform.parent.gameObject);
	}

	void playerdamage(){
		GameObject.Find ("Player").GetComponent<HealthBar> ().HP -=damage;
	}
}
