using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePosition;

    public Vector3 currentPosition;
    public bool canMove;

    public CardController targetCard;
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (!canMove) return;
        currentPosition = transform.position;
        mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        if (!canMove) return;
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }

    private void OnMouseUp()
    {
        if (targetCard != null)
        {
            GameController.Instance.MovePlayer(targetCard);
        }
        else
        {
            ResetPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponentInParent<CardController>())
        {
            targetCard = other.GetComponentInParent<CardController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<CardController>())
        {
            targetCard = null;
        }
    }

    public void ResetPlayer()
    {
        transform.position = currentPosition;
    }
}
