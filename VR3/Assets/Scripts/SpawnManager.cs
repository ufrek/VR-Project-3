using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Transform[] spawnPoints;
    [SerializeField]
    int activeApples = 0;
   

    public static SpawnManager S;
    public int MaxApples = 3;
    public GameObject applePrefab;
    GameObject[] spawnObjs;
    bool[] hasApple;
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        activeApples = 0;
        //get all spawn transforms
        spawnObjs = GameObject.FindGameObjectsWithTag("Respawn");
        hasApple = new bool[spawnObjs.Length];
        spawnPoints = new Transform[spawnObjs.Length];
        for (int i = 0; i < spawnObjs.Length; i++)
        {
            hasApple[i] = false;
            spawnPoints[i] = spawnObjs[i].gameObject.transform;
        }

        StartCoroutine(SpawnApples());

    }

    private IEnumerator SpawnApples()
    {
        while (!GameMaster.S.getGameOver())
        {
            float waitTime = Random.Range(.25f, 2f);
            yield return new WaitForSeconds(waitTime);
            if (activeApples <= MaxApples)
            {
                activeApples += 1;

                int randIndex = Random.Range(0, spawnPoints.Length);
                if (hasApple[randIndex] == false)
                {
                    hasApple[randIndex] = true;
                    GameObject apple = Instantiate(applePrefab, spawnObjs[randIndex].transform);
                    apple.transform.SetParent(spawnObjs[randIndex].transform);
                    apple.GetComponent<Apple>().setIndex(randIndex);
                    
                }
                


            }
        }
       
    }
    

    public GameObject[] getSpawnPoints() => spawnObjs;
    public bool[] getActiveApples() => hasApple;
    public void ResetHasApple(int i)
    {
        hasApple[i] = false;
        activeApples -= 1;
    } 



}
