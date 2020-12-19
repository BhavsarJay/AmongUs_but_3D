using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentAbility : MonoBehaviour
{
    private Animator anim;
    private PlayerMovement playerMovement;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    [PunRPC]
    public void Vent_In(string name, Vector3 ventPos)
    {
        GameObject player = PlayersList.GetPlayer(name);
        StartCoroutine(CR_Vent_In(player, ventPos, 0.7f));
    }
    
    [PunRPC]
    public void Vent_Out(string name)
    {
        GameObject player = PlayersList.GetPlayer(name);
        StartCoroutine(CR_Vent_Out(player));
    }


    IEnumerator CR_Vent_In(GameObject _player, Vector3 target, float overTime)
    {
        //If I am Venting then disable movement.
        GameObject myPlayer = PlayersList.GetMyPlayer();
        if(_player == myPlayer)
            playerMovement.canMove = false;

        // Move the player on top of the vent.
        float startTime = Time.time;
        Vector3 source = _player.transform.position;
        float distance = Vector3.Distance(source, target);

        if(!(distance > 0.349 && distance < 0.35))
        {
            while (Time.time < startTime + overTime)
            {
                _player.transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
                yield return null;
            }
        }
        _player.transform.position = target;


        // Play vent-in animation.
        //_player.GetComponentInChildren<Animator>().SetBool("vent", true);

        // Wait for the length of animations then hide the character.
        yield return new WaitForSeconds(0.25f);

        //Disable the sprite.
        _player.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    IEnumerator CR_Vent_Out(GameObject _player)
    {
        //Enable the sprite.
        _player.GetComponentInChildren<MeshRenderer>().enabled = true;

        // Play vent-out animation.
        _player.GetComponentInChildren<Animator>().SetBool("vent", false);

        // Wait for the length of animations.
        yield return new WaitForSeconds(0.25f);

        playerMovement.canMove = true;
    }
}
