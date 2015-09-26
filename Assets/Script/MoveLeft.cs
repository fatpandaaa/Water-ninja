using UnityEngine;
using System.Collections;

public class MoveLeft : MonoBehaviour {

	private float MoveSPeed;



	void Start () {

		destroyallspike ();
	
	}
	
	void Update () {

		if (Manager.currentScore < 5) {
			MoveSPeed = 9.0f;
				}
		if (Manager.currentScore > 6 && Manager.currentScore <10) {
			MoveSPeed = 10.0f;
				}

		if (Manager.currentScore > 11 && Manager.currentScore <19) {
			MoveSPeed = 11.0f;
		}

		if (Manager.currentScore > 20 && Manager.currentScore <25) {
			MoveSPeed = 12.0f;
		}

		if (Manager.currentScore > 26 && Manager.currentScore <30) {
			MoveSPeed = 13.0f;
		}

		if (Manager.currentScore > 31 && Manager.currentScore <35) {
			MoveSPeed = 14.0f;
		}

		if (Manager.currentScore > 36 && Manager.currentScore <50) {
			MoveSPeed = 15.0f;
		}
		if (Manager.currentScore > 51 && Manager.currentScore <80) {
			MoveSPeed = 16.0f;
		}
		if (Manager.currentScore > 81 && Manager.currentScore <100) {
			MoveSPeed = 17.0f;
		}
		if (Manager.currentScore > 101 && Manager.currentScore <120) {
			MoveSPeed = 18.0f;
		}
		if (Manager.currentScore > 121 && Manager.currentScore <150) {
			MoveSPeed = 19.0f;
		}
		if (Manager.currentScore > 151 && Manager.currentScore <200) {
			MoveSPeed = 20.0f;
		}
		if (Manager.currentScore > 201 ) {
			MoveSPeed = 22.0f;
		}





		transform.Translate(Vector3.left*MoveSPeed * Time.deltaTime,Space.Self);
	}

	void destroyallspike(){
		
		Destroy (gameObject, 7.0f);
	}
}
