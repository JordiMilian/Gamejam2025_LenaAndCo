using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 mousePosition;

    public Vector3 currentPosition;
    public bool canMove;

    public CardController targetCard;

    public PlayerCard_AnimationController animationController;

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
        animationController.StartFloating();
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
        animationController.EndFloating();
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
        if (GameController.Instance.blockTransform)
        {
            animationController.FailedFlip();
            return;
        }
        Animator cardanimator = GetComponent<Animator>();
        GameController.Instance.isSeal = !GameController.Instance.isSeal;
        cardanimator.SetBool("isHuman", !GameController.Instance.isSeal);
        Debug.Log("FLIP" + GameController.Instance.isSeal);
        animationController.FlipCard();

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

    public void TriggerDamageAnimation()
    {
        animationController.Damaged();
    }
    IEnumerator CantMoveFor(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }
}
