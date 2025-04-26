using System;

public class GameHandler
{
    private GameBoard board;
    private Player player;
    private int playerRow;
    private int playerCol;

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
        int transformation, nextRow, nextCol; 


        while (!gameOver)
        {
            // Decidir si isFoca o no
            Console.Write("Vols transformar-te? (0 - no, 1 - yes):");
            transformation = Convert.ToInt32(Console.ReadLine());

            if(transformation == 1)
            {
                player.IsSeal = !player.IsSeal;
            }
            
            // Moure el player -> control de les posicions cap a on pot moure etc. 
            while (!correctMovement)
            {
                Console.Write("Selecciona la pròxima fila del player (current: "+playerRow+")");
                nextRow = Convert.ToInt32(Console.ReadLine());
                Console.Write("Selecciona la pròxima columna del player (current: "+playerCol+")");
                nextCol = Convert.ToInt32(Console.ReadLine());
                correctMovement = CheckMovement(nextRow, nextCol);

                // For debuggin purposes :)
                if (correctMovement) {Console.Write("OLEE la posición es correcta\n");}
                else {Console.Write("ayayyyyyy la posición es incorrecta\n");}
            }

            // GESTIONAR MOVIMENT
            
            //  -> si entra amb un dealer -> decidir si comprar objecte o no 
            //                                          -> si està en humà -> si: restar calers, decidir objecte, afegir boost
            //                                          -> si està en foca -> quita vida i s'et dona un objecte random
            //  -> si agafa un prize
            //                    -> si es foca y es pez, se suma a la vida
            //                    -> si es humano y son monedas, se suman las monedas
            //  -> si cau en un monstre -> SAME LOGIC AS SI TÉ DAVANT UN ENEMIC
            //  -> si cau en una casella de terreny
            //              -> si iceberg
            //                      -> si humà -> es baixa les monedes i peixos -1 de la següent filera
            //              -> si barca
            //                      -> si foca -> següent enemic x2 de daño
            //              -> si gaviota
            //                      -> si humà -> - monedes 
            //              -> si medusa
            //                      -> si foca -> treu vida
            //              -> si otro terreno -> no te permite girar 
            //  SI TË DAVANT UN ENEMIC
            //              -> si es caçador 
            //                    -> i persona -> no es pot girar el següent torn
            //                    -> i foca -> lluita
            //              -> si es orca
            //                    -> i persona -> +5 daño
            //                    -> i foca -> +9 daño 
            //              -> si es pirata
            //                    -> i persona -> -2 monedes
            //                    -> i foca -> +3 monedes
            // SI TÉ DAVANT UN PEIX -> desapareix sempre
            
            /*Console.Clear();
            board.DisplayBoard();
            Console.WriteLine($"\nLife: {player.Life}, Coins: {player.Coins}, IsSeal: {player.IsSeal}");
            Console.WriteLine("Move with W/A/S/D. Press Q to quit.");

            ConsoleKey key = Console.ReadKey(true).Key;
            int newRow = playerRow;
            int newCol = playerCol;

            switch (key)
            {
                case ConsoleKey.W: newRow--; break;
                case ConsoleKey.S: newRow++; break;
                case ConsoleKey.A: newCol--; break;
                case ConsoleKey.D: newCol++; break;
                case ConsoleKey.Q: return;
                default: continue;
            }

            if (!IsInBounds(newRow, newCol))
                continue;

            object target = board.GetObjectAt(newRow, newCol);
            HandleInteraction(target);

            // Move player
            board.SetObjectAt(playerRow, playerCol, null);
            board.SetObjectAt(newRow, newCol, player);
            playerRow = newRow;
            playerCol = newCol;

            // Check win or lose
            if (player.Life <= 0)
            {
                Console.Clear();
                Console.WriteLine("You lost! You have no life left.");
                gameOver = true;
            }
            else if (playerRow == board.Rows - 1)
            {
                Console.Clear();
                Console.WriteLine("You reached the final row. You win!");
                gameOver = true;
            }*/
        }

        Console.WriteLine("Game Over. Press any key to exit.");
        Console.ReadKey();
    }

    // Checks if a movement can be performed
    private bool CheckMovement(int nextRow, int nextCol)
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


    /*private void HandleInteraction(object target)
    {
        if (target is Monster monster)
        {
            player.Life -= monster.Damage;
        }
        else if (target is Prize prize)
        {
            if (prize.Type == PrizeType.Fish && player.IsSeal)
            {
                player.Life += prize.Value;
            }
            else if (prize.Type == PrizeType.Coin && !player.IsSeal)
            {
                player.Coins += prize.Value;
            }
        }
        // Dealer logic can be added later if needed
    }*/

    private bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < board.Rows && col >= 0 && col < board.Cols;
    }
}
