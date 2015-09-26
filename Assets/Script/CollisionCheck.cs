using UnityEngine;
using System.Collections;

public class CollisionCheck : MonoBehaviour
{
	
		private RevMobSampleAppCSharp revMob;

		public static bool AdSwitch = true;
	

		void Start ()
		{
				Debug.Log ("lagse");
				revMob = GameObject.FindObjectOfType<RevMobSampleAppCSharp> ().GetComponent<RevMobSampleAppCSharp> ();

				revMob.StartRevMob ();
				revMob.CreateFullscreen ();

	
				ChartboostExample.cacheCB ();
				ChartboostExample.showCB ();

				
		}
	





		void OnCollisionEnter2D (Collision2D other)
		{

				if (other.gameObject.tag == "Spikes") {

						Destroy (other.gameObject);
						
						UIManager.uianime.SetTrigger ("GameOver");

						

						StartCoroutine ("AdShownDelay");

						//Debug.Log ("game over ");
				}
		
		}


		//vodrota dekhanor drkr ny 

		IEnumerator AdShownDelay ()
		{
				yield return new WaitForSeconds (0.3f);
				Time.timeScale = 0.0f;
					
			
				if (!AdSwitch) {
						revMob.CreateFullscreen ();
						revMob.ShowFullScreen ();
						AdSwitch = true;
				} else {
						ChartboostExample.cacheCB ();
						ChartboostExample.showCB ();
						AdSwitch = false;
				}

        
		}

}
