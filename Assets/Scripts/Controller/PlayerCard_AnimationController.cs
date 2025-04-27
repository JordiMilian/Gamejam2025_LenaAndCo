using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard_AnimationController : MonoBehaviour
{
    Animator cardAnimator;
 
    private void Awake()
    {
        cardAnimator = GetComponent<Animator>();
    }

    public void StartFloating() { cardAnimator.SetBool("isFloating", true); }
    public void EndFloating() { cardAnimator.SetBool("isFloating", false); }
    public void FlipCard()
    {
        Debug.Log("Flipped");
       //cardAnimator.SetBool("isFlipped", !cardAnimator.GetBool("isFlipped"));
        cardAnimator.SetTrigger("Flip");
    }
    public void Died() { cardAnimator.SetTrigger("Death"); }
    public void Damaged() { cardAnimator.SetTrigger("Damaged"); }
    public void Interact() { cardAnimator.SetTrigger("Interact"); }
    public bool isFlipped() { return cardAnimator.GetBool("isFlipped"); }

}
