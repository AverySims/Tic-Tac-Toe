namespace TicTacToe;

public class AsciiClass
{
    public void PrintAsciiBoard(char[][] c)
    {
        string board = $" {c[0][0]} | {c[0][1]} | {c[0][2]} \n" +
                       "---+---+---\n" +
                       $" {c[1][0]} | {c[1][1]} | {c[1][2]} \n" +
                       "---+---+---\n" +
                       $" {c[2][0]} | {c[2][1]} | {c[2][2]} \n";
        Console.WriteLine(board);
    }
}