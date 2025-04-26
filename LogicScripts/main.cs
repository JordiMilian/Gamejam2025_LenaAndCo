using System;

public class Program
{
    public static void Main(string[] args)
    {
        bool lost;
        Player player = new Player();
        GameBoard gameBoard = new GameBoard(player, 5);;
        
        gameBoard.DisplayBoard();

        GameHandler gameHandler = new GameHandler(gameBoard, player);
        lost = gameHandler.Play();

        if (lost)
        {
            Console.WriteLine("\nOhh you lost... what a dumbass");
        }
        else 
        {
            Console.WriteLine("\nYOU WON, but you're still a dumbass");
        }
    }
}
