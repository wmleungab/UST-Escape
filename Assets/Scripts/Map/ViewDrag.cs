using UnityEngine;
using System.Collections;

public class ViewDrag : MonoBehaviour {
Vector3 hit_position = Vector3.zero;
Vector3 current_position = Vector3.zero;
Vector3 camera_position = Vector3.zero;
float z = 0.0f;

public bool isDragging = false;
private NavMeshAgent navComponent;

	public float mapX = 25.0f;
	public float mapY = 20.0f;
	public Vector2 centerPoint = new Vector2(0,0);
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

// Use this for initialization
void Start () {
	Transform player = GameObject.FindWithTag("Player").transform;
	navComponent = (NavMeshAgent)player.transform.GetComponent ("NavMeshAgent");
	
		float vertExtent = Camera.main.camera.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;
		
		// Calculations assume map is position at the origin
		minX = horzExtent - mapX / 2.0f + centerPoint.x;
		maxX = mapX / 2.0f - horzExtent + centerPoint.x;
		minY = vertExtent - mapY / 2.0f + centerPoint.y;
		maxY = mapY / 2.0f - vertExtent + centerPoint.y;
		
		//Debug.Log("Camera: " + maxX);
		if(maxX < minX) {
			minX = 0.0f;
			maxX = 0.0f;
		}
		if(maxY < minY) {
			minY = 0.0f;
			maxY = 0.0f;
		}
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

	position.x = Mathf.Clamp(position.x, minX, maxX);
	position.z = Mathf.Clamp(position.z, minY, maxY);

    transform.position = position;
}
}