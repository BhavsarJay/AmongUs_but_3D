using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class VentTrigger : MonoBehaviour
{
    private PhotonView PV;
    private VentAbility ventAbility;
    [HideInInspector] public bool insideVent;
    [HideInInspector] public Transform nearestVent;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        ventAbility = GetComponent<VentAbility>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVent()
    {
        if (insideVent == false)
        {
            StartCoroutine(ventAbility.Vent_In(transform.position, nearestVent.position, 0.7f));
            insideVent = true;
        }
        else
        {
            StartCoroutine(ventAbility.Vent_Out());
            insideVent = false;
        }
    }
}
