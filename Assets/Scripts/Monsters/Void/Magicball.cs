using UnityEngine;
using System.Collections;

public class Magicball : MonoBehaviour {
	public AudioClip magicsound;

	public GameObject explosion;
	public GameObject parentMon;

	public float threshold;
	public int fingTimeThreshold;

	bool allowFing=true;
	int fingFrameTime=0;

	
	public int damage;
	// Use this for initialization
	void Start () {
		allowFing=true;
		fingFrameTime=0;
		AudioSource.PlayClipAtPoint (magicsound, gameObject.transform.position);
	}

	// Update is called once per frame
	void Update () {


		if(allowFing){
			if (Vector3.Distance (Vector3.zero, Input.acceleration) > threshold)
				fingFrameTime++;
		
			if (fingFrameTime > fingTimeThreshold){
	
				magicballDisappear ();
				allowFing=false;
			}
	
		}


	}

	void magicballDisappear(){
		transform.parent.gameObject.GetComponent<Void> ().invokeAttack(); //call the parent that the magic ball is finished and invoke attack

		Destroy (gameObject);
	}

	void attack(){
		StartCoroutine ("exp1");
		playerdamage ();
	}

	IEnumerator exp1(){
		Vector3 offset=new Vector3(0,-2.2f,0);
		Vector3 range=new Vector3(5f,1f,0);
	Vector3 pos = gameObject.transform.position;
		for (int i=0; i<3; i++) {
			GameObject exp = Instantiate (explosion, new Vector3 (offset.x+pos.x +(Random.value-0.5f)*range.x, offset.y+pos.y +(Random.value-0.5f)*range.y, pos.z), Quaternion.identity)as GameObject;
			exp.transform.localScale = new Vector3 (5, 5, 0);

			yield return new WaitForSeconds (0.06f);
				}


		}

	void toAllowFing(){
		allowFing = true;
		fingFrameTime=0;
	}
	void toNotAllowFing(){
		allowFing = false;
	}

	void playerdamage(){
		GameObject.Find ("Player").GetComponent<HealthBar> ().HP -=damage;
	}


	void ToDestroy(){
		Destroy (gameObject);
		parentMon.gameObject.GetComponent<Void> ().invokeAttack(); //call the parent that the magic ball is finished and invoke attack
	}
	void OnGUI(){
		GUILayout.Label ("fingFrameTime:" + fingFrameTime + "\n"
		                 +"d:"+allowFing);
	}
}
