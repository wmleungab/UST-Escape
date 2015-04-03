using UnityEngine;
using System.Collections;

public class MickeyAnimateScript : MonoBehaviour {

	private Animator animator;

	NavMeshAgent navmesh;
	bool faceRight = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		navmesh = transform.parent.transform.Find("MickeyZombie").GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		int direction = animator.GetInteger("Direction");
		if(!faceRight && (direction == 1 || direction == 3)){
			faceRight = true;
			flipHorizontal();
		}
		else if (faceRight && (direction == 2 || direction == 4)) {
			faceRight = false;
			flipHorizontal();
		}
		//Debug.Log(direction + " " + faceRight);
	}
	
	void flipHorizontal() {
		Debug.Log("Mickry Flip!!");
		
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
