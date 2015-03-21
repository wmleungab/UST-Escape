using UnityEngine;
using System.Collections;

public class Starter : MonoBehaviour {

	public GameObject Ready;
	public GameObject Go;

	// Use this for initialization
	void Start () {
		Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 0);
		Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 0);

		StartCoroutine ("startanim");
	}
	
	
	IEnumerator startanim ()
	{
		float ReadyYPos=Ready.transform.position.y;
		float GoYPos=Go.transform.position.y;
		for (float i=0; i<=1; i+=0.1f) {
			Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, i);
			Ready.transform.position=new Vector3(Ready.transform.position.x,ReadyYPos-(1-i),Ready.transform.position.z);

			yield return new WaitForSeconds (0.01f);
		}
		Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 1);
		yield return new WaitForSeconds (1.5f);
		for (float i=0; i<=1; i+=0.05f) {
			Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 1-i);
			Ready.transform.position=new Vector3(Ready.transform.position.x,ReadyYPos+i,Ready.transform.position.z);

			Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, i);
			Go.transform.position=new Vector3(Ready.transform.position.x,ReadyYPos-(1-i),Ready.transform.position.z);

			yield return new WaitForSeconds (0.01f);
		}
		Ready.renderer.material.color = new Vector4 (Ready.renderer.material.color.r, Ready.renderer.material.color.g, Ready.renderer.material.color.b, 0);
		Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 1);

		yield return new WaitForSeconds (1.5f);
		for (float i=0; i<=1; i+=0.05f) {
			Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 1-i);
			Go.transform.position=new Vector3(Ready.transform.position.x,ReadyYPos+i,Ready.transform.position.z);

			yield return new WaitForSeconds (0.01f);
		}
		Go.renderer.material.color = new Vector4 (Go.renderer.material.color.r, Go.renderer.material.color.g, Go.renderer.material.color.b, 0);

		BattleController.currentBattleState =BattleState.BATTLE_PROGRESSING;
	}
}
