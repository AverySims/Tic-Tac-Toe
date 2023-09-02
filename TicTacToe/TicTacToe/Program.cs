using CustomConsole;
using GenericParse;

namespace TicTacToe;

internal class Program
{
	public static char PlayerChar { get; private set; }
	public static char OpponentChar { get; private set; }
	
	public static Player? Player { get; private set; }
	public static PlayerBot? Opponent { get; private set; }
	public static Player? ActivePlayer { get; private set; }
	
	private static char[,] _currentBoard = new char[3,3];

	static bool _loopMain = true;
	static bool _loopGame = true;
	
	static async Task Main(string[] args)
	{
		BoardManager board = new BoardManager();

		while (_loopMain)
		{
			InitializePlayerSymbol();
			Player = new Player(PlayerChar);
			ActivePlayer = Player;
			
			InitializeOpponentSymbol();
			Opponent = new PlayerBot(OpponentChar, 2);
			
			board.ResetBoard(ref _currentBoard);
			while (_loopGame)
			{
				Console.Clear();
				board.PrintAsciiBoard(_currentBoard);
				
				ConsoleHelper.PrintBlank();
				PrintPositionLayout();
				
				await ActivePlayer.Play(_currentBoard);
				
				//if (GameManager.CheckWinCondition(_currentBoard,))

				EndTurn();

			}
		}
	}
	
	/// <summary>
	/// Initializes the player's symbol to any single character
	/// </summary>
	static void InitializePlayerSymbol()
	{
		Console.Write("Choose your symbol (e.g., 'X', 'O', '$'): ");
		PlayerChar = GenericReadLine.TryReadLine<char>();
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
	
	/* 
	static void PlayMove()
	{
		ConsoleHelper.PrintBlank();
		Console.Write($"{ActivePlayer.Symbol}'s turn. Enter a number: ");
		while (true)
		{
			int position = GenericReadLine.TryReadLine<int>();
			if (position > 0 && position < 10)
			{
				// Assigning the row and column to match a number pad's layout
				 // 7 8 9
				 // 4 5 6
				 // 1 2 3 
	
				int row = 2 - (position - 1) / 3;
				int col = (position - 1) % 3;
			
				if (_currentBoard[row, col] != PlayerChar && _currentBoard[row, col] != OpponentChar)
				{
					_currentBoard[row, col] = ActivePlayer.Symbol;
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
	 */

	static void PrintPositionLayout()
	{
		Console.WriteLine(" 7   8   9 \n" +
		                  " 4   5   6 \n" +
		                  " 1   2   3 ");
	}

	static void EndTurn()
	{
		ActivePlayer = ActivePlayer == Player ? Opponent : Player;
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