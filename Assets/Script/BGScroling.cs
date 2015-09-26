using UnityEngine;
using System.Collections;

public class BGScroling : MonoBehaviour {

	private float ScrollSpeed =4.0f;
	public float tilesizeZ;

	private Vector3 StartPos;
	void Start () {
	
		StartPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		float newPOSision = Mathf.Repeat (Time.time *ScrollSpeed, tilesizeZ);
		transform.position = StartPos + Vector3.left * newPOSision;
	
	}
}
