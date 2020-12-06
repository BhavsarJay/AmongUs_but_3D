using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportTrigger : MonoBehaviour
{
    private ReportAbility reportAbility;
    private PhotonView PV;
    public bool canReport;
    [HideInInspector] public GameObject nearestBody;


    private void Start()
    {
        reportAbility = GetComponent<ReportAbility>();
        PV = GetComponent<PhotonView>();
    }

    public void OnReport()
    {
        if (canReport)
        {
            PV.RPC("Report", RpcTarget.All, new string[] { gameObject.name, nearestBody.name });
            //reportAbility.Report();
        }

    }
}
