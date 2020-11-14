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
    bool insideVent;

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
            ventAbility.Vent();
            insideVent = true;
        }
        else
        {
            ventAbility.VentOut();
            insideVent = false;
        }
    }
}
