using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntManager : MonoBehaviour
{
    private VRObjectInteract[] huntItems;
    private int itemCount;

    // Start is called before the first frame update
    void Start()
    {
        GameObject mgr = GameObject.FindGameObjectWithTag("HuntMgr");
        huntItems = mgr.GetComponentsInChildren<VRObjectInteract>();
        itemCount = huntItems.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
