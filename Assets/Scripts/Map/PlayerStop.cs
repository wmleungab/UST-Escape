using UnityEngine;
using System.Collections;

public class PlayerStop : MonoBehaviour {

	private NavMeshAgent playerNavMesh;

	// Use this for initialization
	void Start () {
		playerNavMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void stopWalking () {
		playerNavMesh.enabled = false;
	}
	
	void continueWalking () {
		playerNavMesh.enabled = true;
	}
}
