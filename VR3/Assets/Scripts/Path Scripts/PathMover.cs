using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this on the object you want to move, or even the parent.
/// 
/// IMPORTANT: You will need to darg the pathMaster boject to this component in the inspector
/// 
/// Currently set to ONLY rotate boats around as they move. Modify move and turn speed in inspector as needed.
/// </summary>
public class PathMover : MonoBehaviour
{
   [SerializeField]
    GameObject pathObj;              //set in inspector

    WayPoint[] points;
    bool setTarget = false;
    bool hasTarget = false;
    int currentTarget;
    int finalTarget;
    Vector3 target;
    int length;
    bool isTakingBreak = false;

    [SerializeField]
    float moveSpeed = .5f;
    [SerializeField]
    float turnSpeed = 1;
    [SerializeField]
    float restSpeed = .1f;
    
    
    float lerpSpeed;
    [SerializeField]
    int interpolationFramesCount = 45; // Number of frames to completely interpolate between the 2 positions
    int elapsedFrames;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Init", .1f);
    }

    void Init()
    {
        lerpSpeed = moveSpeed;
        points = pathObj.gameObject.GetComponent<PathMaster>().getPath();
        length = points.Length;
        elapsedFrames = interpolationFramesCount;
        //currentTarget = 0;
        
    }

    void SetPath()
    {
       
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
                    points = pathObj.gameObject.GetComponent<PathMaster>().getPath();                           //redundant but it throws a null exception if not put here......
                    print(points.Length);
                    if (points.Length != 0)
                    {
                       
                        target = points[0].gameObject.transform.position;                                       //done stupidly, but it works for now
                    }
                    else
                        GameMaster.S.endGame();

                    
                    print(target);
                    setTarget = true;
                    hasTarget = true;
                }
                
           

            }
            else
            {

                if(this.gameObject.tag == "Boat")
                    rotateTowards(target);
                
                if(isTakingBreak)
                    transform.position = Vector3.MoveTowards(transform.position, target, changeVelocity(-1) );                    ///probably too choppy need to lerp it I'm thinking
                else
                    transform.position = Vector3.MoveTowards(transform.position, target, changeVelocity(1));

            }

        }     
    }

    float changeVelocity(int direction)
    {
        if (direction < 0)
        {
            elapsedFrames -= 1;
        }
        else
            elapsedFrames += 1;
       elapsedFrames =  Mathf.Clamp(elapsedFrames, 0, interpolationFramesCount);

        float interpolationRatio = (float)elapsedFrames / interpolationFramesCount;
        float speed = Mathf.Lerp(restSpeed, moveSpeed, interpolationRatio);
       
        return speed;
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

  
    public void setTakingBreak(bool b) => isTakingBreak = b;

    public void resetTarget()
    {
        setTarget = false;
        hasTarget = false;
        isTakingBreak = false;
        elapsedFrames = 0;
        //currentTarget += 1;
    }
}
