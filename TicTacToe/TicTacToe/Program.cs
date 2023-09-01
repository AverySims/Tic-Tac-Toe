namespace TicTacToe;

internal class Program
{
	public char PlayerChar { get; private set; } = ' ';
	public char OpponentChar { get; private set; } = 'O';

	private static char[,] currentBoard = new char[3,3];

	static bool _loopMain = true;
	static void	Main(string[] args)
	{
		AsciiClass ascii = new AsciiClass();

		while (_loopMain)
		{
			// Reset the board to empty spaces, then randomize it
			ascii.ResetBoard(ref currentBoard);
			ascii.RandomizeBoard(ref currentBoard);
			ascii.PrintAsciiBoard(currentBoard);
	
			Console.WriteLine("Use your number pad to play.");
			if (int.TryParse(Console.ReadLine(), out int input))
			{
				Console.Clear();
			}
			else
			{
				Console.Clear();
			}
		}
	}
}