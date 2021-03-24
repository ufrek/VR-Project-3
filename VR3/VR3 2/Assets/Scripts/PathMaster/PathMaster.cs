using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Put this on an empty game object that is to be the parent of all the WayPoints. This will sort them out by order and pass it along to Path Mover.
/// </summary>
public class PathMaster : MonoBehaviour
{
    WayPoint[] path;

    void Start()
    {
        UpdatePath();
    }

    void UpdatePath()
    {
        path = this.GetComponentsInChildren<WayPoint>();
        /*foreach (WayPoint w in path)
        {
            w.GetComponent<MeshRenderer>().enabled = false;
        }*/
        Array.Sort(path, SortByOrder);
    }
    public int SortByOrder(WayPoint a, WayPoint b)
    {
        if (a == null)
        {
            if (b == null)
            {
                // If a null and b null, they're eqyal.
                return 0;
            }
            else
            {
                // If a null and b isn't, b is greater
                return -1;
            }
        }
        else
        {
            // a not null
            if (b == null)
            //b is null, x greater
            {
                return 1;
            }
            else
            {
                //compare lengths
                int retval = a.callOrder.CompareTo(b.callOrder);

                return retval;
            }
        }
    }

    public WayPoint[] getPath()
    {

        UpdatePath();
        return path;

        //for vector 3 output
        /* UpdatePath();
         Vector3[] positions = new Vector3[path.Length];
         for(int i = 0; i < path.Length; i++)
         {
             positions[i] = path[i].gameObject.transform.position;
         }

         return positions;
        */
    }
}
