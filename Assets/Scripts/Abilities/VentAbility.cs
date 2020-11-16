using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentAbility : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private PlayerMovement playerMovement;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public IEnumerator Vent_In(Vector3 source, Vector3 target, float overTime)
    {
        playerMovement.canMove = false;

        // Move the player on top of the vent.
        float startTime = Time.time;
        float distance = Vector3.Distance(transform.position, target);

        if(!(distance > 0.349 && distance < 0.35))
        {
            while (Time.time < startTime + overTime)
            {
                transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
                yield return null;
            }
        }
        transform.position = target;

        // Play vent-in animation.
        anim.SetBool("vent", true);

        // Wait for the length of animations then hide the character.
        yield return new WaitForSeconds(0.25f);
        sr.enabled = false;
    }

    public IEnumerator Vent_Out()
    {
        sr.enabled = true;
        // Play vent-out animation.
        anim.SetBool("vent", false);

        // Wait for the length of animations.
        yield return new WaitForSeconds(0.25f);

        playerMovement.canMove = true;

    }
}
