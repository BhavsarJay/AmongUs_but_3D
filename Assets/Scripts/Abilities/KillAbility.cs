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

        // No Animations Yet :(
        //Animator targetsAnimator = target.GetComponentInChildren<Animator>();
        //targetsAnimator.SetTrigger("dead");

        // Disable SpriteRenderer only if i am not the target.
        if(target != PlayersList.GetMyPlayer())
        {
            target.GetComponentInChildren<MeshRenderer>().enabled = false;
            
            // Set State of the target to dead if its not my player
            target.GetComponent<Player>().myState = Player.State.dead;
        }
        else
        {
            target.GetComponent<Player>().ThisPlayerDead();
        }

        // Spawm DeadBody rename it to targets name.
        Vector3 offset = new Vector3(0, -0.3f, 0);
        GameObject deadbody = Instantiate(deadBodyPrefab, target.transform.position + offset, Quaternion.identity);
        deadbody.name = targetsName;

        // Show death animations here, if needed.

        // Change targets collider to trigger
        target.GetComponent<CapsuleCollider>().isTrigger = true;

        // Teleport killer to targets position
        killer.transform.position = target.transform.position;
        
        
        /** If u want to write a custom position synchronization script.
        Might help:
            https://doc.photonengine.com/en-us/pun/current/gameplay/synchronization-and-state
            https://forum.photonengine.com/discussion/13140/teleport-like-abilities-while-using-photon-transform-view
        **/
    }
}
