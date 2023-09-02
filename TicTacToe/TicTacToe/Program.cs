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
			Console.Clear();
			
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
				board.PrintBoard(_currentBoard);
				
				ConsoleHelper.PrintBlank();
				await ActivePlayer.Play(_currentBoard);

				Player winner = GameManager.CheckWinCondition(_currentBoard, ActivePlayer);

				if (winner != null)
				{
					InitiateEndGame(board, winner);
				}
				else
				{
					EndTurn(Player1, Opponent1);
				}
			}
		}
	}

	static void InitiateEndGame(BoardManager board, Player player)
	{
		Console.Clear();
		board.PrintBoard(_currentBoard);
		
		ConsoleHelper.PrintBlank();
		if (player.Symbol == 'D')
		{
			// Print the draw in colored text
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("Draw!");
			Console.ResetColor();
			
			SelectEndingAction();
			return;
		}
		// Print the winner in colored text
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"{player.Symbol} wins!");
		Console.ResetColor();
		
		SelectEndingAction();
	}
	
	static void SelectEndingAction()
	{
		bool validSelection = false;
		
		ConsoleHelper.PrintBlank();
		Console.WriteLine("Choose what happens next:");
		ConsoleHelper.PrintBlank();
		Console.WriteLine("1. Play again");
		Console.WriteLine("2. Quit");
		do
		{
			switch (GenericReadLine.TryReadLine<int>())
			{
				case 1: // Restart program
					validSelection = true;
					_loopGame = false;
					break;
				case 2: // Quit program
					validSelection = true;
					_loopGame = false;
					_loopMain = false;
					break;
				default: // Invalid selection
					break;
			}
		} while (!validSelection);
	}
	
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

	static void EndTurn(Player player1, Player player2)
	{
		ActivePlayer = ActivePlayer == player1 ? player2 : player1;
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
		Player player1 = new Player('X');
		Player player2 = new Player('O');
		Player currentPlayer = player1;
		// Run the matches
		for (int i = 0; i < matchCount; i++)
		{
			// Reset the board to empty spaces, then randomize it
			board.ResetBoard(ref dummyBoard);
			board.RandomizeBoard(ref dummyBoard, player1, player2);

			GameManager.CheckWinCondition(dummyBoard, currentPlayer);
		}
		GameManager.PrintScores(PlayerChar, OpponentChar);
	}
}