using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    [HideInInspector] public GameObject currentTask;

    public void OnClick_UseBtn()
    {
        currentTask.GetComponent<ITask>().TriggerTask();
    }
}
