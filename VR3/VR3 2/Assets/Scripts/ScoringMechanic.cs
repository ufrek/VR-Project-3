using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(TextMesh))]
public class ScoringMechanic : MonoBehaviour
{
   
    
    public static ScoringMechanic S;
    [SerializeField]
    int scoreValue = 100;
    private VRObjectInteract[] targetItems;
    private int itemCount;
    private TextMesh objectCounter;
    private  int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        S = this;
        score = 0;
        objectCounter = this.GetComponent<TextMesh>();
        objectCounter.text = "Score: " + score;

    }

    public void IncrementScore()
    {
        score += scoreValue;


        objectCounter.text = "Score: " + score;

      
    }
    // Update is called once per frame

}
