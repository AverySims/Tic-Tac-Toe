using CustomConsole;
using GenericParse;

namespace TicTacToe;

public class Player
{
	public int wins = 0;
	
	public char Symbol { get; set; }
	public Player(char symbol)
	{
		Symbol = symbol;
	}

	public virtual async Task Play(char[,] board)
	{
		await Task.Yield();
		
		Console.Write($"{Symbol}'s turn. Enter a position (1-9): ");
		while (true)
		{
			int position = GenericReadLine.TryReadLine<int>();
			if (position is > 0 and < 10)
			{
				int row = AssignRow(position, true);
				int col = AssignCollumn(position);
			
				if (board[row, col] == ' ')
				{
					board[row, col] = Symbol;
					break;
				}
				else
				{
					Console.Write("Position is already taken. Select another position: ");
				}
			}
			else
			{
				Console.Write("Please enter a number between 1 to 9: ");
			}
		}
	}
	
	protected int AssignRow(int position, bool flipKeys)
	{
		/* flipKeys = true:
		 * 7 8 9
		 * 4 5 6
		 * 1 2 3 */
		
		/* flipKeys = false:
		 * 1 2 3
		 * 4 5 6
		 * 7 8 9 */
		
		return flipKeys ? 2 - (position - 1) / 3 : (position - 1) / 3;
	}
	
	protected int AssignCollumn(int position)
	{
		return (position - 1) % 3;
	}
}