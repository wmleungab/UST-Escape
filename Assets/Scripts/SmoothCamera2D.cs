 using UnityEngine;
 using System.Collections;
 
 public class SmoothCamera2D : MonoBehaviour {
     
     public float dampTime = 0.15f;
     private Vector3 velocity = Vector3.zero;
     private Transform target;
 
	float mapX = 25.0f;
	float mapY = 20.0f;
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;
	private bool isClamp = false;

	void Start() {
		target = GameObject.FindWithTag("Player").transform;
		
		float vertExtent = Camera.main.camera.orthographicSize;    
		float horzExtent = vertExtent * Screen.width / Screen.height;
		
		// Calculations assume map is position at the origin
		if(maxY > Screen.width && maxX > Screen.height) {
			minX = horzExtent - mapX / 2.0f;
			maxX = mapX / 2.0f - horzExtent;
			minY = vertExtent - mapY / 2.0f;
			maxY = mapY / 2.0f - vertExtent;
			isClamp = true;
		}

	}

     // Update is called once per frame
     void Update () 
     {
         if (target)
         {
             Vector3 point = camera.WorldToViewportPoint(target.position);
             Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             Vector3 destination = transform.position + delta;
			 if(isClamp) {
				destination.x = Mathf.Clamp(destination.x, minX, maxX);
				destination.z = Mathf.Clamp(destination.z, minY, maxY);
			 }
			 transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }
     
     }
 }