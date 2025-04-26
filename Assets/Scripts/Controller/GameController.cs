using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerCardController;
    [SerializeField] CameraController cameraController;

    [SerializeField] TextMeshProUGUI hpUI, coinUI;

    public Dictionary<Vector2, CardController> gameCards = new Dictionary<Vector2, CardController>();

    public int hp;
    public int maxHp;

    public int coins;

    public static GameController Instance;

    public Vector2 playerCardPosition;

    public bool isSeal = false;
    private void Awake()
    {
        Instance = this;
        SetHp(10);
        SetCoin(0);
        isSeal = false;
    }
    private void Start()
    {
        playerCardController.canMove = true;
    }

    public void AddGameCard(CardController newCard)
    {
        Debug.Log("Añadiendo " + newCard.transform.name + " a " + newCard.cardPosition.ToString());
        gameCards.Add(newCard.cardPosition, newCard);
    }

    public void RemoveGameCard(Vector2 cardPos)
    {
        gameCards.Remove(cardPos);
    }
    public void SetHp(int newHp)
    {
        hp = newHp;
        if(hp > maxHp) hp = maxHp;

        hpUI.text = "HP: " + hp.ToString();
    }

    public void SetCoin(int newCoins)
    {
        coins = newCoins;

        coinUI.text = "Coins: " + coins.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraController.MoveCamera();
        }
    }

    public bool CanPlayerMove(Vector2 newTargetCardSlot)
    {
        if (playerCardPosition.x == -1 && newTargetCardSlot.x == 0) //Solo sirve en el primer turno, donde puedes elegir la zona que quieras
        {
            return true;
        }

        if ((newTargetCardSlot.x - playerCardPosition.x == 1) &&
            (newTargetCardSlot.y == playerCardPosition.y || newTargetCardSlot.y + 1 == playerCardPosition.y || newTargetCardSlot.y - 1 == playerCardPosition.y))
        {
            return true;
        }
        return false;
    }

    public void MovePlayer(CardController targetCard)
    {
        playerCardController.canMove = true;
        if(CanPlayerMove(targetCard.cardPosition))
        {
            playerCardController.transform.position = targetCard.transform.position;
            playerCardPosition = targetCard.cardPosition;

            StartCoroutine(MoveCamera());
            HandleCardInteraction(targetCard);

            playerCardController.TriggerActionAnimation();
            DestroyNeighbourCards(targetCard);
        }
        else
        {
            playerCardController.transform.position = playerCardController.currentPosition;
            playerCardController.canMove = true;
        }
    }

    void DestroyNeighbourCards(CardController targetCard)
    {
        for (int i = 0; i < 4; i++)
        {
            CardController temp = null;
            gameCards.TryGetValue(new Vector2(targetCard.cardPosition.x, i), out temp);

            if(temp != null) Destroy(temp.gameObject);

            RemoveGameCard(new Vector2(targetCard.cardPosition.x, i));
        }
    }
    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.1f);
        cameraController.MoveCamera();

    }

    void HandleCardInteraction(CardController targetCard)
    {
        Debug.Log("La carta a la que hemos ido es: " + targetCard.card.cardType.ToString());
        HandleNeighbourCard();
        Destroy(targetCard.gameObject);
    }

    void HandleNeighbourCard()
    {
        //Comprobamos la carta vecina

        EndRound();
    }

    void EndRound()
    {
        //El jugador puede volver a moverse
    }
}
