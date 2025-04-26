using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStarterController : MonoBehaviour
{
    public bool sceneCreated = false;
    public GameObject cardSlotPrefab;
    public GameObject cardPrefab;

    public Vector3 originalPos;
    public float XOffset = 0;
    public float ZOffset = 0;
    public GamePresetObject gamePreset;
    private void Awake()
    {
        sceneCreated = false;

        CreateCardSlots();

        sceneCreated = true;
    }
    
    void CreateCardSlots()
    {
        for (int i = 0; i < gamePreset.level.Count; i++) //I es el Nivel
        { 
            for (int j = 0; j < gamePreset.level[i].position.Count; j++) //J es la Posicion
            {
                GameObject newCardSlot = Instantiate(cardSlotPrefab);
                Vector3 newPosition = new Vector3(originalPos.x + XOffset * j, originalPos.y, originalPos.z + ZOffset * i);

                newCardSlot.transform.position = newPosition;
                newCardSlot.transform.parent = this.transform;
                newCardSlot.name = "Card(" + (i + 1).ToString() + "," + (j + 1).ToString() + ")";
                CardController newCardController = newCardSlot.GetComponentInChildren<CardController>();

                newCardController.cardPosition = new Vector2(i, j);
                GameObject newCardObject = Instantiate(cardPrefab, newCardSlot.transform);

                newCardController.card = gamePreset.level[i].position[j];

                if(newCardController.card.cardType == CardObject.CardType.Null)
                {
                    Destroy(newCardObject);
                }
                else
                {
                    GameController.Instance.AddGameCard(newCardController);
                }
            }
        }

    }
}
