 using UnityEngine;
 using System.Collections;
 
 public class SmoothCamera2D : MonoBehaviour {
     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     private Transform target;
 
	public float mapX = 25.0f;
	public float mapY = 20.0f;
	public Vector2 centerPoint = new Vector2(0,0);
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private ViewDrag viewDrag;

	void Start() {
		target = GameObject.FindWithTag("Player").transform;
		
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
		
		viewDrag = (ViewDrag)GetComponent("ViewDrag");

	}

     // Update is called once per frame
     void Update () 
     {
		 if (viewDrag == null || (viewDrag && !viewDrag.isDragging)) {
			 if (target)
			 {
				 Vector3 point = camera.WorldToViewportPoint(target.position);
				 Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				 Vector3 destination = transform.position + delta;
				 destination.x = Mathf.Clamp(destination.x, minX, maxX);
				 destination.z = Mathf.Clamp(destination.z, minY, maxY);
				 transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			 }
		 }
     }
 }