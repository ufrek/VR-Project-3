using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VRObjectInteract : MonoBehaviour
{
    [SerializeField]
    Material mInactive;
    [SerializeField]
    Material mGazed;
    [SerializeField]
    AudioClip sGazeSound;
    [SerializeField]
    AudioClip sClickSound;
    [SerializeField]
    GameObject textPrefab;

    public float clickSoundVolume = .6f;
    private AudioSource myAudioSource;
    private Renderer myRenderer;
    private bool showText = false;

    //maybe add in a location randomizer later?


    // Start is called before the first frame update
    void Start()
    {
        myRenderer = this.GetComponent<Renderer>();
        myAudioSource = GameObject.FindGameObjectWithTag("FX").GetComponent<AudioSource>();
        SetMaterial(false);
    }

    public void OnPointerEnter()
    {
        myAudioSource.PlayOneShot(sGazeSound);
        SetMaterial(true);
        showText = true;      //make text popup when you eat a seaweed
    }

    public void OnPointerExit()
    {
        SetMaterial(false);
        Destroy(this.transform.gameObject);
    }

    //create a text label
    public void OnGUI()
    {
        if (showText)
            GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height - 50, 300, 50), "Yummy! You just found a seaweed. Eat them!");
    }


    public void OnPointerClick()                                            //just plays a a sound for now
    {
        myAudioSource.PlayOneShot(sClickSound, clickSoundVolume);
        GameObject g = GameObject.FindGameObjectWithTag("GameMaster");
        ScoringMechanic.S.IncrementScore();
        Destroy(this.gameObject);
    }

    /// <param name="gazedAt">
    /// Value `true` if this object is being gazed at, `false` otherwise.
    /// </param>
    private void SetMaterial(bool gazedAt)
    {
        if (mInactive != null && mGazed != null)
        {
            myRenderer.material = gazedAt ? mGazed : mInactive;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
