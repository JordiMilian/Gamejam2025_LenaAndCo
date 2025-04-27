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

    public static SceneStarterController Instance;
    private void Awake()
    {
        Instance = this;
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
                newCardSlot.name = "Card(" + (i).ToString() + "," + (j).ToString() + ")";
                CardController newCardController = newCardSlot.GetComponentInChildren<CardController>();

                newCardController.cardPosition = new Vector2(i, j);
                GameObject newCardObject = Instantiate(cardPrefab, newCardSlot.transform);

                newCardController.card = gamePreset.level[i].position[j];

                if(newCardController.card.cardType == CardType.Null)
                {
                    Destroy(newCardObject.transform.parent.gameObject);
                }
                else
                {
                    GameController.Instance.AddGameCard(newCardController);
                }
            }
        }

    }

    public void AddCard(Vector2 cardPosition, CardObject card, bool rotate = false)
    {
        GameObject newCardSlot = Instantiate(cardSlotPrefab);
        Vector3 newPosition = new Vector3(originalPos.x + XOffset * cardPosition.y, originalPos.y, originalPos.z + ZOffset * cardPosition.x);

        newCardSlot.transform.position = newPosition;
        newCardSlot.transform.parent = this.transform;
        newCardSlot.name = "Card(" + (cardPosition.x).ToString() + "," + (cardPosition.y).ToString() + ")";

        CardController newCardController = newCardSlot.GetComponentInChildren<CardController>();
        
        newCardController.cardPosition = cardPosition;
        GameObject newCardObject = Instantiate(cardPrefab, newCardSlot.transform);

        newCardController.card = card;
        GameController.Instance.AddGameCard(newCardController);

        if(rotate)
        {
            newCardObject.GetComponent<RegularCard_AnimationController>().AutoFlip();
        }
    }

}
