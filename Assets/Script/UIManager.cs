using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

		public static Animator uianime;
		public Text scoreeText;
		public Text highscoreeText;

		public Text GOscoreeText;
		public Text GOhighscoreeText;


		float scoreee = 0.0f;

		void Start ()
		{
				uianime = GetComponent<Animator> ();
		}

		void OnLevelWasLoaded (int level)
		{
				if (level == 0) {
						Manager.currentScore = 0;
				}
		}

		void Update ()
		{
				scoreee += Time.deltaTime;

				Manager.currentScore = (int)scoreee;
				scoreeText.text = "Score  " + Manager.currentScore.ToString ();



				highscoreeText.text = "Best " + PlayerPrefs.GetInt ("HighScore");



				GOscoreeText.text = "Your Score " + Manager.currentScore.ToString ();
				GOhighscoreeText.text = highscoreeText.text;

				saveBestScore ();

		}

		public static void saveBestScore ()
		{

				Manager.TotalHighScore = PlayerPrefs.GetInt ("HighScore");

				if (Manager.currentScore > Manager.TotalHighScore) {
						Manager.TotalHighScore = Manager.currentScore;
						PlayerPrefs.SetInt ("HighScore", Manager.TotalHighScore);
				}
		}

		public void gameStart ()
		{
				Time.timeScale = 1.0f;
				//ChartboostExample.showCB ();
				Manager.currentScore = 0;
				uianime.SetTrigger ("GameStart");
				//ChartboostExample.cacheCB ();

		}

		public void pausebtnpress ()
		{
				uianime.SetTrigger ("gamePaused");
				Invoke ("timeTOPause", 0.1f);
		}

	
		void timeTOPause ()
		{
				Time.timeScale = 0.00f;
		}
		public void resumeBTNPress ()
		{
				uianime.SetTrigger ("gameResume");
				Time.timeScale = 1.0f;
		}

		public void RetryBTNPresss ()
		{
				Manager.currentScore = 0;
				Application.LoadLevel (Application.loadedLevel);

		}
}
