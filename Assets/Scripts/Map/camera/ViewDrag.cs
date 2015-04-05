using UnityEngine;
using System.Collections;

public class ViewDrag : MonoBehaviour {
	Vector3 hit_position = Vector3.zero;
	Vector3 current_position = Vector3.zero;
	Vector3 camera_position = Vector3.zero;

	public bool isDragging = false;
	private NavMeshAgent navComponent;

	private ClampCamera clampCamera;

	// Use this for initialization
	void Start () {
		Transform player = GameObject.FindWithTag("Player").transform;
		navComponent = (NavMeshAgent)player.transform.GetComponent ("NavMeshAgent");
		clampCamera = (ClampCamera)GetComponent ("ClampCamera");

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.S)){
			isDragging = true;
			navComponent.enabled = false;
		}
		if(Input.GetKeyUp(KeyCode.S)){
			isDragging = false;
			navComponent.enabled = true;
		}
		if (isDragging) {
			if(Input.GetMouseButtonDown(0)){
				hit_position = Input.mousePosition;
				camera_position = transform.position;

			}
			if(Input.GetMouseButton(0)){
				current_position = Input.mousePosition;
				LeftMouseDrag();        
			}
		}
	}

	void LeftMouseDrag(){
	    // From the Unity3D docs: "The z position is in world units from the camera."  In my case I'm using the y-axis as height
	    // with my camera facing back down the y-axis.  You can ignore this when the camera is orthograhic.
	    current_position.z = hit_position.z = camera_position.y;

	    // Get direction of movement.  (Note: Don't normalize, the magnitude of change is going to be Vector3.Distance(current_position-hit_position)
	    // anyways.  
	    Vector3 direction = Camera.main.ScreenToWorldPoint(current_position) - Camera.main.ScreenToWorldPoint(hit_position);

	    // Invert direction to that terrain appears to move with the mouse.
	    direction = direction * -1;
		
	    Vector3 position = camera_position + direction;

		if (clampCamera!=null) position = clampCamera.clampDes(position);

	    transform.position = position;
	}
}