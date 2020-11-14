using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentProximity : MonoBehaviour
{
    RoleManager roleManager;
    public GameObject ventBtn;
    public GameObject sabotageBtn;
    
    void Start()
    {
        roleManager = FindObjectOfType<RoleManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        GameObject myPlayer = PlayersList.GetMyPlayer();

        if(other == myPlayer)
        {
            if(roleManager.myrole == RoleManager.IMPOSTER)
            {
                //Enable VentBtn Disable Sabotage
                sabotageBtn.SetActive(false);
                ventBtn.SetActive(true);

                //Set nearest vent in Vent Ability Script.
                myPlayer.GetComponent<VentAbility>().nearestVent = transform;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Disable Vent Btn Enable Sabotage
        ventBtn.SetActive(false);
        sabotageBtn.SetActive(true);
    }
}
