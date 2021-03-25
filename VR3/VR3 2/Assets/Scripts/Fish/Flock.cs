using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GlobalFlock gFlock;
    public float speed = 1f;
    float rotationSpeed = 2.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 5.0f;
    bool turning = false;

    // Use this for initialization
    void Start()
    {
        gFlock = this.transform.parent.GetComponent<GlobalFlock>();
        speed = Random.Range(0.5f, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, gFlock.goalPos) >= gFlock.tankSize)
        {
            turning = true;
        }
        else
            turning = false;

        if (turning)
        {
            //Vector3 direction = gFlock.goalPos - transform.position;
            //transform.rotation = Quaternion.Slerp(transform.rotation,
            //    Quaternion.LookRotation(direction),
            //    rotationSpeed * Time.deltaTime);
            //speed = Random.Range(0.5f, 1);
        }
        else
        {
            if (Random.Range(0, 5) < 3)
            {
                //ApplyRules();
            }
                
        }
        transform.Translate(0, 0, Time.deltaTime * speed);

    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = gFlock.allFish;

        Vector3 vcentre = gFlock.goalPos;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;

        Vector3 goalPos = gFlock.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if (dist <= neighbourDistance)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }

                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }

        if (groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != gFlock.goalPos)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                        Quaternion.LookRotation(direction),
                                                        rotationSpeed * Time.deltaTime);
        }
    }
}