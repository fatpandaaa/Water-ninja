using UnityEngine;
using System.Collections;

public class ROtateScript : MonoBehaviour {

	private float speed ;

	void Start () {
	
		speed = Random.Range(15.0f,50.0f);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(Vector3.forward *speed* Time.deltaTime);
	
	}
}
