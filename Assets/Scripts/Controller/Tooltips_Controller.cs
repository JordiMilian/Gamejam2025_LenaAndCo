using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Tooltips_Controller : MonoBehaviour
{
    [SerializeField] Animator rootAnimator;
    [SerializeField] TextMeshProUGUI tooltipText;
    public static Tooltips_Controller Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Serializable]
    public struct CardTypeTooltip
    {
        public string tooltipText;
        public CardType cardType;
        public bool hasShown;
    }
    public List<CardTypeTooltip> cardTypeTooltipsList = new List<CardTypeTooltip>();
    
    public void CheckIfTooltip(CardType cardType)
    {
        
        for (int t = 0; t < cardTypeTooltipsList.Count; t++)
        {
            CardTypeTooltip tooltip = cardTypeTooltipsList[t];
            if (tooltip.cardType == cardType && tooltip.hasShown == false)
            {
                ShowTooltip(t);
                return;
            }
        }
    }
    public void ShowTooltip(int indexOfTooltip)
    {
        CardTypeTooltip newCardType = cardTypeTooltipsList[indexOfTooltip];
        newCardType.hasShown = true;
        newCardType.tooltipText = cardTypeTooltipsList[indexOfTooltip].tooltipText;
        cardTypeTooltipsList[indexOfTooltip] = newCardType;
        tooltipText.text = newCardType.tooltipText;
        rootAnimator.SetTrigger("Appear");
    }
    
}