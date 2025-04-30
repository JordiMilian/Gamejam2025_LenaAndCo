using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Tooltips_Controller;

public class Toottips_mouseController : MonoBehaviour
{
    [SerializeField] Animator rootAnimator;
    [SerializeField] TextMeshProUGUI tooltipText;
    [SerializeField] PlayerController playerController;
    [Serializable]
    public struct CardTypeTooltip
    {
        public string tooltipText;
        public CardType cardType;
    }
    public List<CardTypeTooltip> cardTypeTooltipsList = new List<CardTypeTooltip>();

    
    private void Update()
    {
        rootAnimator.transform.position = Input.mousePosition;

        if (playerController.targetCard != null && !playerController.targetCard.isHidden)
        {
            for (int t = 0; t < cardTypeTooltipsList.Count; t++) //go thorw all the tooltips list, if it matches, show?
            {
                CardTypeTooltip tooltip = cardTypeTooltipsList[t];
                if (tooltip.cardType == playerController.targetCard.card.cardType)
                {
                    tooltipText.text = tooltip.tooltipText;
                    rootAnimator.SetBool("isActive", true);
                    return;
                }
            }
        }
        else
        {
            rootAnimator.SetBool("isActive", false);
        }
    }
}
