using UnityEngine;
using System.Collections;

public class Ender : MonoBehaviour {
	public GameObject Win;
	public GameObject Lost;
	// Use this for initialization
	void Start () {
		Win.renderer.material.color = new Vector4 (Win.renderer.material.color.r, Win.renderer.material.color.g, Win.renderer.material.color.b, 0);
	//	Lost.renderer.material.color = new Vector4 (Win.renderer.material.color.r, Win.renderer.material.color.g, Win.renderer.material.color.b, 0);
		Lost.transform.localScale = new Vector3(0,0,1);
	}
	
	// Update is called once per frame
	void Update () {
		if (BattleController.currentBattleState == BattleState.BATTLE_ENDING_WIN) {
						win ();
						BattleController.currentBattleState = BattleState.BATTLE_ENDING;
				} else if (BattleController.currentBattleState == BattleState.BATTLE_ENDING_LOST) {
						lost ();
						BattleController.currentBattleState = BattleState.BATTLE_ENDING;
				}
	}

	void 	win(){
		StartCoroutine ("fadein", Win);
		Invoke ("returnScene", 5);
		Win.audio.Play ();
	}
	void 	lost(){
		GameObject.Find("Inventory").SendMessage ("toMapMode");
		Lost.transform.GetChild(1).GetComponent<TextMesh>().text="X0";
		gameObject. SendMessage ("findKey", "greenapple");
		StartCoroutine ("fadein2", Lost);
		CancelInvoke ();
	}

	void findKeyCallback(Transform item){
		if (item != null) {
			Lost.transform.GetChild(1).GetComponent<TextMesh>().text="X1";
		}
	}

	void returnScene(){
		if(GlobalValues.BattleData.isFinalStage)
			Application.LoadLevel("SecretChamberAfter");
			else
		Application.LoadLevel(GlobalValues.BattleData.returnScene);

	}

	IEnumerator fadein (GameObject o)
	{
		yield return new WaitForSeconds (0.1f);

		Vector3 ols = o.transform.localScale;
			for (float i=0; i<=1; i+=0.05f) {
			o.renderer.material.color = new Vector4 (o.renderer.material.color.r, o.renderer.material.color.g, o.renderer.material.color.b, i);
			o.transform.localScale=new Vector3(ols.x+20*(1-i),ols.y+20*(1-i),1);
				yield return new WaitForSeconds (0.01f);
			}

	}

	IEnumerator fadein2 (GameObject o)
	{
		yield return new WaitForSeconds (0.1f);
		Lost.audio.Play ();
		Vector3 op = o.transform.position;
		Lost.transform.localScale = new Vector3 (1.4f, 1.4f, 1);


		for (float i=0; i<=1; i+=0.05f) {
			o.transform.position=new Vector3(op.x,op.y-5*(1-i),op.z);
			yield return new WaitForSeconds (0.01f);
		}
		
	}
}
