using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform player;
	public float distance = 7;
	public float speed = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {

		transform.position = new Vector3(transform.position.x,transform.position.y,player.position.z-distance);
		transform.position = Vector3.Lerp (transform.position,new Vector3(player.position.x,transform.position.y,transform.position.z), Time.deltaTime*speed);
		//transform.LookAt(player.position);
	}
}
