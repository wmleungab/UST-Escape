using UnityEngine;
using System.Collections;

public class StopNavMesh : MonoBehaviour {

	private NavMeshAgent navMeshComponent;
	private bool isPause = false;

	// Use this for initialization
	void Start () {
		navMeshComponent = GetComponent<NavMeshAgent>();
	}
	
	void Update () {
		if(GlobalVal.GamePause && !isPause){
			isPause = true;
			stopWalking();
		}
		else if (!GlobalVal.GamePause && isPause){
			isPause = false;
			resumeWalking();
		}
	}
	
	// Update is called once per frame
	void stopWalking () {
		navMeshComponent.enabled = false;
	}
	
	void resumeWalking () {
		navMeshComponent.enabled = true;
	}
}
