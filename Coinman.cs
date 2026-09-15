using Godot;
using System;

public partial class Coinman : CharacterBody2D
{
	private AnimatedSprite2D _sprite;

	[Export] public float Speed = 300.0f;

	[Export] public float CellSize = 80.0f;

	private Vector2 _moveDirection = Vector2.Zero;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("CoinMan");
	}

	public override void _PhysicsProcess(double delta)
	{

		Vector2 nextDir = Vector2.Zero;
		if (Input.IsActionJustPressed("move_right"))
		{
			nextDir = Vector2.Right;
		}
		else if (Input.IsActionJustPressed("move_left"))
		{
			nextDir = Vector2.Left;
		}
		else if (Input.IsActionJustPressed("move_up"))
		{
			nextDir = Vector2.Up;
		}
		else if (Input.IsActionJustPressed("move_down"))
		{
			nextDir = Vector2.Down;
		}
		if (nextDir != Vector2.Zero)
		{
			_moveDirection = nextDir;
			UpdateSpriteOrientation(_moveDirection);
			Position = (Position - Vector2.One * (CellSize * 0.5f)).Snapped(Vector2.One * CellSize) + Vector2.One * (CellSize * 0.5f); ;

		}
		if (_moveDirection != Vector2.Zero)
		{
			Velocity = _moveDirection * Speed;
			MoveAndSlide();
			if (GetSlideCollisionCount() > 0)
			{
				_moveDirection = Vector2.Zero;
				Velocity = Vector2.Zero;
				Position = (Position - Vector2.One * (CellSize * 0.5f)).Snapped(Vector2.One * CellSize) + Vector2.One * (CellSize * 0.5f); ;
			}
		}
	}

	private void UpdateSpriteOrientation(Vector2 dir)
	{
		if (_moveDirection.X < 0)
		{
			_sprite.FlipH = true;
			_sprite.Rotation = 0;
		}
		else
		{
			_sprite.FlipH = false;
			_sprite.Rotation = dir.Angle();
		}

	}
}
