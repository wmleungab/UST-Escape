using UnityEngine;
using System.Collections;

public class changeClamp : MonoBehaviour {

	public float LmapX = 25.0f;
	public float LmapY = 20.0f;
	public Vector2 LcenterPoint = new Vector2(0,0);

	public float RmapX = 25.0f;
	public float RmapY = 20.0f;
	public Vector2 RcenterPoint = new Vector2(0,0);
	
	public bool cameraOnRight = true;
	
	ClampCamera ccObj;
	
	void Start() {
		Debug.Log("changeClamp start");
		ccObj=Camera.main.GetComponent<ClampCamera>();
		Debug.Log("cameraOnRight: " + cameraOnRight);
		 if (cameraOnRight && SaveLoadSystem.getInstance().atriumSceneStateArr[(int)SaveLoadSystem.AtriumSceneState.FROMLG2]){
		Debug.Log("Load camera");
				moveToLeft();
				ccObj.setClamp();
		}
	}
	
	void OnTriggerEnter(Collider other){

		if(other.tag == "Player"){
			if (!(other.bounds.max.x > collider.bounds.max.x)){
				// enter on right
				if(!cameraOnRight){
					moveToRight();
					ccObj.setClamp();
				}
			}		
		}			
		
	}
	
	void OnTriggerExit(Collider other){

		if(other.tag == "Player"){
			if (!(other.bounds.max.x > collider.bounds.max.x)){
				// exit on right
				if(cameraOnRight){
					moveToLeft();
					ccObj.setClamp();
				}
			}
		}
		
	}
	
	void moveToLeft(){
		ccObj.mapX = LmapX;
		ccObj.mapY = LmapY;
		ccObj.centerPoint = LcenterPoint;
		cameraOnRight = false;
		Debug.Log("moveCamera to left");
	}
	
	void moveToRight(){
		ccObj.mapX = RmapX;
		ccObj.mapY = RmapY;
		ccObj.centerPoint = RcenterPoint;
		cameraOnRight = true;
		Debug.Log("moveCamera to Right");
	}
	
/*	void swapPara(){
		
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
	*/
}
