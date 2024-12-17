namespace GameOfLife;

class Program
{
	public static void Paint(bool[,] field)
	{
		Console.SetCursorPosition(0, 0);
		for (var y = 0; y < field.GetLength(1); y++)
		{
			for (var x = 0; x < field.GetLength(0); x++)
			{
				var symbol = field[x, y] ? '#' : 'x';
				Console.Write(symbol);
			}
			Console.WriteLine();
		}
	}

	static void Main(string[] args)
	{
		var field = new bool[20, 20];
		field[1, 4] = true;
		field[0, 4] = true;
		field[0, 3] = true;
		field[5, 5] = true;
		field[6, 5] = true;
		field[7, 5] = true;
		field[10, 4] = true;
		field[11, 4] = true;
		field[10, 3] = true;
		field[10, 5] = true;
		field[11, 5] = true;
		field[9, 5] = true;
		while (true)
		{
			Paint(field);
			Thread.Sleep(500);
			field = Game.NextStep(field);
		}
	}
}