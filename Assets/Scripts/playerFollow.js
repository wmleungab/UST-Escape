#pragma strict

private var navComponent : NavMeshAgent;

function Start () {

	navComponent = this.transform.GetComponent(NavMeshAgent);

}

function Update () {

		if(Input.GetMouseButton(0) && navComponent.enabled) {
			//Debug.Log("Pressed left click.");

			var targetPos = Input.mousePosition;
			targetPos.z = Camera.main.transform.position.y;  //camera to floor value
			targetPos = Camera.main.ScreenToWorldPoint(targetPos);
			//Debug.Log(transform.position + " " + targetPos);
			
			navComponent.SetDestination(targetPos);
		}

}