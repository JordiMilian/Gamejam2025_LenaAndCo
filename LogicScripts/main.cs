using System;

public class Program
{
    public static void Main(string[] args)
    {
        Player player = new Player(20);
        GameBoard gameBoard = new GameBoard(player, 15);;
        
        gameBoard.DisplayBoard();

        GameHandler gameHandler = new GameHandler(gameBoard, player);
        gameHandler.Play();

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
