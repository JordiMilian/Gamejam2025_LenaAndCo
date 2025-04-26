using System;
using System.Collections.Generic;

public class GameBoard
{
    public List<List<object>> Board;
    private int rows;
    private int cols;
    private bool shopPlaced = false;
    private Random random = new Random();

    public int Rows => rows;
    public int Cols => cols;

    public GameBoard(Player player, int numRows, int numCols = 4, int dealerPrice = 5, int prizeValue = 3)
    {
        rows = numRows;
        cols = numCols;
        Board = new List<List<object>>(rows);

        for (int i = 0; i < rows; i++)
        {
            Board.Add(new List<object>(new object[cols]));
        }

        InitializeBoard(player, dealerPrice, prizeValue);
    }

    private void InitializeBoard(Player player, int dealerPrice, int prizeValue)
    {
        // Fila 0
        Board[0][0] = player;
        for (int j = 1; j < cols; j++)
        {
            Board[0][j] = new object();
        }

        // Fila final
        int bossCol = random.Next(cols);
        Board[rows - 1][bossCol] = new FinalBoss();

        for (int i = 1; i < rows - 1; i++)
        {
            List<int> availableCols = new List<int>();
            for (int j = 0; j < cols; j++) availableCols.Add(j);
            Shuffle(availableCols);

            // Insertar mínimo uno de cada tipo por fila
            PlaceInRow(i, availableCols, prizeValue);

            // Rellenar huecos restantes con objetos aleatorios
            for (int j = 0; j < cols; j++)
            {
                if (Board[i][j] == null)
                {
                    Board[i][j] = RandomObject(prizeValue, dealerPrice, i, j);
                }
            }
        }

        // Si no se ha puesto shop aún, ponerlo
        if (!shopPlaced)
        {
            int randomRow = random.Next(1, rows - 1);
            int randomCol = random.Next(0, cols);
            Board[randomRow][randomCol] = new Shop(dealerPrice);
        }
    }

    private void PlaceInRow(int row, List<int> colsAvailable, int prizeValue)
    {
        List<Func<object>> mandatoryObjects = new List<Func<object>>()
        {
            () => new CoinPrize(prizeValue),
            () => new FishPrize(prizeValue),
            () => new Seagull(2),
            () => new SeaUrchin(2),
            () => new Iceberg(),
            () => new Ship(),
            () => new Net(),
            () => new Hunter(),
            () => new Whale(),
            () => new Pirate()
        };

        foreach (var createObject in mandatoryObjects)
        {
            if (colsAvailable.Count == 0) break;
            int col = colsAvailable[0];
            colsAvailable.RemoveAt(0);

            if (row == 1 && col == 0) // Segunda fila, columna delante del jugador
            {
                object obj = createObject();
                if (obj is FishPrize || obj is Whale || obj is Hunter || obj is Pirate)
                {
                    continue; // Saltar si es uno prohibido delante del player
                }
            }

            Board[row][col] = createObject();
        }
    }

    private object RandomObject(int prizeValue, int dealerPrice, int row, int col)
    {
        if (!shopPlaced && random.NextDouble() < 0.1)
        {
            shopPlaced = true;
            return new Shop(dealerPrice);
        }

        object obj;
        do
        {
            double r = random.NextDouble();
            if (r < 0.2) obj = new CoinPrize(prizeValue);
            else if (r < 0.4) obj = new FishPrize(prizeValue);
            else if (r < 0.5) obj = new Seagull(2);
            else if (r < 0.6) obj = new SeaUrchin(2);
            else if (r < 0.7) obj = new Iceberg();
            else if (r < 0.8) obj = new Ship();
            else if (r < 0.85) obj = new Net();
            else if (r < 0.9) obj = new Hunter();
            else if (r < 0.95) obj = new Whale();
            else obj = new Pirate();
        }
        while (row == 1 && col == 0 && (obj is FishPrize || obj is Whale || obj is Hunter || obj is Pirate));

        return obj;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = random.Next(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void DisplayBoard()
    {
        for (int i = 0; i < Board.Count; i++)
        {
            for (int j = 0; j < Board[i].Count; j++)
            {
                object item = Board[i][j];
                if (item != null)
                {
                    if (item is Player)
                        Console.Write("Pl ");
                    else if (item is FinalBoss)
                        Console.Write("FB ");
                    else if (item is Shop)
                        Console.Write("Ti ");
                    else if (item is CoinPrize)
                        Console.Write("Mo ");
                    else if (item is FishPrize)
                        Console.Write("Pe ");
                    else if (item is Seagull)
                        Console.Write("Ga ");
                    else if (item is SeaUrchin)
                        Console.Write("Er ");
                    else if (item is Iceberg)
                        Console.Write("Ic ");
                    else if (item is Ship)
                        Console.Write("Ba ");
                    else if (item is Net)
                        Console.Write("Re ");
                    else if (item is Hunter)
                        Console.Write("Ca ");
                    else if (item is Whale)
                        Console.Write("Or ");
                    else if (item is Pirate)
                        Console.Write("Pi ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine();
        }
    }
}
