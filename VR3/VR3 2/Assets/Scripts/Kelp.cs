using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kelp : MonoBehaviour
{
    [SerializeField]
    int rotationSpeed = 130; //how many degrees per second
    [SerializeField]
    float bounceSpeed = 3;
    [SerializeField]
    float bounceHeight = 5f;
    Vector3 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        //Vector3 pos = origin;
        //transform.position = new Vector3(pos.x, Mathf.Sin(Time.time * bounceSpeed) * bounceHeight, pos.z);

    }
}
