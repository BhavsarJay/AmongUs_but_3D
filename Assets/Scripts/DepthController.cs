using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Now this is simple to understand.
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 3f);
    }
}
