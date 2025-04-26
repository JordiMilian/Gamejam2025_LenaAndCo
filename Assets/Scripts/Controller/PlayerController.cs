using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePosition;

    public Vector3 currentPosition;
    public bool canMove;

    public CardController targetCard;

    [SerializeField] PlayerCard_AnimationController animationController;

    int clickCounter = 0;

    private void Start()
    {
        clickCounter = 0;
    }
    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (!canMove) return;

        clickCounter++;

        StartCoroutine(DoubleClickEvent());
        currentPosition = transform.position;
        mousePosition = Input.mousePosition - GetMousePos();
    }

    IEnumerator DoubleClickEvent()
    {
        yield return new WaitForSeconds(0.2f);

        if (clickCounter >= 2) FlipCard();
        
        clickCounter = 0;
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

    void FlipCard()
    {
        Debug.Log("FLIP");
        animationController.FlipCard();
        GameController.Instance.isSeal = !GameController.Instance.isSeal;

        StartCoroutine(CantMoveFor(1));
    }
    public void ResetPlayer()
    {
        transform.position = currentPosition;
    }

    public void TriggerActionAnimation()
    {
        animationController.Interact();
    }
    IEnumerator CantMoveFor(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }
}
