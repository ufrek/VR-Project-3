using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Set the callAOrder value in the inspector. This is the order in which the object will travel to the points.
/// </summary>
public class WayPoint : MonoBehaviour
{
    public int callOrder;               //set in inspector
    public bool isRestPoint = false;
    [SerializeField]
    float waitTime = 3;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("hit");
            if (isRestPoint)
            {
                PathMover p = other.GetComponent<PathMover>();
                p.setTakingBreak(true);
                p.StartCoroutine(Rest(p, waitTime));
            }
            else
                other.GetComponent<PathMover>().resetTarget();
            Destroy(this.gameObject);

        }

    }

    IEnumerator Rest(PathMover p, float wait)
    {
        yield return new WaitForSecondsRealtime(wait);
        p.resetTarget();
    }

}
