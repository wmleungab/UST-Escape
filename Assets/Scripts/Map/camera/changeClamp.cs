using UnityEngine;
using System.Collections;

public class changeClamp : MonoBehaviour {

	public float mapX = 25.0f;
	public float mapY = 20.0f;
	public Vector2 centerPoint = new Vector2(0,0);
	
	private bool onLeft = false;
	
	ClampCamera ccObj;
	
	void Start() {
		ccObj=Camera.main.GetComponent<ClampCamera>();
	}

	void OnTriggerEnter(Collider other){

		if (!onLeft && (other.transform.position.x > this.transform.position.x)){
			onLeft = true;
			swapPara();
			ccObj.setClamp();
		}
		else if (onLeft && (other.transform.position.x < this.transform.position.x)){
			onLeft = false;
			swapPara();
			ccObj.setClamp();
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
