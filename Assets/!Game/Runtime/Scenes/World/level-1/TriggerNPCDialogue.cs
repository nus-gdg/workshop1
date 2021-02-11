using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNPCDialogue : MonoBehaviour
{
    public bool enter;

    // Use this for initialization
    void Start()
    {
        enter = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("triggered");
        enter = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enter = false;
        Fungus.Flowchart.BroadcastFungusMessage("EndConversation");
        //Debug.Log("Exited");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (enter)
        {
            Fungus.Flowchart.BroadcastFungusMessage("StartConversation");
        }
        enter = false;
    }
}
