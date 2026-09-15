using Godot;
using System;

public partial class Coin : Area2D
{
	public int Value = 1;

	public override void _Ready()
	{
		AddToGroup("coins");
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is Coinman coinman )
		{
			//coinman.AddScore(Value);

			SetDeferred(Area2D.PropertyName.Monitoring, false);

			Hide();
			RemoveFromGroup("coins");
			int remainingCoins = GetTree().GetNodesInGroup("coins").Count;
			if(remainingCoins == 0)
			{
				var gameOverUi = GetTree().Root.FindChild("GameOverUI", true, false) as GameWinUi;
				gameOverUi.ShowGameWin();
			}
			var sfx = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
			sfx.Finished += QueueFree;
			sfx.Play();
			

		}else if(body is WwMan wwman)
		{
			SetDeferred(Area2D.PropertyName.Monitoring, false);
			Hide();
			RemoveFromGroup("coins");
		}
	}

	
}
