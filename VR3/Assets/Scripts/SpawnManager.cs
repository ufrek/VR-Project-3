using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    Transform[] spawnPoints;
    List<GameObject> pointsInUse;
    [SerializeField]
    Camera mainCamera;              //set in Inspector
    [SerializeField]
    float maximumActivePointDistance = 400;
    [SerializeField]
    int activeObjects = 0;
   

    public static SpawnManager S;
    public int MaxObjects = 3;
    public GameObject objectPrefab;
    SpawnPoint[] spawnObjs;
    bool[] hasObject;
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        activeObjects = 0;
        //get all spawn transforms
        spawnObjs = this.GetComponentsInChildren<SpawnPoint>() ;
        hasObject = new bool[spawnObjs.Length];
        spawnPoints = new Transform[spawnObjs.Length];
        for (int i = 0; i < spawnObjs.Length; i++)
        {
            hasObject[i] = false;
            spawnPoints[i] = spawnObjs[i].gameObject.transform;
        }
        pointsInUse = new List<GameObject>();
        StartCoroutine(SpawnObjects());

    }

    private IEnumerator SpawnObjects()
    {
        while (!GameMaster.S.getGameOver())
        {
            float waitTime = Random.Range(.25f, 2f);
            yield return new WaitForSeconds(waitTime);
            if (activeObjects <= MaxObjects)
            {
                activeObjects += 1;

                int randIndex = Random.Range(0, spawnPoints.Length);
                if (hasObject[randIndex] == false)
                {
                    hasObject[randIndex] = true;
                    GameObject apple = Instantiate(objectPrefab, spawnObjs[randIndex].transform);
                    apple.transform.SetParent(spawnObjs[randIndex].transform);
                    apple.GetComponent<Apple>().setIndex(randIndex);
                    
                }
                


            }
        }
       
    }

    public void EmptyPointsInUse()
    {
        pointsInUse.Clear();
    }
    public void SetPointsInUse(GameObject gs)
    {
        pointsInUse.Add(gs);
    }
    
    public SpawnPoint[] getSpawnPoints() => spawnObjs;
    public bool[] getActiveObjects() => hasObject;
    public void ResetHasObject(int i)
    {
        hasObject[i] = false;
        activeObjects -= 1;
    } 



}
