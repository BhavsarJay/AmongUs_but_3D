using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    /** <summary>
     * Every Task will have a trigger function that allows the player to do the task
     * and make the camera change and everything else..
     * </summary>
     **/
    void TriggerTask();

    /** <summary>
     * To Exit from the task
     * </summary>
     **/
    void ExitTask(bool isComplete);
}