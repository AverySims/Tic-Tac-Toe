using CustomConsole;

namespace TicTacToe;

public class PlayerBot : Player
{
	public double PlayDelay { get; private set; }
	
	public PlayerBot(char symbol, double delay) : base(symbol)
	{
		PlayDelay = delay * 1000;
	}
	
	private List<int> _playedPositions = new List<int>();
	
	private readonly Random _random = new Random();
	
	public void ResetPlayedPositions()
	{
		_playedPositions.Clear();
	}
	
	public override async Task Play(char[,] board)
	{
		Console.WriteLine($"{Symbol}'s turn. Waiting for opponent to play...");
		while (true)
		{
			int position = _random.Next(1, 9);
			
			Console.WriteLine(position);
			
			if (!_playedPositions.Contains(position))
			{
				_playedPositions.Add(position);
			}
			else
			{
				continue;
			}
			
			if (position is > 0 and < 10)
			{
				
				int row = AssignRow(position, true);
				int col = AssignCollumn(position);
			
				await Task.Delay((int)PlayDelay);
				
				if (board[row, col] == ' ')
				{
					board[row, col] = Symbol;
					break;
				}
				else
				{
					//Console.Write("Position is already taken. Select another position: ");
				}
			}
			else
			{
				//Console.Write("Please enter a number between 1 to 9: ");
			}
		}
	}
}