using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool canPlayerMove = false;
    [SerializeField] PlayerCardController playerCardController;
    [SerializeField] CameraController cameraController;

    private void Start()
    {
        canPlayerMove = true;
        //playerCardController.StartFollowingMouse();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            cameraController.MoveCamera();
        }
    }
}
