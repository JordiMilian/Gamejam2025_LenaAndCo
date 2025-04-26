using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    public CardObject card;

    public RegularCard_AnimationController animationController;
    public Vector2 cardPosition;
    private void Start()
    {
        if (card.cardType == CardType.Null) Destroy(this);
        animationController = GetComponentInChildren<RegularCard_AnimationController>();
    }

    public void DieAnimation()
    {
        animationController.Dissapear();

        StartCoroutine(DestroyCoroutine());
    }

    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
