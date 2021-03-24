using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GazeOverEvent : MonoBehaviour
{
    [Range(0, 360)]
    public float maximiumViewingAngle = 30f;
    [SerializeField]
    float maxDistance = 200;
    public UnityEvent OnGazeEnter;
    public UnityEvent OnGazeExit;
    public UnityEvent OnGazeClick;

    private bool isHovering = false;
    private bool doOnce = false;
    private VRObjectInteract obj;

    void Start()
    {
        obj = this.gameObject.GetComponent<VRObjectInteract>();
    }
    //called once per frame
    void Update()
    {
        Vector3 forward = Camera.main.transform.forward;
        Vector3 toObject = this.transform.position - Camera.main.transform.position;
        float angleToObject = Vector3.Angle(forward, toObject);
        if (angleToObject < maximiumViewingAngle)
        {
            float dist = Vector3.Distance(this.transform.position, Camera.main.transform.position);
            if (dist <= maxDistance)
            {
                Hovering();

                if (Google.XR.Cardboard.Api.IsTriggerPressed || Input.GetMouseButtonDown(0))
                {

                    obj.OnPointerClick();
                }
            }
            else
                NotHovering();
           
        }
        else
        {
            NotHovering();
        }
        
    }

    private void Hovering()
    {
        if (!isHovering)
        {
            obj.OnPointerEnter();
            isHovering = true;
        }
        
    }

    private void NotHovering()
    {
        if (isHovering)
        {
            obj.OnPointerExit();
            isHovering = false;
        }
    }
        
    



}
