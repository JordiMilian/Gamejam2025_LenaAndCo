using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]
public class CardObject : ScriptableObject
{
    public enum CardType { Null, Player, Hunter, Fish, Coin, Shop}
    public string cardName = "";
    public CardType cardType = CardType.Null;
    public int value = 0;

}
