using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {
	public GameObject Player;
	private Vector3 CameraFollow;


	void Start () {
		CameraFollow = transform.position - Player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Player.transform.position  + CameraFollow;
	}
}
