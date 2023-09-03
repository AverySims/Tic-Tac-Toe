using CustomConsole;
using GenericParse;

namespace TicTacToe;

internal class Program
{
	public static char PlayerChar { get; private set; }
	public static char OpponentChar { get; private set; }

	public static Player? player1;
	public static PlayerBot? player2;
	public static Player? currentPlayer;
	
	private static char[,] _currentBoard = new char[3,3];

	static bool _loopMain = true;
	static bool _loopGame = true;
	
	static async Task Main(string[] args)
	{
		BoardManager board = new BoardManager();
		player1 = new Player('X');
		player2 = new PlayerBot('O', 0.5);
		
		while (_loopMain)
		{
			// setting the active player
			currentPlayer = player1;
			
			// resetting the bot played positions
			player2.ResetPlayedPositions();
			
			// Initializing player symbols
			Console.Clear();
			InitializePlayerSymbol();
			player1.Symbol = PlayerChar;
			
			InitializeOpponentSymbol();
			player2.Symbol = OpponentChar;
			
			board.ResetBoard(ref _currentBoard);
			
			_loopGame = true;
			
			while (_loopGame)
			{
				Console.Clear();
				board.PrintBoard(_currentBoard);
				
				ConsoleHelper.PrintBlank();
				await currentPlayer.Play(_currentBoard);

				Player winner = GameManager.CheckWinCondition(_currentBoard, currentPlayer);

				if (winner != null)
				{
					InitiateEndGame(board, winner);
				}
				else
				{
					EndTurn();
				}
			}
		}
	}
	
	/// <summary>
	/// Handles the assignment of scores and printing of the end game screen
	/// </summary>
	/// <param name="board">Reference to the current board</param>
	/// <param name="winner">Reference to the winning player</param>
	static void InitiateEndGame(BoardManager board, Player winner)
	{
		GameManager.SaveWinner(ref winner);
		
		Console.Clear();
		board.PrintBoard(_currentBoard);
		
		ConsoleHelper.PrintBlank();
		if (winner.Symbol == 'D')
		{
			// Print the draw in colored text
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("Draw!");
			Console.ResetColor();
			
			ConsoleHelper.PrintBlank();
			GameManager.PrintScores(new[]{player1, player2});
			
			SelectEndingAction();
			return;
		}
		
		// Print the winner in colored text
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"{winner.Symbol} wins!");
		Console.ResetColor();
		
		ConsoleHelper.PrintBlank();
		GameManager.PrintScores(new[]{player1, player2});
		
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
					ConsoleHelper.PrintInvalidSelection();
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

	static void EndTurn()
	{
		currentPlayer = currentPlayer == player1 ? player2 : player1;
	}
}