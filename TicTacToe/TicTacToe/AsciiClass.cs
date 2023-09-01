namespace TicTacToe;

public class AsciiClass
{
	public void PrintAsciiBoard(char[,] n)
	{
		string a = $" {n[0,0]} | {n[0,1]} | {n[0,2]} ";
		string b = $" {n[1,0]} | {n[1,1]} | {n[1,2]} ";
		string c = $" {n[2,0]} | {n[2,1]} | {n[2,2]} ";
		string d = "---+---+---";

		string[] board = {a, d, b, d, c};

		foreach (var row in board)
		{
			Console.WriteLine(row);
		}
	}

	public void ResetBoard(ref char[,] n)
	{
		// Reset the board to empty spaces
		for (int row = 0; row < n.GetLength(0); row++)
		{
			for (int col = 0; col < n.GetLength(1); col++)
			{
				n[row, col] = ' ';
			}
		}
	}

	public void RandomizeBoard(ref char[,] n)
	{
		// Randomize the board to X's and O's
		Random random = new Random();
		for (int row = 0; row < n.GetLength(0); row++)
		{
			for (int col = 0; col < n.GetLength(1); col++)
			{
				n[row, col] = random.Next(0, 6) >= 3 ? 'X' : 'O';
			}
		}
	}
}