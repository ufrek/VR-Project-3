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

    public float clickSoundVolume = .6f;
    private AudioSource myAudioSource;
    private Renderer myRenderer;

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
    }

    public void OnPointerExit()
    {
        SetMaterial(false);
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
