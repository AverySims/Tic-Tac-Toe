using CustomConsole;
using GenericParse;

namespace TicTacToe;

internal class Program
{
	public static char PlayerChar { get; private set; }
	public static char OpponentChar { get; private set; }
	public static char CurrentPlayer { get; set; }

	private static char[,] _currentBoard = new char[3,3];

	static bool _loopMain = true;
	static bool _gameLoop = true;
	static void	Main(string[] args)
	{
		BoardManager board = new BoardManager();

		while (_loopMain)
		{
			InitializePlayerSymbol();
			InitializeOpponentSymbol();
			
			board.ResetBoard(ref _currentBoard);
			while (_gameLoop)
			{
				// Reset the board to empty spaces, then randomize it
				Console.Clear();
				board.PrintAsciiBoard(_currentBoard);
			
				ConsoleHelper.PrintBlank();
				PrintPositionLayout();
				PlayMove();
			}
		}
	}
	
	/// <summary>
	/// Initializes the player's symbol to any single character
	/// </summary>
	static void InitializePlayerSymbol()
	{
		Console.Write("Choose your symbol (e.g., 'X', 'O', '$'): ");
		char newSymbol = Console.ReadLine().ToCharArray()[0];
		
		PlayerChar = newSymbol;
		
		CurrentPlayer = PlayerChar;
	}
	
	/// <summary>
	/// Tries to initialize the opponent's symbol to 'O', but defaults to 'X' if the player chose 'O'
	/// </summary>
	static void InitializeOpponentSymbol()
	{
		if (PlayerChar.ToString().ToUpper() == "O")
		{
			OpponentChar = 'X';
		}
		else
		{
			OpponentChar = 'O';
		}
	}
	
	static void PlayMove()
	{
		ConsoleHelper.PrintBlank();
		Console.Write($"{CurrentPlayer}'s turn. Enter a number: ");
		while (true)
		{
			int position = GenericReadLine.TryReadLine<int>();
			if (position > 0 && position < 10)
			{
				// Assigning the row and column to match a number pad's layout
				/* 7 8 9
				 * 4 5 6
				 * 1 2 3 */
				int row = 2 - (position - 1) / 3;
				int col = (position - 1) % 3;
			
				if (_currentBoard[row, col] != PlayerChar && _currentBoard[row, col] != OpponentChar)
				{
					_currentBoard[row, col] = CurrentPlayer;
					break;
				}
				else
				{
					ConsoleHelper.PrintInvalidSelection();
				}
			}
			else
			{
				Console.Write("Please enter a number from 1 to 9: ");
			}
		}
		EndTurn();
	}

	static void PrintPositionLayout()
	{
		Console.WriteLine(" 7   8   9 \n" +
		                  " 4   5   6 \n" +
		                  " 1   2   3 ");
	}

	static void EndTurn()
	{
		CurrentPlayer = CurrentPlayer == PlayerChar ? OpponentChar : PlayerChar;
	}
	
	
	
	/// <summary>
	/// Runs a number of random matches and prints the results
	/// </summary>
	/// <param name="board">The active BoardManager instance</param>
	/// <param name="matchCount">Number of matches to run</param>
	private static void RunMatches(BoardManager board, int matchCount = 100)
	{
		// Create a dummy board to test with
		char[,] dummyBoard = new char[3,3];
		
		// Run the matches
		for (int i = 0; i < matchCount; i++)
		{
			// Reset the board to empty spaces, then randomize it
			board.ResetBoard(ref dummyBoard);
			board.RandomizeBoard(ref dummyBoard, PlayerChar, OpponentChar);

			GameManager.SaveWinner(GameManager.CheckWinCondition(dummyBoard, PlayerChar, OpponentChar), PlayerChar,
				OpponentChar);
		}
		GameManager.PrintScores(PlayerChar, OpponentChar);
	}
}