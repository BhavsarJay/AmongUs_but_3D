using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAbility : MonoBehaviour
{

    public GameObject deadBodyPrefab;


    [PunRPC]
    void Kill(string killersName, string targetsName)
    {
        GameObject target = PlayersList.GetPlayer(targetsName);
        GameObject killer = PlayersList.GetPlayer(killersName);

        Debug.Log("Someones Dead :(");


        //Change State of the Player
        target.GetComponent<Player>().ThisPlayerDead();

        // Change to ghost sprite and play ghost animation in local instance.
        Animator targetsAnimator = target.GetComponentInChildren<Animator>();
        targetsAnimator.SetTrigger("dead");

        // Disable SpriteRenderer only if i am not the target.
        if(target != PlayersList.GetMyPlayer())
            target.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // Spawm DeadBody
        Instantiate(deadBodyPrefab, target.transform.position, Quaternion.identity);

        // Change targets collider to trigger
        target.GetComponent<BoxCollider2D>().enabled = false;

        // Teleport killer to targets position
        killer.transform.position = target.transform.position;
        /** Need to do somthing about this cause it doesnt teleport to new pos in others instances cause
            the transform view smoothens it out.
            
            Might have to write a custom position synchronization script.
        Might help:
            https://doc.photonengine.com/en-us/pun/current/gameplay/synchronization-and-state
            https://forum.photonengine.com/discussion/13140/teleport-like-abilities-while-using-photon-transform-view
        **/
    }
}
