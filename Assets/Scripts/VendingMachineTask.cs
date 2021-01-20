using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineTask : MonoBehaviour, ITask
{
    public CinemachineVirtualCamera VendingMachineVCam;
    private bool doingTask;
    public LayerMask layermask;
    bool TaskComplete;

    /// <summary>
    /// length = no.of types of drinks,
    /// default is [1, 2 , 2] which means 1 red, 2 yellow, 2 blue.
    /// </summary>
    public int[] TypesOfDrinks = new int[] {1, 2, 2};

    
    public void TriggerTask()
    {
        if (!doingTask && !TaskComplete)
        {
            doingTask = true;
            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = false;
            VendingMachineVCam.gameObject.SetActive(true);
            StartCoroutine(AcceptClicks());
        }
        else
        {
            doingTask = false;
            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = true;
            VendingMachineVCam.gameObject.SetActive(false);
        }
    }

    IEnumerator AcceptClicks()
    {
        Debug.Log("Take 1 red Drink , 2 Yellow Drinks and 2 Blue Drinks");
        while (doingTask)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray: ray, hitInfo: out hit, maxDistance: Mathf.Infinity, layerMask: layermask.value))
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 10f);
                    GameObject drink = hit.collider.gameObject;

                    if (LayerMask.LayerToName(drink.layer).Equals("Red Drink"))
                    {
                        if (TypesOfDrinks[0] != 0)
                        {
                            TypesOfDrinks[0]--;
                            drink.GetComponent<Collider>().isTrigger = false;
                            drink.GetComponent<Rigidbody>().useGravity = true;
                        }
                    }
                    if (LayerMask.LayerToName(drink.layer).Equals("Yellow Drink"))
                    {
                        if (TypesOfDrinks[1] != 0)
                        {
                            TypesOfDrinks[1]--;
                            drink.GetComponent<Collider>().isTrigger = false;
                            drink.GetComponent<Rigidbody>().useGravity = true;
                        }
                    }
                    if (LayerMask.LayerToName(drink.layer).Equals("Blue Drink"))
                    {
                        if (TypesOfDrinks[2] != 0)
                        {
                            TypesOfDrinks[2]--;
                            drink.GetComponent<Collider>().isTrigger = false;
                            drink.GetComponent<Rigidbody>().useGravity = true;
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
                ExitTask(isComplete: false);


            foreach (int i in TypesOfDrinks)
            {
                if (i != 0)
                    break;
                Debug.Log("Task Complete !!!");

                yield return new WaitForSeconds(2f);
                ExitTask(isComplete: true);
            }
            yield return 0;
        }
    }

    public void ExitTask(bool isComplete)
    {
        doingTask = false;
        PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = true;
        VendingMachineVCam.gameObject.SetActive(false);

        TaskComplete = isComplete;
    }
}
