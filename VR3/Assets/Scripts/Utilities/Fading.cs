/* This creates a black GUI object to fade in and out of the scenes
 * 
 * Step 1:  Put this line in the Start() of any main script
 * 
 * this.GetComponent<Fading> ().BeginFade (-1); use -1 for fading in and + 1 for fading out
 * 
 * Step 2: Put these lines in a coroutine and call it when you need to fade out a scene
 * this.GetComponent<Fading> ().fadeOut = true;
		float fadeTime = this.GetComponent<Fading> ().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);

	Note: Everything but the public bools are there to adjust the fade so it works 
 * */




using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

	public Texture2D fadeOutTexture;	//black image to overlay the screen
	public float fadeInSpeed = 3; //speed to fade it
	public float fadeOutSpeed = 3;
	private float fadeSpeed = 3;
	private int drawDepth = -1000; //image's draw order in hierarchy, so render on top of everything
	private float alpha = 0;
	private int fadeDirection = -1; // if equal to 1, fades out, = to -1 fades in
	public bool fadeIn;	//don't touch it, it needs to be accessed by music fade script
	public bool fadeOut; //don't touch it, it needs to be accessed by music fade script
	public float fadeSmooth = 3; //this helps to smooth the fade
 
	void OnGUI()
	{
		//Unity's function for rendering a GUI
		if (fadeIn && fadeDirection == -1)
		{
			//fade in/out the alpha value using a direction, a speed, and Time.deltaTime to convert the operation into seconds
			alpha += fadeDirection * fadeSpeed * Time.deltaTime;
			fadeOut = false;

		}
		else if (fadeOut && fadeDirection == 1)
		{
			alpha += fadeDirection * fadeSpeed * Time.deltaTime;
			fadeIn = false;
		}




		//clamp01() forces the number to be between 0 and 1
		//GUI.color uses values between 0 and 1
		alpha = Mathf.Clamp01 (alpha);

		//set color of GUI(or our texture in this case)
		//all color values remain the same and the Alpha is set to the alpha variable
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha); //setting the alpha

		GUI.depth = drawDepth; //make sure the black texture will render on top
		if (fadeOutTexture != null)
		{
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeOutTexture); //define screen size and texture to draw
		}
	}

	//sets fade direction to direction parameter, making the scene faade in if -1 and fade out if 1
	public float BeginFade(int direction)
	{
		fadeDirection = direction;
		if (fadeDirection < 0) {
			//fade in speed set
			fadeIn = true;
			fadeOut = false;
			alpha = 1;
			fadeSpeed = fadeInSpeed;
		} 
		else if(fadeDirection > 0)
		{
			fadeOut = true;
			fadeIn = false;
			alpha = 0;
			//fade out speed set
			fadeSpeed = fadeOutSpeed;
		}
		return(fadeSpeed * fadeSmooth); //return the fadespeed so it's easy to time the Application.LoadLevel() with WaitForSeconds in a coroutine

	}


}
