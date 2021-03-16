using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
    static Text gt;
    static public Timer S;
    public Text timesUp;
    public int timer = 60;
    int halfTime;
    public bool doOnce = false;
    public bool triggerOnce = true;
    public AudioSource soundMgr;
    public AudioClip beep;
    public float beepVol = .9f;
    public AudioClip  horn;
    public float hornVol = .7f;
    public AudioClip tenLeft;
    public float tenLeftVol = 1;
    public AudioClip halfWay;
    public float halfWayVol = .85f;
	// Use this for initialization
	void Start ()
    {
        S = this;
        soundMgr = this.GetComponent<AudioSource>();
        gt = this.gameObject.GetComponent<Text>();
        gt.text = timer.ToString();
        halfTime = timer / 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (triggerOnce && doOnce && GameMaster.S.getGameOver())
        {
            StartCoroutine(CountingDown());
            doOnce = false;
            triggerOnce = false;
        }
	
	}

    public IEnumerator CountingDown()
    {
        do
        {
            yield return new WaitForSeconds(1);
            if (GameMaster.S.getGameOver())
            {
                
                timer -= 1;
                gt.text = timer.ToString();

                ////------------SOUND MUX TEST
                //if (timer != 0)
                //{
                //    soundMgr.PlayOneShot(halfWay, vol);
                //}

                if (timer == halfTime)
                {
                    if (PlayerPrefs.GetInt("isMute") == 0)
                        soundMgr.PlayOneShot(halfWay, halfWayVol);
                }
                    
                else if (timer == 10)
                {
                    if (PlayerPrefs.GetInt("isMute") == 0)
                        soundMgr.PlayOneShot(tenLeft, tenLeftVol);
                }
                    
                else if (timer <= 5 && timer != 0)
                {
                    if (PlayerPrefs.GetInt("isMute") == 0)
                        soundMgr.PlayOneShot(beep, beepVol);
                }
                    
                else if (timer == 0)
                {
                    timesUp.GetComponent<Text>().enabled = true;
                    if (PlayerPrefs.GetInt("isMute") == 0)
                        soundMgr.PlayOneShot(horn, .7f);
                }
                    
               

            }
               
        }
        while (ScoreDisplay.S.getLives() > 0 && timer > 0);

        yield return new WaitForSeconds(1.5f);
        print("End Game");
        GameMaster.S.setGameOver(true);
        yield return new WaitForSeconds(1.5f);
        GameMaster.S.endGame();



    }
}
