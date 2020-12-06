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
            PV.RPC("Vent_In", RpcTarget.All, new object[] { transform.name, nearestVent.position });
            insideVent = true;
        }
        else
        {
            PV.RPC("Vent_Out", RpcTarget.All, new object[] { transform.name });
            insideVent = false;
        }
    }
}
