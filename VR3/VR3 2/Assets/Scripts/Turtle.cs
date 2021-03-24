using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour
{
    SpawnPoint[] spawnPoints;
    bool[] activeTargets;
    bool setTarget = false;
    bool hasTarget = false;
    Vector3 target;
    

    [SerializeField]
    float moveSpeed = .5f;
    [SerializeField]
    float turnSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Init", .1f);
    }

    void Init()
    {
        spawnPoints = SpawnManager.S.getSpawnPoints();
        activeTargets = SpawnManager.S.getActiveObjects();
    }
    // Update is called once per frame
    void Update()
    {
        if (!GameMaster.S.getGameOver())
        {
            if (!setTarget)
            {
                transform.position = Vector3.MoveTowards(transform.position, (transform.position + transform.forward), moveSpeed);
                if (!hasTarget)
                {
                    activeTargets = SpawnManager.S.getActiveObjects();

                    int randIndex = Random.Range(0, activeTargets.Length);
                    if (activeTargets[randIndex])
                    {
                        target = spawnPoints[randIndex].gameObject.GetComponentInChildren<Apple>().transform.position;
                       
                        setTarget = true;
                    }
                }

            }
            else 
            {
               
                    rotateTowards(target);
                    transform.position = Vector3.MoveTowards(transform.position, (transform.position +transform.forward), moveSpeed);
                
            }
                
        }

    }


    protected void rotateTowards(Vector3 to)
    {
        Vector3 lookDifference = to - transform.position;

        Quaternion lookRotation =
            Quaternion.LookRotation((lookDifference).normalized);

        //over time
        transform.rotation =
            Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    public void resetTarget()
    {
        setTarget = false;
       
    }
}
