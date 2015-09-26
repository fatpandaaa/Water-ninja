using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

	public static int TotalHighScore;
	public static int currentScore;

	void Start () {
		GamezeroSpeed ();


	}


	void Update () {
	}

	public void GameNormalSpeed(){

		Time.timeScale = 1.0f;
	}

	public static void GamezeroSpeed(){

		Time.timeScale = 0.0f;

	}
}
