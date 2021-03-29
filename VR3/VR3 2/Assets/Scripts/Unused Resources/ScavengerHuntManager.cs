using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengerHuntManager : MonoBehaviour
{
    [SerializeField]
    public Material mTelevisionOn;                              //set in Inspector
    [SerializeField]
    public GameObject television;                                  //set in INspector
    public static ScavengerHuntManager S;
    private VRObjectInteract[] huntItems;
    private int itemCount;
    private TextMesh objectCounter;
                              
    // Start is called before the first frame update
    void Start()
    {
        S = this;
        huntItems = this.GetComponentsInChildren<VRObjectInteract>();
        itemCount = huntItems.Length;
        objectCounter = this.GetComponent<TextMesh>();
        
        if(itemCount == 1)
            objectCounter.text = itemCount + "\nObject Left!";
        else
            objectCounter.text = itemCount + "\nObjects Left!";

        if (itemCount == 0)
        {
            print("Put Some  hhunt objects as a child of this oobject into the scene");
        }
        print("item count = " + itemCount);
        
    }

    public void decrementItem()
    {
        itemCount -= 1;
        
        if (itemCount == 1)
            objectCounter.text = itemCount + "\nObject Left!";
        else
            objectCounter.text = itemCount + "\nObjects Left!";

        if (itemCount == 0)
        {
            television.gameObject.GetComponent<MeshRenderer>().materials[2] = mTelevisionOn;            //Fades too fast anyways to matter
            GameMaster.S.endGame();
        }
    }
    // Update is called once per frame
  
}
