using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour 
{
    public Texture[] count;
    public AudioSource soundMgr;
    public AudioClip horn;          //when time runs out
    public AudioClip beep;          //when counting down the timer
    public float beepVol = 1;
    public float hornVol = .8f;
    int isMute;
    Vector3 min = Vector3.zero;
    Vector3 max;
    float scaleFactor = .05f;
    float opacityScaleFactor = .2f;
    Color originalColor;
    Timer timer;
	// Use this for initialization
	void Start () 
    {
        timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        isMute = PlayerPrefs.GetInt("isMute");
        soundMgr = this.GetComponent<AudioSource>();
        max = new Vector3(45.6f, 42.3f, 1 );
        originalColor = this.GetComponent<Renderer>().material.color;
        StartCoroutine(CDown());
	}

    public IEnumerator CDown()
    {
        this.GetComponent<Renderer>().material.mainTexture = count[3];
        StartCoroutine(ScaleAndFade(beep, false));
        yield return new WaitForSeconds(1);
      
        this.GetComponent<Renderer>().material.mainTexture = count[2];
        StartCoroutine(ScaleAndFade(beep, false));
        yield return new WaitForSeconds(1);

        this.GetComponent<Renderer>().material.mainTexture = count[1];
        StartCoroutine(ScaleAndFade(beep, false));
        yield return new WaitForSeconds(1);

        this.GetComponent<Renderer>().material.mainTexture = count[0];
        StartCoroutine(ScaleAndFade(horn, true));
    }

    public IEnumerator ScaleAndFade(AudioClip sound, bool isFinished)
    {
        if(isMute == 0)
        {
            if(sound == beep)
                soundMgr.PlayOneShot(sound, beepVol);
            else
            soundMgr.PlayOneShot(sound, hornVol);
        }
           

        Vector3 tempScale = new Vector3(0, 0, 0);
        Color tempColor;
        float i = 0;
        float j = 0;

        this.transform.localScale = min;
        this.GetComponent<MeshRenderer>().enabled = true;

        do
        {
            tempScale = this.transform.localScale;
            tempScale.x = Mathf.Lerp(min.x, max.x, i);
            tempScale.y = Mathf.Lerp(min.y, max.y, i);
            tempScale.z = Mathf.Lerp(min.z, max.z, i);
            //print(tempScale.x + ", " + tempScale.y + ", " + tempScale.z);
           
            if (i >= .75f)       //opacity modifier
            {
                tempColor = this.GetComponent<Renderer>().material.color;
                tempColor.a = Mathf.Lerp(originalColor.a, 0, j);
                j += opacityScaleFactor;
            }
            this.transform.localScale = tempScale;
            i += scaleFactor;
            yield return new WaitForSeconds(scaleFactor * Time.deltaTime);
        } while (i < 1);

        this.GetComponent<MeshRenderer>().enabled = false;

        if (isFinished)
        {
            timer.doOnce = true;
            GameMaster.S.startGame();
            yield return new WaitForSeconds(2);
            Destroy(this.gameObject);
        }
    }
}
