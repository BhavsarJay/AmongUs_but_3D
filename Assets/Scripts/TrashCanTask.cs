using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class TrashCanTask : MonoBehaviour, ITask
{
    public CinemachineFreeLook TrashCanFreeLookCam;
    public LayerMask interactableLayers;
    public GameObject trashBagSpringJoint;
    public GameObject trashCanLid;

    [HideInInspector] public bool doingTask;
    private bool TaskComplete;
    private float defaultCamSpeed;
    private float defaultSpringForce;
    private Vector3 trashCanLidStartPos;

    [Tooltip("Speed at which the camera slows down while orbiting in a task.")]
    public float camSpeedDampning = 10f;


    private void Start()
    {
        defaultCamSpeed = TrashCanFreeLookCam.m_XAxis.m_MaxSpeed;
        TrashCanFreeLookCam.m_XAxis.m_MaxSpeed = 0;
        trashCanLidStartPos = trashCanLid.transform.position;
    }

    public void TriggerTask()
    {
        if (!doingTask && !TaskComplete)
        {
            doingTask = true;
            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = false;
            TrashCanFreeLookCam.gameObject.SetActive(true);
            StartCoroutine(AcceptInput());
        }
        else
        {
            doingTask = false;
            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = true;
            TrashCanFreeLookCam.gameObject.SetActive(false);
        }
    }

    IEnumerator AcceptInput()
    {
        GameObject hitObject = null;
        RaycastHit hit = new RaycastHit();
        float depth = 0;
        Vector3 offset = Vector3.zero;

        while (doingTask)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray: ray, hitInfo: out hit, maxDistance: Mathf.Infinity, layerMask: interactableLayers))
                {
                    hitObject = hit.collider.gameObject;

                    //if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Trash Bag"))
                    //{
                    //    defaultSpringForce = trashBagSpringJoint.GetComponent<SpringJoint>().spring;
                    //    trashBagSpringJoint.GetComponent<SpringJoint>().spring = 0;
                    //}

                    Debug.DrawLine(ray.origin, hit.point, Color.red, 10f);
                    depth = Camera.main.WorldToScreenPoint(hit.transform.position).z;

                    offset = hit.transform.position - GetMouseWorldPos(depth);
                }
            }
            else if (Input.GetMouseButton(0))
            {
                if (hitObject != null)
                {
                    hitObject.transform.LookAt(transform.position);
                    hitObject.transform.Rotate(0, 180f, 0, Space.Self);

                    depth = Camera.main.WorldToScreenPoint(hitObject.transform.position).z;

                    Vector3 newPos = GetMouseWorldPos(depth) + offset;

                    if (hitObject == trashCanLid)
                    {
                        //Move The Lid to newPos
                        if (newPos.y > trashCanLidStartPos.y)
                            hitObject.transform.position = newPos;
                    }
                    else
                        trashBagSpringJoint.transform.position = newPos;
                }
            }
            else if (Input.GetMouseButtonUp(0))
                hitObject = null;



            if (Input.GetMouseButtonDown(1))
            {
                TrashCanFreeLookCam.m_XAxis.m_MaxSpeed = defaultCamSpeed;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                // Lerp the value of max speed down to 0

                float timeToStart = Time.time;
                while (TrashCanFreeLookCam.m_XAxis.m_MaxSpeed != 0)
                {
                    TrashCanFreeLookCam.m_XAxis.m_MaxSpeed = Mathf.Lerp(defaultCamSpeed, 0, (Time.time - timeToStart) * camSpeedDampning);
                    //Here speed is the 1 or any number which decides the how fast it reach to one to other end.

                    yield return null;
                }

                //OR just set it to 0
                //TrashCanFreeLookCam.m_XAxis.m_MaxSpeed = 0f;
            }

            else if (Input.GetKeyDown(KeyCode.Escape))
                ExitTask(isComplete: false);
            yield return 0;
        }
    }

    private Vector3 GetMouseWorldPos(float depth)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = depth;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    public void ExitTask(bool isComplete)
    {
        doingTask = false;
        PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = true;
        TrashCanFreeLookCam.gameObject.SetActive(false);
        trashBagSpringJoint.GetComponent<SpringJoint>().spring = defaultSpringForce;


        TaskComplete = isComplete;
    }
}