using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public void PrintEvent(string s) 
    {
        Debug.Log("Event o nazwie: " + s + " leci w" + Time.time + " sekundzie");
    }
}
