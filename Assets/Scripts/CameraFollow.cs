using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing = 0.05f;
    public Vector3 offset;
    private Transform target;

    void Start()
    {
        //name = Player.player.name;
        //Debug.Log(name);
        //target = PlayersList.GetPlayer(name).transform;

        target = PlayersList.GetMyPlayer().transform;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothing * Time.deltaTime * 100);
        transform.position = smoothedPosition;
    }
}
