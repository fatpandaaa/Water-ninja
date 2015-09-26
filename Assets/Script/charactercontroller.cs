using UnityEngine;
using System.Collections;

public class charactercontroller : MonoBehaviour
{

		public static Animator anim;
		public static Rigidbody2D playerrigidbody;

		void Start ()
		{

				anim = GetComponent <Animator> ();
				//playerrigidbody = GetComponent<Rigidbody2D> ();


		}
	

		void Update ()
		{

			
				if ((Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Began) || Input.GetMouseButton (0)) {
						//jumpHero();
						//Debug.Log("touch");
				}
		}


	#if UNITY_EDITOR
		public  void jumpHero ()
		{
				anim.SetTrigger ("JUMP");
				playerrigidbody.AddRelativeForce (Vector2.up * 290);
		}
		public  void SlideHero ()
		{
				anim.SetTrigger ("SLIDE");
		}
	#else
	public static void jumpHero(){
		anim.SetTrigger("JUMP");
		//playerrigidbody.AddRelativeForce(Vector2.up * 250);
	}
	public static void SlideHero(){
		anim.SetTrigger("SLIDE");
	}
	#endif


}
