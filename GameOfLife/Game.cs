namespace GameOfLife;

/* Реализуйте игру в жизнь на прямоугольном конечном поле.
 
 На каждом ходе клетка меняет свое состояние по таким правилам:
 1. Если у нее менее 2 живых соседей или более трех живых — она становится мертвой (false).
 2. Если ровно 3 живых соседа, то клетка становится живой (true)
 3. Если ровно 2 живых соседа, то клетка сохраняет своё состояние.

 У каждой неграничной клетки есть 8 соседей (в том числе по диагонали)

Работу над игрой постройте итеративно в стиле TDD:
    1. Сначала напишите какой-нибудь простейший тест в соседнем файле GameTest.cs. Тест должен быть красным.
    То есть должен проверять ещё нереализованное требование.
    2. Только потом напшишите простейшую реализацию, которая делает тест зеленым. 
    Не старайтесь реализовать всю логику, просто сделайте тест зеленым как можно быстрее.
    3. Повторяйте процесс, пока ещё можете придумать новые красные тесты.

 На каждый шаг (тест и реализация) у вас должно уходить не более 5 минут.
 Если вы не успели поднять тест за 5 минут — удалите этот тест и придумайте тест попроще.
 Засекайте время таймером на телефоне.

 После каждого шага (тест или реализация) меняйте активного человека за клавиатурой.

 Начните с простейших тестов. 

 Проект настроен так, что при каждой сборке запускаются все тесты и отчет выводится на консоль
*/
public class Game
{
	public static bool[,] NextStep(bool[,] field)
	{
		var xLength = field.GetLength(0);
		var yLength = field.GetLength(1);
		var updatedField = new bool[xLength, yLength];
		for (var i = 0; i < xLength; i++)
		{
			for (var j = 0; j < yLength; j++)
			{
				var alive = CountSells(field, i, j, yLength, xLength);
				if (alive is < 2 or > 3)
				{
					updatedField[i, j] = false;
				}
				else if (alive == 3)
				{
					updatedField[i, j] = true;
				}
				else
				{
					updatedField[i, j] = field[i, j];
				}
			}
		}
		return updatedField;
	}

	private static bool CheckIfSell(int i, int j,  int xLength, int yLength)
	{
		return i < xLength && i >= 0 && j < yLength && j >= 0;
	}

	private static int CountSells(bool[,] field, int x, int y, int xLength, int yLength)
	{
		var alive = 0;
		for (var i = -1; i <= 1; i++)
		{
			for (var j = -1; j <= 1; j++)
			{
				if ((i != 0 || j != 0) && CheckIfSell(x + j, y + i, xLength, yLength) && field[x + j, y + i])
				{
					alive++;
				}
			}
		}

		return alive;
	}
}