using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportProximity : MonoBehaviour
{
    public GameObject ReportBtn;
    ReportTrigger reportTrigger;

    GameObject myPlayer;
    
    private void Start()
    {
        myPlayer = PlayersList.GetMyPlayer();
        reportTrigger = myPlayer.GetComponent<ReportTrigger>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == myPlayer)
        {
            //Enable Report Button
            reportTrigger.canReport = true;

            //Set nearestBody in Body Ability Script.
            myPlayer.GetComponent<ReportAbility>().nearestBody = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Disable Vent Btn Enable Sabotage
        reportTrigger.canReport = false;
    }
}
