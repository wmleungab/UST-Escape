using UnityEngine;
using System.Collections;

public class Fries : MonoBehaviour {
	
	public int damage;
	public GameObject explosion;

	public void Explode (){
		playerdamage ();
		Vector3 pos = gameObject.transform.position;
		GameObject exp = Instantiate (explosion, new Vector3 (pos.x-1.2f, pos.y-0.5f, pos.z),Quaternion.identity)as GameObject;
		exp.transform.localScale = new Vector3 (5, 5, 0);

		Destroy (gameObject);
	}

	void playerdamage(){
		GameObject.Find ("Player").GetComponent<HealthBar> ().HP -=damage;
	}
}
