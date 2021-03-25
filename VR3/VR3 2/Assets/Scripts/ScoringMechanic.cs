using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringMechanic : MonoBehaviour
{
   
    
    public static ScoringMechanic S;
    [SerializeField]
    int scoreValue;
    private VRObjectInteract[] targetItems;
    private int itemCount;
    private TextMesh objectCounter;
    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        S = this;
        targetItems = this.GetComponentsInChildren<VRObjectInteract>();
        itemCount = targetItems.Length;
        objectCounter = this.GetComponent<TextMesh>();

    }

    public void IncrementScore()
    {
        score += scoreValue;


        objectCounter.text = "Score: " + score;

      
    }
    // Update is called once per frame

}
