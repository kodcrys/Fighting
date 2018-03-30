using UnityEngine;
using System.Collections;

public class CameraTracksPlayer : MonoBehaviour {
	// Transform of the player.
	Transform player;
	float offsetX;

	// Use this for initialization
	void Start () 
	{
		GameObject player_go = GameObject.FindGameObjectWithTag("Player");

		if(player_go == null) 
		{
			Debug.LogError("Couldn't find an object with tag 'Player'!");
			return;
		}

		// Get the start position of the player.
		player = player_go.transform;

		// the range of the camera and the player.
		offsetX = transform.position.x - player.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// the camera follow the player with a idenfity range. 
		if(player != null) 
		{
			Vector3 pos = transform.position;
			pos.x = player.position.x + offsetX;
			transform.position = pos;
		}
	}
}
