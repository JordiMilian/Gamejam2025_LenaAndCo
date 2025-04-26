using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerCardController;
    [SerializeField] CameraController cameraController;

    public int currentLevel = 0;
    public static GameController Instance;

    public Vector2 playerCardPosition;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentLevel = 0;
        playerCardController.canMove = true;
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
        }
        else
        {
            playerCardController.transform.position = playerCardController.currentPosition;
            playerCardController.canMove = true;
        }
    }

    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.1f);
        cameraController.MoveCamera();

    }

    void HandleCardInteraction(CardController targetCard)
    {
        //Basandonos en playerCardPosition realizamos logica mirando que carta había aquí
        //Destruimos la antigua carta ahí
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
