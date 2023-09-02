using CustomConsole;
using GenericParse;

namespace TicTacToe;

internal class Program
{
	public static char PlayerChar { get; private set; }
	public static char OpponentChar { get; private set; }
	
	public static Player? Player1 { get; private set; }
	public static PlayerBot? Opponent1 { get; private set; }
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
			Player1 = new Player(PlayerChar);
			ActivePlayer = Player1;
			
			InitializeOpponentSymbol();
			Opponent1 = new PlayerBot(OpponentChar, 0.5);
			
			board.ResetBoard(ref _currentBoard);
			
			_loopGame = true;
			
			while (_loopGame)
			{
				Console.Clear();
				board.PrintAsciiBoard(_currentBoard);
				
				ConsoleHelper.PrintBlank();
				await ActivePlayer.Play(_currentBoard);

				Player winner = GameManager.CheckWinCondition(_currentBoard, ActivePlayer);

				if (winner != null)
				{
					InitiateEndGame(winner);
				}
				else
				{
					EndTurn();
				}
			}
		}
	}

	static void InitiateEndGame(Player player)
	{
		if (player.Symbol == 'D')
		{
			Console.WriteLine("Draw!");
			ConsoleHelper.SelectEndingAction(out _loopGame);
			return;
		}
					
		Console.WriteLine($"{player.Symbol} wins!");
		ConsoleHelper.SelectEndingAction(out _loopGame);
	}

	// TODO: Complete the function to allow the user to select an action
	static void SelectEndingAction()
	{
		Console.WriteLine("Choose what happens next:");
		ConsoleHelper.PrintBlank();
		Console.WriteLine("1. Restart program");
		Console.WriteLine("2. Quit program");
	}
	
	/*
	 * 
	static void MatchResult()
	{
		if (winner.Symbol == 'D')
		{
			Console.WriteLine("Draw!");
			break;
		}
					
		Console.WriteLine($"{winner.Symbol} wins!");
		Console.ReadLine();
	}
	 */
	
	/// <summary>
	/// Initializes the player's symbol to any single character
	/// </summary>
	static void InitializePlayerSymbol()
	{
		Console.Write("Choose your symbol (e.g., 'X', 'O', '!'): ");
		PlayerChar = GenericReadLine.TryReadLine<char>();
	}
	
	/// <summary>
	/// Tries to initialize the opponent's symbol to 'O', but defaults to 'X' if the player chose 'O'
	/// </summary>
	static void InitializeOpponentSymbol()
	{
		if (PlayerChar.ToString().ToUpper() == "O")
			OpponentChar = 'X';
		else
			OpponentChar = 'O';
	}

	static void PrintPositionLayout()
	{
		Console.WriteLine(" 7   8   9 \n\n" +
		                  " 4   5   6 \n\n" +
		                  " 1   2   3 ");
	}

	static void EndTurn()
	{
		ActivePlayer = ActivePlayer == Player1 ? Opponent1 : Player1;
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