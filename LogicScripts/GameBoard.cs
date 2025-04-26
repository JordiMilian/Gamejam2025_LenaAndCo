using System;
using System.Collections.Generic;

public class GameBoard
{
    public List<List<object>> Board;
    private int rows;
    private int cols;
    private bool dealerPlaced = false;
    private Random random = new Random(); 
    
    public int Rows
    {
        get { return rows; }
    }

    public int Cols
    {
        get { return cols; }
    }

    public GameBoard(Player player, int numRows, int numCols = 4, int dealerPrice = 5, int coinPrize = 3, int fishPrize = 3)
    {
        rows = numRows;
        cols = numCols;
        Board = new List<List<object>>(rows);

        for (int i = 0; i < rows; i++)
        {
            Board.Add(new List<object>(new object[cols]));
        }

        InitializeBoard(player, dealerPrice, coinPrize, fishPrize);
    }

    private void InitializeBoard(Player player, int dealerPrice, int coinPrize, int fishPrize)
    {
        Board[0][0] = player;
        Board[rows - 1][cols - 1] = new Monster(10);

        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!dealerPlaced && random.NextDouble() < 0.2) 
                {
                    Board[i][j] = new Dealer(dealerPrice);
                    dealerPlaced = true;
                }
                else
                {
                    double choice = random.NextDouble();
                    if (choice < 0.5)
                    {
                        if (random.Next(0, 2) == 0)
                            Board[i][j] = new CoinPrize(coinPrize);
                        else
                            Board[i][j] = new FishPrize(fishPrize);
                    }
                    else
                    {
                        Board[i][j] = new Monster(5);
                    }
                }
            }
        }

        if (!dealerPlaced)
        {
            int randomRow = random.Next(1, rows - 1);
            int randomCol = random.Next(0, cols);
            Board[randomRow][randomCol] = new Dealer(100);
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
                        Console.Write("P ");
                    else if (item is Monster)
                        Console.Write("M ");
                    else if (item is CoinPrize)
                        Console.Write("C ");
                    else if (item is FishPrize)
                        Console.Write("F ");
                    else if (item is Dealer)
                        Console.Write("D ");
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
