using UnityEngine;
using System.Collections;

public class CameraTracksPlayer : MonoBehaviour {

	Transform player;
	float offsetx;
	float offsetz;

	// Use this for initialization
	void Start () {
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");
		if (player_go == null) {
			Debug.LogError ("Couldn't find GameObject with tag 'Player'");
			return;
		}

		player = player_go.transform;
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
