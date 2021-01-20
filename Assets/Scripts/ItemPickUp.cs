using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Transform trashBagSpringJoint;
    public TrashCanTask trashCanTask;
    private bool canPick;
    private bool isHolding;
    private Transform myplayer;

    void Start()
    {
        myplayer = PlayersList.GetMyPlayer().transform;
        Physics.IgnoreCollision(GetComponentInChildren<MeshCollider>(), myplayer.GetComponent<CapsuleCollider>(), ignore: true);
    }

    void Update()
    {
        if (isHolding && trashCanTask.doingTask == false)
            trashBagSpringJoint.position = myplayer.transform.position;
    }

    private void OnMouseDown()
    {
        if (canPick)
            isHolding = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == myplayer.gameObject)
            canPick = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == myplayer.gameObject)
            canPick = false;
    }
}
