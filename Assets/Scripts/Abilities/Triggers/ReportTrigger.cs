using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportTrigger : MonoBehaviour
{
    private ReportAbility reportAbility;
    public bool canReport;

    private void Start()
    {
        reportAbility = GetComponent<ReportAbility>();
    }

    public void OnReport()
    {
        if (canReport)
            reportAbility.Report();

    }
}
