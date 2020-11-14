using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentAbility : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private PlayerMovement playerMovement;
    [HideInInspector] public Transform nearestVent;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void Vent()
    {
        //Move on top of Vent
        StartCoroutine(MovePlayer(transform.position, nearestVent.position, 0.7f));
    }

    IEnumerator MovePlayer(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.position = target;
        
        //Play Animation
        anim.SetBool("vent", true);

        yield return new WaitForSeconds(0.2f);

        //Lower the player a little.
        Vector3 loweredPos = new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z);
        startTime = Time.time;
        while (Time.time < startTime + 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, loweredPos, (Time.time - startTime) / overTime);
            yield return null;
        }

        sr.enabled = false;
        playerMovement.canMove = false;
    }

    internal void VentOut()
    {
        sr.enabled = true;
        anim.SetBool("vent", false);
        playerMovement.canMove = true;
    }
}
