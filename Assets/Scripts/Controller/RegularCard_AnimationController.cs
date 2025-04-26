using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularCard_AnimationController : MonoBehaviour
{
    Animator cardAnimator;
    [SerializeField] GameObject GO_cardRoot;
    private void Awake()
    {
        cardAnimator = GetComponent<Animator>();
    }
    public void FlipCard()
    {
        cardAnimator.SetBool("isFlipped", !cardAnimator.GetBool("isFlipped"));
        cardAnimator.SetTrigger("Flip");
    }
    public void AttackCardBelow() { cardAnimator.SetTrigger("Attack"); }
    public bool isFlipped() { return cardAnimator.GetBool("isFlipped"); }

    public void HideCard()
    {
        GO_cardRoot.SetActive(false);
    }
    public void Dissapear()
    {
        cardAnimator.SetTrigger("Dissapear");
    }

}
