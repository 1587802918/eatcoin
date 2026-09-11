using Godot;
using System;

public partial class Main : Node2D
{
	[Export] private PackedScene _coinScene;

	public override void _Ready()
	{
		for(int i = 0; i < 9; i++)
		{
			for(int j = 0;j < 16; j++)
			{
				SpawnCoin(new Vector2(i*80+40, j*80+40));
			}
		}
	}
	public void SpawnCoin(Vector2 spawnPosition)
	{
		Coin coin = _coinScene.Instantiate<Coin>();
		coin.Position = spawnPosition;
		AddChild(coin);
	}


}
