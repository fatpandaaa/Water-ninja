using UnityEngine;
using System.Collections;

public class SpikeCaller : MonoBehaviour
{

		public GameObject landpikePrefeb;
		public GameObject AirspikePrefeb;

		public Camera  cameraaatemp;

		//private static Vector3 startingposfromcamera = camera.ScreenToWorldPoint(new Vector3 (0,0,0));

		private Vector3 startingPos;

		void Start ()
		{
	
				startCallingTheSpikesinLoop ();

				Vector3 startingposfromcamera = cameraaatemp.ScreenToWorldPoint (new Vector3 (Screen.width + 50, Screen.height * 0.35f, 0));
				startingPos = new Vector3 (startingposfromcamera.x, startingposfromcamera.y, 0);



		}
	
		void Update ()
		{

				//Debug.Log (startingPos.x);
	
		}

		void startCallingTheSpikesinLoop ()
		{

				InvokeRepeating ("CreateSpike", 2, 2.5F);
		}



		void CreateSpike ()
		{


				int randomObject = Random.Range (0, 2);

				switch (randomObject) {
				case 0:
						Instantiate (landpikePrefeb, startingPos, Quaternion.identity);
						break;
				case 1:
						Instantiate (AirspikePrefeb, startingPos, Quaternion.identity);
		
						break;
				default:
						print ("water me. Loadblocks");
						break;
				}


				//startCallingTheSpikesinLoop ();

		}




}
