using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    GameObject toFollow;
    [SerializeField]
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position - toFollow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform != null)
            this.transform.position = toFollow.transform.position + offset;
    }
}
