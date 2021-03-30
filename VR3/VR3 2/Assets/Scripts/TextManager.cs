using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    //public GameBehavior GameManager;

    /*
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }
    */

    public string labelText = "Look around and eat the seaweeds";
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 130, Screen.height - 50, 300, 50), labelText);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Manatee")
        {
            Debug.Log("Yummy");
            // GameManager.Items += 10;
            labelText = ("Yummy! You just ate a seaweed.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Manatee")
        {
            Debug.Log("Keep searching");
        }
    }
}


