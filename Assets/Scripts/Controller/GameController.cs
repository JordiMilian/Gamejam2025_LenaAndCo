using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerCardController;
    [SerializeField] CameraController cameraController;

    [SerializeField] TextMeshProUGUI hpUI, coinUI;

    public Dictionary<Vector2, CardController> gameCards = new Dictionary<Vector2, CardController>();

    public List<CardObject> cardObjectsPool;

    public int hp;
    public int maxHp;

    public int coins;

    public static GameController Instance;

    public Vector2 playerCardPosition;

    public bool isSeal = false;

    [SerializeField] RawImage blackImage;

    [SerializeField] Color hpColor, coinColor;
    public TextMeshPro playerTextInfo;
    public float maxScaleText = 1;

    #region EFFECT BOOLS
    private bool doubleEnemyDamage = false;
    private bool blockTransform = false;
    #endregion
    private void Awake()
    {
        Instance = this;
        SetHp(10);
        SetCoin(0);
        isSeal = false;

        doubleEnemyDamage = false;
        blockTransform = false;
        playerTextInfo.gameObject.SetActive(false);
    }
    private void Start()
    {
        playerCardController.canMove = true;
    }

    public bool CanTransform()
    {
        return !blockTransform;
    }
    public void AddGameCard(CardController newCard)
    {
        Debug.Log("A�adiendo " + newCard.transform.name + " a " + newCard.cardPosition.ToString());
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
        if (hp <= 0) hp = 0;

        hpUI.text = "HP: " + hp.ToString();

        if (hp == 0) StartCoroutine(KillPlayerCoroutine());
    }

    IEnumerator KillPlayerCoroutine()
    {
        float duration = 2;
        float elapsed = 0;

        yield return new WaitForSeconds(0.5f);
        playerCardController.animationController.Died();
        yield return new WaitForSeconds(0.5f);
        while (duration > elapsed) 
        {
            yield return null;

            float a = blackImage.color.a;

            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            float newA = Mathf.Lerp(a, 1, t);

            Color newColor = new Color(blackImage.color.r, blackImage.color.g, blackImage.color.b, newA);

            blackImage.color = newColor;
        }

        yield return null;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator ShowTextOnCharacter(string text, Color textColor)
    {
        playerTextInfo.gameObject.SetActive(true);
        playerTextInfo.gameObject.transform.localScale = Vector3.zero;
        playerTextInfo.text = text;
        playerTextInfo.color = textColor;

        float duration = 1;
        float elapsed = 0;

        Vector3 localScale = playerTextInfo.transform.localScale;
        Vector3 targetScale = Vector3.one * maxScaleText;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            playerTextInfo.transform.localScale = Vector3.Lerp(localScale, targetScale, t);
            yield return null;
        }

        playerTextInfo.gameObject.SetActive(false);



    }
    IEnumerator AttackPlayer(int value, CardController enemyCard)
    {
        if (doubleEnemyDamage)
        {
            value = value * 2;

            doubleEnemyDamage = false;
        }

        enemyCard.animationController.AttackCardBelow();
        playerCardController.animationController.Damaged();

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ShowTextOnCharacter("-" + value.ToString(), hpColor));
        Debug.Log("Atacan jugador: " + value.ToString());
        SetHp(hp - value);
        yield return new WaitForSeconds(0.5f);
        Destroy(enemyCard.gameObject);

    }
    void DamagePlayer(int value)
    {
        if(doubleEnemyDamage)
        {
            value = value * 2;

            doubleEnemyDamage = false;
        }
        StartCoroutine(ShowTextOnCharacter("-" + value.ToString(), hpColor));
        SetHp(hp - value);

        playerCardController.TriggerActionAnimation(); //ESTE EFECTO DE DA�O DEBER�A MEJORAR
    }

    void HealPlayer(int value)
    {
        SetHp(hp + value);
        StartCoroutine(ShowTextOnCharacter("+" + value.ToString(), hpColor));
        playerCardController.TriggerActionAnimation(); //A�ADIR EFECTO CURA
    }

    void AddCoinsPlayer(int value)
    {
        SetCoin(coins + value);

        StartCoroutine(ShowTextOnCharacter("+" + value.ToString(), coinColor));
        playerCardController.TriggerActionAnimation(); //A�ADIR EFECTO CONSEGUIR DINERO
    }

    void RemoveCoinsPlayer(int value)
    {
        SetCoin(coins - value);
        ShowTextOnCharacter("-" + value.ToString(), coinColor);
        playerCardController.TriggerActionAnimation(); //A�ADIR EFECTO QUITAR DINERO
    }
    public void SetCoin(int newCoins)
    {
        coins = newCoins;

        coinUI.text = "Coins: " + coins.ToString();
    }

    public bool CanPlayerMove(Vector2 newTargetCardSlot)
    {
        CardController temp = null;
        gameCards.TryGetValue(newTargetCardSlot, out temp);

        if(temp != null)
        {
            if (temp.card.cardType == CardType.Iceberg && !isSeal) return false;
        }
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
        StartCoroutine(MovePlayerCoroutine(targetCard));
    }

    IEnumerator MovePlayerCoroutine(CardController targetCard)
    {
        playerCardController.canMove = false;

        yield return null;
        if (CanPlayerMove(targetCard.cardPosition))
        {
            playerCardController.transform.position = targetCard.transform.position;
            playerCardPosition = targetCard.cardPosition;

            yield return StartCoroutine(MoveCamera());

            yield return StartCoroutine(HandleCardInteraction(targetCard));

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

            if (temp != null)
            {

                StartCoroutine(KillCardAnimation(temp, 1f, i));
            }
        }
    }
    IEnumerator KillCardAnimation(CardController targetCard, float maxRandomTime, int i)
    {
        float random = Random.Range(0, maxRandomTime);

        yield return new WaitForSeconds(random);

        targetCard.DieAnimation();

        RemoveGameCard(new Vector2(targetCard.cardPosition.x, i));
    }
    IEnumerator MoveCamera()
    {
        yield return new WaitForSeconds(0.1f);
        cameraController.MoveCamera();

    }

    IEnumerator HandleCardInteraction(CardController targetCard)
    {
        Debug.Log("La carta a la que hemos ido es: " + targetCard.card.cardType.ToString());

        yield return null;
        switch (targetCard.card.cardType)
        {
            case CardType.Shop:
                ShopInteraction(targetCard.card.value, targetCard);
                break;
            case CardType.Fish:
                FishInteraction(targetCard.card.value);
                break;
            case CardType.Coin: 
                CoinInteraction(targetCard.card.value);
                break;
            case CardType.Seagull:
                SeagullInteraction(targetCard.card.value);
                break;
            case CardType.SeaUrchin:
                SeaUrchinInteraction(targetCard.card.value);
                break;
            case CardType.Iceberg:
                IcebergInteraction();
                break;
            case CardType.Ship:
                ShipInteraction();
                break;
            case CardType.Net:
                NetInteraction();
                break;
            case CardType.Hunter:
                HunterInteraction(targetCard.card.value);
                break;
            case CardType.Whale:
                WhaleInteraction(targetCard.card.value);
                break;
            case CardType.FinalBoss:
                //FinalBossInteraction(targetCard.card.value);
                break;
            default:
                Debug.LogError("Oops something went wrong! You are on an incorrect card class");
                break;
        }

        yield return StartCoroutine(HandleFrontCard(targetCard));
        Destroy(targetCard.gameObject);
    }

    #region CardInteractions

    private IEnumerator CreateShop(bool visibleItems, CardController shopCard)
    {
        List<CardController> topCards = gameCards
            .Where(kv => kv.Key.x > shopCard.cardPosition.x)
            .Select(kv => kv.Value)
            .ToList();

        gameCards.Clear(); // Limpiamos la lista de gameCards para poder a�adirlas ahora con una X superior para meter los objetos en medio

        int movingCards = topCards.Count; // Contador de cartas movi�ndose

        yield return new WaitForSeconds(0.5f);
        foreach (CardController card in topCards)
        {
            StartCoroutine(MoveCard(card, () => movingCards--));
        }

        // Esperar a que todas las cartas hayan terminado de moverse
        yield return new WaitUntil(() => movingCards == 0);

        // Ahora s�, a�adimos las nuevas cartas
        for (int i = 0; i < 4; i++)
        {
            Vector2 newPos = new Vector2(shopCard.cardPosition.x + 1, i);
            SceneStarterController.Instance.AddCard(newPos, cardObjectsPool[0]);
        }
    }

    // Corrutina que mueve una carta y avisa al terminar
    private IEnumerator MoveCard(CardController card, System.Action onComplete)
    {
        Vector3 startPos = card.transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y, startPos.z + 15);

        float duration = 0.5f; // Duraci�n del movimiento en segundos
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            card.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        // Aseguramos que termina exactamente en la posici�n objetivo
        card.transform.position = targetPos;

        // Actualizamos el card despu�s de moverse
        card.cardPosition = new Vector2(card.cardPosition.x + 1, card.cardPosition.y);
        card.name = "Card(" + (card.cardPosition.x).ToString() + "," + (card.cardPosition.y).ToString() + ")";
        gameCards.Add(card.cardPosition, card);

        onComplete?.Invoke(); // Avisamos que esta carta ha terminado
    }

    private void ShopInteraction(int value, CardController shopCard)
    {
        if (!isSeal)
        {
            if (coins < value) return; //No tienes dinero suficiente
            RemoveCoinsPlayer(value);

            StartCoroutine(CreateShop(true, shopCard));
        }
        else
        {
            DamagePlayer(2);

            StartCoroutine(CreateShop(false, shopCard));
        }
    }
    private void FishInteraction(int value)
    {
        if (isSeal)
        {
            HealPlayer(value);
        }
    }
    private void IcebergInteraction()
    {
        //No hace nada, porque permite pasar a la foca y al humano no
    }
    private void ShipInteraction()
    {
        if (isSeal)
        {
            doubleEnemyDamage = true;
        }
    }
    private void CoinInteraction(int value)
    {
        if (!isSeal)
        {
            AddCoinsPlayer(value);
        }
    }

    private void SeaUrchinInteraction(int value)
    {
        if (isSeal)
        {
            DamagePlayer(value);
        }
    }

    private void HunterInteraction(int value)
    {
        if (isSeal)
        {
            Debug.Log("Da�o hunter: " + value.ToString());
            DamagePlayer(value);
        }
        else
        {
            blockTransform = true;
        }
    }

    private void SeagullInteraction(int value)
    {
        if (!isSeal)
        {
            RemoveCoinsPlayer(value);
        }
    }

    private void WhaleInteraction(int value)
    {
        if (isSeal)
        {
            DamagePlayer(value * 2);
        }
        else
        {
            DamagePlayer(value);
        }
    }

    private void NetInteraction()
    {
        blockTransform = true;
    }

    private void FrontHunterInteraction(int value, CardController hunterCard)
    {
        if(isSeal)
        {
            StartCoroutine(AttackPlayer(value, hunterCard));
        }
    }

    private void FrontWhaleInteraction(int value, CardController hunterCard)
    {
        if (isSeal)
        {
            StartCoroutine(AttackPlayer(value * 2, hunterCard));
        }
        else
        {
            StartCoroutine(AttackPlayer(value, hunterCard));
        }
    }

    #endregion
    IEnumerator HandleFrontCard(CardController targetCard)
    {
        yield return null;
        CardController frontCard = null;

        gameCards.TryGetValue(new Vector2(targetCard.cardPosition.x + 1, targetCard.cardPosition.y), out frontCard);

        if(frontCard == null)
        {
            EndRound();
        }
        else
        {
            switch(frontCard.card.cardType)
            {
                case CardType.Hunter:
                    FrontHunterInteraction(frontCard.card.value, frontCard);
                    break;
                case CardType.Whale:
                    FrontWhaleInteraction(frontCard.card.value, frontCard);
                    break;
                default:
                    break;
            }
        }

        EndRound();
    }

    void EndRound()
    {
        blockTransform = false;
        playerCardController.canMove = true;
    }
}
