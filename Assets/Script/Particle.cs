using UnityEngine;
using System.Collections;

public class Particle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		particleSystem.renderer.sortingLayerName = "bg";
		particleSystem.renderer.sortingOrder = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
