using Godot;
using System;

public partial class Main : Node2D
{
	[Export] private PackedScene _coinScene;
	[Export] private PackedScene _orderScene;

	private Timer _orderTimer;
	private readonly RandomNumberGenerator _rng = new RandomNumberGenerator();


	public override void _Ready()
	{
		_rng.Randomize();
		_orderTimer = new Timer();
		_orderTimer.OneShot = true;
		_orderTimer.Timeout += OnOrderTimerTimeout;
		AddChild(_orderTimer);
		StartNextOrderTimer();

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

	private void OnOrderTimerTimeout()
	{
		SpawnOrder();
		StartNextOrderTimer();
	}

	private void SpawnOrder()
	{
		Node orderInstance = _orderScene.Instantiate();
		AddChild(orderInstance);
	}
	private void StartNextOrderTimer()
	{
		float nextWaitTime = _rng.RandfRange(10.0f, 30.0f);
		_orderTimer.WaitTime = nextWaitTime;
		_orderTimer.Start();

	}

}
