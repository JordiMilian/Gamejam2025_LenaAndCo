using System;

public class Program
{
    public static void Main(string[] args)
    {
        GameBoard gameBoard = new GameBoard(6);

        gameBoard.DisplayBoard();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
