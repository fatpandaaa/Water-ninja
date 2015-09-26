using UnityEngine;
using System.Collections;

public class bgMusic : MonoBehaviour
{

	public AudioSource SoundSource;
	private static bool created = false;

	void Awake ()
	{ 


		if (!created) { 
			DontDestroyOnLoad (this.gameObject); 
			created = true; 
		} else { 
			Destroy (this.gameObject); 
		}

		SoundSource.playOnAwake = false; 
		SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
		SoundSource.loop = true; 
	}
		
		

	void Start ()
	{
	
		//SoundSource.clip = SoundClip;
		SoundSource.Play ();
		ChartboostExample.cacheCB ();
		ChartboostExample.showCB ();

	}
	
	// Update is called once per frame
	void Update ()
	{


	
	}
}
