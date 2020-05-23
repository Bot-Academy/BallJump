using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAgentFollow : MonoBehaviour
{
    public Transform BallAgentTransform;

    private Vector3 _cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        _cameraOffset = transform.position - BallAgentTransform.position;
    }

    void LateUpdate()
    {
        transform.position = BallAgentTransform.position + _cameraOffset;
    }
}


