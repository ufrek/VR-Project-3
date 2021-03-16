using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Audio Courtesy of 
[RequireComponent(typeof(AudioSource))]

public class GameMaster : MonoBehaviour
{
    public static GameMaster S;
    bool isOver = true;


    AudioSource soundMgr;
    [SerializeField]
    AudioClip sGameOver;
    FadeMusic musicFader;
    bool fadeInMusic;                   //this is badly done, fix later
    bool fadeOutMusic = false;
    // Start is called before the first frame update
    void Start()
    {
       
        S = this;
        musicFader = this.GetComponentInChildren<FadeMusic>();
        fadeInMusic = true;

        soundMgr = this.GetComponent<AudioSource>();
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeInMusic)
        {
            musicFader.FadeInTunes();
        }
        else if (fadeOutMusic)
        {
            musicFader.FadeOutTunes();
        }
    }

    public void startGame()
    {
        setGameOver(false);
        this.GetComponent<Fading>().BeginFade(-1);
        fadeInMusic = true;
        
    }
    public void endGame()
    {
        fadeInMusic = false;
        fadeOutMusic = true;
        musicFader.setFadeIn(false);
        musicFader.setFadeOut(true);
        setGameOver(true);
        soundMgr.PlayOneShot(sGameOver);
        this.GetComponent<Fading>().BeginFade(1);
        
        StartCoroutine(Close());
        
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(3);                         //hardcoded, can fix later
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene == SceneManager.GetSceneByBuildIndex(0))
            SceneManager.LoadScene(1);
        else
            Application.Quit();

    }

    public bool getGameOver() => isOver;
    public void setGameOver(bool b) => isOver = b;
}
