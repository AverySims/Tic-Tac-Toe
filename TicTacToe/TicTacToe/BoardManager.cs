﻿using CustomConsole;

namespace TicTacToe;

public class BoardManager
{
	/// <summary>
	/// Print the board to the console with the states of each cell
	/// </summary>
	/// <param name="board">2D array representing board state</param>
	public void PrintBoard(char[,] board)
	{
		string a = $" {board[0,0]} | {board[0,1]} | {board[0,2]} ";
		string b = $" {board[1,0]} | {board[1,1]} | {board[1,2]} ";
		string c = $" {board[2,0]} | {board[2,1]} | {board[2,2]} ";
		string d = "---+---+---";

		string[] board1 = {a, d, b, d, c};
		
		Console.WriteLine("Look at your numpad to see the board positions.");
		
		ConsoleHelper.PrintBlank();
		foreach (var row in board1)
		{
			Console.WriteLine(row);
		}
	}
	
	/// <summary>
	/// Reset the board to empty spaces. It does not re-print the board.
	/// </summary>
	/// <param name="board">Reference to a 2D array representing board state</param>
	public void ResetBoard(ref char[,] board)
	{
		// Reset the board to empty spaces
		for (int row = 0; row < board.GetLength(0); row++)
		{
			for (int col = 0; col < board.GetLength(1); col++)
			{
				board[row, col] = ' ';
			}
		}
	}
	
	/// <summary>
	/// Randomize the board to X's and O's
	/// </summary>
	/// <param name="board">Reference to a 2D array representing board state</param>
	/// <param name="p1">Representation of player 1</param>>
	/// <param name="p2">Representation of player 2</param>>
	public void RandomizeBoard(ref char[,] board, char p1, char p2)
	{
		// Randomize the board to X's and O's
		Random random = new Random();
		for (int row = 0; row < board.GetLength(0); row++)
		{
			for (int col = 0; col < board.GetLength(1); col++)
			{
				board[row, col] = random.Next(-1, 1) != 0 ? p1 : p2;
			}
		}
	}
}