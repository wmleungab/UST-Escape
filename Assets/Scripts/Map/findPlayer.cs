using UnityEngine;
using System.Collections;

public class findPlayer : MonoBehaviour {

	private GameObject victim;

	// Use this for initialization
	void Start () {
		victim = GameObject.FindWithTag("Player");
		if (victim != null) {
			Debug.Log ("victim found");
			GameObject.Find("Inventory").SendMessage ("toBattleMode");
			
			NavMeshAgent navComponent = (NavMeshAgent)victim.transform.GetComponent ("NavMeshAgent");
			navComponent.enabled = false;
		}
		else 
			Debug.Log ("victim not found");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
