using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Null, Player, Hunter, Fish, Coin, Shop, Seagull, SeaUrchin, Iceberg, Ship, Net, Whale, FinalBoss }
[CreateAssetMenu(fileName = "Card", menuName = "ScriptableObjects/Card", order = 1)]

public class CardObject : ScriptableObject
{
    public string cardName = "";
    public CardType cardType = CardType.Null;
    public int value = 0;

    public Material material;
}
