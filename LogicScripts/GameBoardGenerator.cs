using System;
using System.Collections.Generic;

public class GameBoard
{
    private List<List<object>> board;
    private int rows;
    private int cols;
    private bool dealerPlaced = false;
    private Random random = new Random();

    public GameBoard(int numRows, int numCols = 4, int playerLife = 20, int dealerPrice = 5, int coinPrize = 3, int fishPrize = 3)
    {
        rows = numRows;
        cols = numCols;
        board = new List<List<object>>(rows);

        for (int i = 0; i < rows; i++)
        {
            board.Add(new List<object>(new object[cols]));
        }

        InitializeBoard(playerLife, dealerPrice, coinPrize, fishPrize);
    }

    private void InitializeBoard(int playerLife, int dealerPrice, int coinPrize, int fishPrize)
    {
        board[0][0] = new Player(playerLife);
        board[rows - 1][cols - 1] = new Monster(10);

        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (!dealerPlaced && random.NextDouble() < 0.2) 
                {
                    board[i][j] = new Dealer(dealerPrice);
                    dealerPlaced = true;
                }
                else
                {
                    double choice = random.NextDouble();
                    if (choice < 0.5)
                    {
                        if (random.Next(0, 2) == 0)
                            board[i][j] = new CoinPrize(coinPrize);
                        else
                            board[i][j] = new FishPrize(fishPrize);
                    }
                    else
                    {
                        board[i][j] = new Monster(5);
                    }
                }
            }
        }

        if (!dealerPlaced)
        {
            int randomRow = random.Next(1, rows - 1);
            int randomCol = random.Next(0, cols);
            board[randomRow][randomCol] = new Dealer(100);
        }
    }

    public void DisplayBoard()
    {
        for (int i = 0; i < board.Count; i++)
        {
            for (int j = 0; j < board[i].Count; j++)
            {
                object item = board[i][j];
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
