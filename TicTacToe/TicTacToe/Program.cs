namespace TicTacToe
{
	internal class Program
	{
		public char Player { get; private set; } = ' ';
		public char Opponent { get; private set; } = 'O';
		
		static void	Main(string[] args)
		{
			AsciiClass ascii = new AsciiClass();
			
			char[] a = {'X', ' ', ' ' };
			char[] b = {' ', 'O', ' ' };
			char[] c = {' ', ' ', 'X' };
			char[][] board = {a, b, c};

			ascii.PrintAsciiBoard(board);

			Console.ReadLine();
		}
	}
}