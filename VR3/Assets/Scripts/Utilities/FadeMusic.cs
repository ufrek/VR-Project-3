/* This will fade in and fade out music according to the Fading script
 * Step 1: attach this line at Start() to assign your music
 * 
 * this.GetComponent<FadeMusic> ().AssignTunes ();
 * Make sure you have a clip loaded in your AudioSource Component
 * 
 *Step 2: Attach these lines in Update()
 *
 *GetComponent<FadeMusic> ().FadeInTunes ();
  GetComponent<FadeMusic> ().FadeOutTunes ();

Step 3: Put this in a method that calls fades. This stops the fading in and turns on fading out
	GetComponent<FadeMusic> ().fadeInTunes = false;
	GetComponent<FadeMusic>().fadeOutTunes = true;
	
 */

//Song Credit: 
//Donkey Kong "Beneath the Surface" by Vig
//https://ocremix.org/remix/OCR01242
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(AudioSource))]

public class FadeMusic : MonoBehaviour {
	private float tparam = 0;
	public float fadeInSpeed;
	public float fadeOutSpeed;
	public AudioSource bgm;
	public float playAtPoint; // pick how far into the clip you want to start
	public bool fadeInTunes = true; // if you want to turn off fade in at start make false
	public bool fadeOutTunes = false;
    public float bgmVol = .5f;

	// Use this for initialization
	void Start ()
	{
		bgm = this.GetComponent<AudioSource>();
		if (PlayerPrefs.HasKey ("isMute"))
		{
			if (PlayerPrefs.GetInt ("isMute") == 1) 
			{
				bgm.mute = true;
			} else 
				bgm.mute = false;
		}
		else
			bgm.mute = false;

		AssignTunes();
	}

	//call in start
	public void AssignTunes()
	{
		bgm = GetComponent<AudioSource> ();
		bgm.time = playAtPoint;
		bgm.volume = 0;
		bgm.Play ();
		
	}
	// Update is called once per frame
	void Update () 
	{
	
	}

	//call in update
	public void FadeInTunes()
	{

		if (fadeInTunes && tparam < 1)
		{
			fadeOutTunes = false;
			tparam += Time.deltaTime * fadeInSpeed;
			bgm.volume =  Mathf.Lerp (0, bgmVol, tparam);
		}

	}

	public void FadeOutTunes()
	{
		if (fadeOutTunes && tparam > 0)
		{
			fadeInTunes = false;
			tparam -= Time.deltaTime * fadeOutSpeed * 2.75f;
			bgm.volume =  Mathf.Lerp (0, 1, tparam);
		}
	}

	public bool getFadeIn() => fadeInTunes;
	public bool getFadeOut() => fadeOutTunes;
	public void setFadeIn(bool b) => fadeInTunes = b;
	public void setFadeOut(bool b) => fadeOutTunes = b;




}
