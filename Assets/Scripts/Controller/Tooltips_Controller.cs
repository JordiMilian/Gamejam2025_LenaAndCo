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
        foreach (CardTypeTooltip tooltip in cardTypeTooltipsList)
        {
            if(tooltip.cardType == cardType && !tooltip.hasShown)
            {
                ShowTooltip(tooltip);
                return;
            }
        }
    }
    void ShowTooltip(CardTypeTooltip tooltip)
    {
        tooltip.hasShown = true;
        tooltipText.text = tooltip.tooltipText;
        rootAnimator.SetTrigger("Appear");
    }
    
}