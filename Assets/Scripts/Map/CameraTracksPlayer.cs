using UnityEngine;
using System.Collections;

public class CameraTracksPlayer : MonoBehaviour {

	Transform player;
	float offsetx;
	float offsetz;
	public GameObject target_go;
	public bool tracePlayer = true;

	// Use this for initialization
	void Start () {
		if (tracePlayer) target_go = GameObject.FindGameObjectWithTag ("Player");
		if (target_go == null) {
			Debug.LogError ("Target Object not found for CameraTracksPlayer");
			return;
		}

		player = target_go.transform;
		//offsetx = transform.position.x - player.position.x;
		//offsetz = transform.position.z - player.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (player!=null) {
			Vector3 new_pos;
			new_pos = transform.position;
			new_pos.x = player.position.x + offsetx;
			new_pos.z = player.position.z + offsetz;
			transform.position = new_pos;
		}
	}
}
