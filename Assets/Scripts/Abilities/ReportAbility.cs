using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportAbility : MonoBehaviour
{
    [HideInInspector] public Transform nearestBody;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Report()
    {
        // Show Report Animations on everyone
        // Position Everyone on start of map
        // Show Chat

        Debug.Log("Body Reported");
    }
}
