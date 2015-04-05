using UnityEngine;
using System.Collections;

public class ClampCamera : MonoBehaviour {

	public float mapX = 25.0f;
	public float mapY = 20.0f;
	public Vector2 centerPoint = new Vector2(0,0);
	
	private float minX;
	private float maxX;
	private float minY;
	private float maxY;

	// Use this for initialization
	void Start () {

		setClamp();
		
	}
	
	public void setClamp() {
		
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

	public Vector3 clampDes(Vector3 destination){

		destination.x = Mathf.Clamp(destination.x, minX, maxX);
		destination.z = Mathf.Clamp(destination.z, minY, maxY);

		return destination;

	}
}
