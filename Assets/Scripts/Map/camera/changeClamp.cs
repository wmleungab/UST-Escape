using UnityEngine;
using System.Collections;

public class changeClamp : MonoBehaviour {

	public float mapX = 25.0f;
	public float mapY = 20.0f;
	public Vector2 centerPoint = new Vector2(0,0);
	
	private bool cameraOnRight = true;
	
	ClampCamera ccObj;
	
	void Start() {
		ccObj=Camera.main.GetComponent<ClampCamera>();
	}
	
	void OnTriggerEnter(Collider other){

		if (!(other.bounds.max.x > collider.bounds.max.x)){
			// enter on right
			if(!cameraOnRight){
				swapPara();
				ccObj.setClamp();
				cameraOnRight = true;
			}
		}			
		
	}
	
	void OnTriggerExit(Collider other){

		if (!(other.bounds.max.x > collider.bounds.max.x)){
			// exit on right
			if(cameraOnRight){
				swapPara();
				ccObj.setClamp();
				cameraOnRight = false;
			}
		}
		
	}
	
	void swapPara(){
		
		swap (ref ccObj.mapX , ref mapX);
		swap (ref ccObj.mapY , ref mapY);
		swap (ref ccObj.centerPoint , ref centerPoint);

	}

	void swap (ref float x, ref float y){
		
		float temp;
		
		temp=x;
		x=y;
		y=temp;
		
	}
	void swap (ref Vector2 x, ref Vector2 y){
		
		Vector2 temp;
		
		temp=x;
		x=y;
		y=temp;

	}
	
}
