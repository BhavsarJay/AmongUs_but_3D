using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public CrewmateHudManager crewmateHud;
    TaskManager taskManager;

    private void Start()
    {
        taskManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<TaskManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Enable Use button
        if (other.gameObject == PlayersList.GetMyPlayer())
        {
            taskManager.currentTask = gameObject;
            crewmateHud.ToggleUseBtn(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Disable Use button
        if (other.gameObject == PlayersList.GetMyPlayer())
        {
            taskManager.currentTask = null;
            crewmateHud.ToggleUseBtn(false);
        }
    }
}
