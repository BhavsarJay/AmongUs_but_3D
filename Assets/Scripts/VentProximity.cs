using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentProximity : MonoBehaviour
{
    RoleManager roleManager;
    public GameObject ventBtn;
    public GameObject sabotageBtn;
    GameObject myPlayer;
    
    void Start()
    {
        myPlayer = PlayersList.GetMyPlayer();
        roleManager = FindObjectOfType<RoleManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == myPlayer)
        {
            if(roleManager.myrole == RoleManager.IMPOSTER)
            {
                //Enable VentBtn Disable Sabotage
                sabotageBtn.SetActive(false);
                ventBtn.SetActive(true);

                //Set nearest vent in Vent Ability Script.
                myPlayer.GetComponent<VentTrigger>().nearestVent = transform;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Disable Vent Btn Enable Sabotage
        ventBtn.SetActive(false);
        sabotageBtn.SetActive(true);
    }
}
