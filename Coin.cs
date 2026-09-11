using Godot;
using System;

public partial class Coin : Area2D
{
	public int Value = 1;

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Coinman coinman)
		{
			//coinman.AddScore(Value);
			QueueFree();
		}
	}
}
