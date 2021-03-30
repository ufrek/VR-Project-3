using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenuTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Boat")
        {
            other.gameObject.GetComponent<AudioSource>().mute = true;
            GameMaster.S.endGame();


        }



    }
}
