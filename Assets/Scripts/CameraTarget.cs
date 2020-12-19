using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTarget : MonoBehaviour
{
    Transform target;

    void Start()
    {
        //target = PlayersList.GetMyPlayer().transform;

        #region +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++
        // Comment everything else in this function if you are testing.

        target = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<CinemachineVirtualCamera>().Follow = target;
        GetComponent<CinemachineVirtualCamera>().LookAt = target;
        #endregion +++++++++++++++++++ FOR TESTING SCENE +++++++++++++++++++
    }

    //void LateUpdate()
    //{
    //    if (!fixedUpdate)
    //    {
    //        Vector3 desiredPosition = target.position + offset;
    //        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime * 100);
    //        transform.position = smoothedPosition;
    //    }
    //}

    //void FixedUpdate()
    //{
    //    if (fixedUpdate)
    //    {
    //        Vector3 desiredPosition = target.position + offset;
    //        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime * 100);
    //        transform.position = smoothedPosition;
    //    }
    //}
}
