using NUnit.Framework;

namespace GameOfLife;

[TestFixture]
public class GameTests
{
	[Test]
	public void NothigChanges()
	{
		var field = new bool[3, 3];
		field[1, 1] = true;
		field[1, 2] = true;
		field[2, 0] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(result[0,0], false);
	}
	[Test]
	public void TooMuchAlive()
	{
		var field = new bool[3, 3];
		field[0, 1] = true;
		field[0, 2] = true;
		field[0, 0] = true;
		field[2, 0] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(false, result[1,1]);
	}
	[Test]
	public void Corner()
	{
		var field = new bool[3, 3];
		field[0, 1] = true;
		field[1, 1] = true;
		field[1, 0] = true;
		field[2, 0] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(true, result[0,0]);
	}
	
	[Test]
	public void Border()
	{
		var field = new bool[3, 3];
		field[0, 0] = true;
		field[0, 2] = true;
		field[1, 1] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(true, result[0,1]);
	}
	
	[Test]
	public void TwoAlive()
	{
		var field = new bool[3, 3];
		field[0, 0] = true;
		field[0, 2] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(field[0,1], result[0,1]);
	}
	
	[Test]
	public void AllNeighboursAlive()
	{
		var field = new bool[3, 3];
		field[0, 0] = true;
		field[0, 1] = true;
		field[0, 2] = true;
		field[1, 0] = true;
		field[1, 2] = true;
		field[2, 0] = true;
		field[2, 1] = true;
		field[2, 2] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(false, result[1,1]);
	}
	
	[Test]
	public void DeadCity()
	{
		var field = new bool[3, 3];
		var result = Game.NextStep(field);
		Assert.AreEqual(false, result[1,1]);
	}
	
	[Test]
	public void OneAlive()
	{
		var field = new bool[3, 3];
		field[0, 0] = true;
		var result = Game.NextStep(field);
		Assert.AreEqual(false, result[1,1]);
	}
}
