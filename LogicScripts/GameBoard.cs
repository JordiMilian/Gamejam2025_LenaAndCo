using System;
using System.Collections.Generic;

public class GameBoard
{
    public List<List<object>> Board;
    private int rows;
    private int cols;
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

        // Lista de obligatorios a colocar
        List<Func<object>> mandatoryObjects = new List<Func<object>>()
        {
            () => new CoinPrize(),
            () => new FishPrize(),
            () => new Seagull(),
            () => new SeaUrchin(),
            () => new Iceberg(),
            () => new Ship(),
            () => new Net(),
            () => new Hunter(),
            () => new Whale()
        };

        List<(int, int)> availablePositions = new List<(int, int)>();

        // Recoger todas las posiciones válidas
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (i == 1 && j == 0) continue; // Saltar la posición delante del player
                availablePositions.Add((i, j));
            }
        }

        Shuffle(availablePositions);

        // Colocar uno de cada obligatorio en posiciones aleatorias
        foreach (var createObject in mandatoryObjects)
        {
            if (availablePositions.Count == 0) break;
            var (i, j) = availablePositions[0];
            availablePositions.RemoveAt(0);
            Board[i][j] = createObject();
        }

        // Rellenar el resto de huecos
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (Board[i][j] == null)
                {
                    Board[i][j] = RandomObject(i, j);
                }
            }
        }
    }

    private object RandomObject(int row, int col)
    {
        object obj;
        do
        {
            double r = random.NextDouble();
            if (r < 0.1) obj = new Shop();
            else if (r < 0.2) obj = new CoinPrize();
            else if (r < 0.3) obj = new FishPrize();
            else if (r < 0.4) obj = new Seagull();
            else if (r < 0.5) obj = new SeaUrchin();
            else if (r < 0.6) obj = new Iceberg();
            else if (r < 0.7) obj = new Ship();
            else if (r < 0.8) obj = new Net();
            else if (r < 0.9) obj = new Hunter();
            else obj = new Whale();
        }
        while (row == 1 && col == 0 && (obj is FishPrize || obj is Whale || obj is Hunter));

        return obj;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = random.Next(i, list.Count);
            T temp = list[i];
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
