using System;

public class GameHandler
{
    private GameBoard board;
    private Player player;
    private int playerRow;
    private int playerCol;
    private int enemyMultiplier = 1; 
    private bool canTransform = true;

    public GameHandler(GameBoard gb, Player p)
    {
        board = gb;
        playerRow = 0;
        playerCol = 0;
        player = p;
    }

    public void Play()
    {
        bool gameOver = false; 
        bool correctMovement = false;
        int transformation, nextRow = 0, nextCol = 0; 


        while (!gameOver)
        {
            // Decidir si isFoca o no
            Console.Write("Vols transformar-te? (0 - no, 1 - yes):");
            transformation = Convert.ToInt32(Console.ReadLine());

            if(transformation == 1 && canTransform)
            {
                player.IsSeal = !player.IsSeal;
            }
            canTransform = true; 
            
            // Validar si el player es pot moure a la posició escollida
            while (!correctMovement)
            {
                nextRow = playerRow + 1;
                Console.Write("Selecciona la pròxima columna del player (current: "+playerCol+")");
                nextCol = Convert.ToInt32(Console.ReadLine());
                correctMovement = CheckMovement(nextRow, nextCol);

                // DEBUG
                if (correctMovement) {Console.Write("OLEE la posición es correcta\n");}
                else {Console.Write("ayayyyyyy la posición es incorrecta\n");}
            }
            correctMovement = false;
            
            // GESTIONAR MOVIMENT
            MovePlayer(nextRow, nextCol);
            if (gameOver) {break;}

            gameOver = HandleFrontCard();
        }

        Console.WriteLine("Game Over. Press any key to exit.");
        Console.ReadKey();
    }

    private bool CheckMovement(int nextRow, int nextCol) // Checks if a movement can be performed
    {
        bool correctMovement = true;

        if (!IsInBounds(nextRow, nextCol)) // Si es selecciona una posició fora del tauler falla
        {
            correctMovement = false;
        }
        else if (nextRow != playerRow + 1 ) // Si es selecciona una filera més llunyana de la següent 
        {
            correctMovement = false;
        }
        else if (nextCol > playerCol + 1 || nextCol < playerCol - 1) // Si accedim a una columna que no toca
        {
            correctMovement = false;
        }
        else if (board.Board[nextRow][nextCol] is object obj && obj.GetType() == typeof(object)) // Si l'objecte està buit
        {
            correctMovement = false;
        }

        return correctMovement;
    }

    ///////////////////////////////////////////////////////////////////     UTILITIES

    private bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < board.Rows && col >= 0 && col < board.Cols;
    }

    private void AttackPlayer(int attackValue)
    {
        int multipliedAttackValue;

        multipliedAttackValue = EnemyAttackMultiplier(attackValue);

        player.Strength -= multipliedAttackValue;
        if (player.Strength < 0)
        {
            player.Life = SubstractLife(-player.Strength);
            player.Strength = 0;
        }
    }

    private int EnemyAttackMultiplier(int value)
    {
        int result = value * enemyMultiplier;
        enemyMultiplier = 1;
        return result;
    }

    private int SubstractLife(int value)
    {
        return player.Life-value < 0 ? 0 : player.Life-value;
    }

    private int SubstractCoins(int objectPrice)
    {
        return player.Coins-objectPrice < 0 ? 0 : player.Coins-objectPrice;
    }

    private int AddLife(int value)
    {
        return player.Life+value > player.MaxLife ? 12 : player.Life+value;
    }

    ///////////////////////////////////////////////////////////////////     MOVING CARD
    // Devuelve true si el juego se tiene que acabar
    private bool MovePlayer(int nextRow, int nextCol) // Moves a player to the next position and handles interactions of the other card
    {
        bool gameOver = false;
        object targetCell = board.Board[nextRow][nextCol];


        // HandleInteraction
        switch (targetCell)
        {
            case Shop shop:
                ShopInteraction(shop);
                break;
            case FishPrize fish:
                FishInteraction(fish);
                break;
            case CoinPrize coin:
                CoinInteraction(coin);
                break;
            case Seagull seagull:
                SeagullInteraction(seagull);
                break;
            case SeaUrchin seaUrchin:
                SeaUrchinInteraction(seaUrchin);
                break;
            case Iceberg iceberg:
                IcebergInteraction();
                break;
            case Ship ship:
                ShipInteraction();
                break;
            case Net net:
                NetInteraction();
                break;
            case Hunter hunter:
                HunterInteraction(hunter);
                break;
            case Whale whale:
                WhaleInteraction(whale);
                break;
            case Pirate pirate:
                PirateInteraction(pirate);
                break;
            case FinalBoss finalboss:
                FinalBossInteraction(finalboss);
                break;
            default:
                Console.WriteLine("Oops something went wrong! You are on an incorrect card class");
                return true;
        }

        if (player.Life == 0)
        {
            gameOver = true;
        }


        // Move player
        board.Board[nextRow][nextCol] = player;
        board.Board[playerRow][playerCol] = new object();
        playerRow = nextRow;
        playerCol = nextCol; 

        return gameOver;
    }

    // Transformem les monedes en punts de força
    private void ShopInteraction(Shop shop)
    {
        player.Strength += player.Coins;
        player.Coins = 0;
        /*if (player.IsSeal)
        {
            player.Life = SubstractLife(shop.ObjectPrice);
            // TODO: quedar-se amb un objecte random 
        }
        else
        {
            player.Coins = SubstractCoins(shop.ObjectPrice);
            // TODO: quedar-se amb un objecte escollit
        }*/
    }


    // Si el player es foca suma vida
    private void FishInteraction(FishPrize prize)
    {
        if (player.IsSeal)
        {
            AddLife(prize.Value);
        }
    }

    // Si el player es humano suma monedas
    private void CoinInteraction(CoinPrize prize)
    {
        if (!player.IsSeal)
        {
            player.Coins += prize.Value;
        }
    }

    // Si el player humano resta monedas
    private void SeagullInteraction(Seagull seagull)
    {
        if (!player.IsSeal)
        {
            player.Coins = SubstractCoins(seagull.Value);
        }
    }


    // Si el player es foca resta vida
    private void SeaUrchinInteraction(SeaUrchin seaUrchin)
    {
        if (player.IsSeal)
        {
            AttackPlayer(seaUrchin.Value);
        }
    }

       // resta -1 a las CoinPrize y FishPrice de la fila siguiente al player
    private void IcebergInteraction()
    {
        int nextRow = playerRow + 1;
        if (nextRow >= board.Rows) return;

        for (int col = 0; col < board.Cols; col++)
        {
            object card = board.Board[nextRow][col];
            if (card is CoinPrize coin)
            {
                coin.Value--;
            }
            else if (card is FishPrize fish)
            {
                fish.Value--;
            }
        }
    }

    // Barco x2 siguente enemigo que te ataque
    private void ShipInteraction()
    {
        if (player.IsSeal)
        {
            enemyMultiplier = 2;
        }
    }

    // Red te obliga a no girar en el siguiente turno solo 
    private void NetInteraction()
    {
        canTransform = false;
    }

    // Si el player es humano: no puedes girar en el siguiente turno, foca: resta el value a la vida
    private void HunterInteraction(Hunter hunter)
    {
        if (player.IsSeal)
        {
            AttackPlayer(hunter.Value);
        }
        else
        {
            canTransform = false;
        }
    }

    // Si es foca -> resta sealValue a la vida, si es humano resta humanValue a la vida
    private void WhaleInteraction(Whale whale)
    {
        if (player.IsSeal)
        {
            AttackPlayer(whale.SealValue);
        }
        else
        {
            AttackPlayer(whale.HumanValue);
        }
    }

    // Si es humano resta humanValue a las monedas. Si es foca hay que restarle attackValue a la vida del player y sumar coinValue a las monedas
    private void PirateInteraction(Pirate pirate)
    {
        if (player.IsSeal)
        {
            AttackPlayer(pirate.AttackValue);
            player.Coins += pirate.SealValue;
        }
        else
        {
            player.Coins = SubstractCoins(pirate.HumanValue);
        }
    }

    // Si es el final boss resta el ataque
    private void FinalBossInteraction(FinalBoss finalBoss)
    {
        AttackPlayer(finalBoss.Value);
    }


    ///////////////////////////////////////////////////////////////////     FRONT CARD
    // Devuelve true si el juego tiene que acabar
    private bool HandleFrontCard()
    {
        bool gameOver = false;
        int frontRow = playerRow + 1;

        if (frontRow >= board.Rows) gameOver = true; // Si hemos llegado al final del tablero hemos acabado el juego
        else 
        {
            object frontCell = board.Board[frontRow][playerCol];

            if (frontCell is FishPrize) // Si la celda delantera es un pez
            {
                DeleteFrontCell(frontRow);
            }
            else if (frontCell is Hunter hunter) // Si la celda delantera es un cazador
            {
                HunterInteraction(hunter); // Hacemos la interacción que pertoque del cazador
                DeleteFrontCell(frontRow);
            }
            else if (frontCell is Whale whale) // Si la celda delantera es ballena...
            {
                WhaleInteraction(whale);
                DeleteFrontCell(frontRow);
            }
            else if (frontCell is Pirate pirate)
            {
                PirateInteraction(pirate);
                DeleteFrontCell(frontRow);
            }

            if (player.Life == 0)    // Si el player se queda sin vida se acaba el juego
            {
                gameOver = true;
            }
        }

        return gameOver;
    }

    private void DeleteFrontCell(int frontRow)
    {
        board.Board[frontRow][playerCol] = new object(); // Borramos la celda de delante
    }
}
