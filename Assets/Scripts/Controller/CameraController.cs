using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject cameraFocus;
    [SerializeField] Vector3 cameraMovementVector;
    public void MoveCamera()
    {
        cameraFocus.transform.position = cameraFocus.transform.position + cameraMovementVector;
    }
}
