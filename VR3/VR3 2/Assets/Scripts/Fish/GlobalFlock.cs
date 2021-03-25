using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFlock : MonoBehaviour
{

    public GameObject fishPrefab;
   
    public  int tankSize = 70; // This parameter is very important to control the range of fish
    
    public int numFish = 15; // Control the number of fish
    public GameObject[] allFish;
    public Vector3 goalPos;

    // Use this for initialization
    void Start()
    {
        goalPos = this.transform.position;
        allFish = new GameObject[numFish];
        for (int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), //This parameter is important to control different initial positions of different fish groups
                                      Random.Range(-tankSize, tankSize),
                                      Random.Range(-tankSize, tankSize));
            pos = pos + this.transform.position;
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, fishPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

   
}