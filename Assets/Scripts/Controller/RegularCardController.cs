using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularCardController : MonoBehaviour
{
    Animator cardAnimator;
    private void Awake()
    {
        cardAnimator = GetComponent<Animator>();
    }
    public void FlipCard()
    {
        cardAnimator.SetBool("isFlipped", !cardAnimator.GetBool("isFlipped"));
        cardAnimator.SetTrigger("Flip");
    }
    public void AttackCardBelow()
    {
        //attacking animation and getting attacked animation should be the same lenght
    }
    public void GettingAttacked()
    {

    }
    public void SpawnCard()
    {

    }
    public void DestroyCard()
    {

    }
}
