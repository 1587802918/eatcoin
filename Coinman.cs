using Godot;
using System;

public partial class Coinman : CharacterBody2D
{

	[Export] public float Speed = 300.0f;

	[Export] public float CellSize = 80.0f;

	[Export] public int DashCells = 3;
	[Export] public float DashMultiplier = 5.0f;

	private AnimatedSprite2D _sprite;


	private Vector2 _moveDirection = Vector2.Zero;

	private float _dashDistance = 0.0f;

	private Vector2 SnappedPosition => (Position - Vector2.One * (CellSize * 0.5f)).Snapped(Vector2.One * CellSize) + Vector2.One * (CellSize * 0.5f);

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("CoinMan");
	}

	public override void _PhysicsProcess(double delta)
	{
		if (_dashDistance <= 0f)
		{
			Vector2 nextDir = Vector2.Zero;
			if (Input.IsActionJustPressed("move_right")) nextDir = Vector2.Right;
			else if (Input.IsActionJustPressed("move_left")) nextDir = Vector2.Left;
			else if (Input.IsActionJustPressed("move_up")) nextDir = Vector2.Up;
			else if (Input.IsActionJustPressed("move_down")) nextDir = Vector2.Down;
			if (nextDir != Vector2.Zero)
			{
				_moveDirection = nextDir;
				if (_moveDirection.X < 0)
				{
					_sprite.FlipH = true;
					_sprite.Rotation = 0;
				}
				else
				{
					_sprite.FlipH = false;
					_sprite.Rotation = _moveDirection.Angle();
				}
				Position = SnappedPosition;
			}

			if (Input.IsActionJustPressed("dash") && _moveDirection != Vector2.Zero)
			{
				_dashDistance = DashCells * CellSize;

			}


		}
		if (_moveDirection != Vector2.Zero)
		{
			float currentSpeed = _dashDistance > 0f ? Speed * DashMultiplier : Speed;
			Velocity = _moveDirection * currentSpeed;
			MoveAndSlide();
			if (_dashDistance > 0f)
			{
				_dashDistance -= currentSpeed * (float)delta;
				if (_dashDistance <= 0f)
				{
					_dashDistance = 0f;
					Position = SnappedPosition;
				}
			}

			if (GetSlideCollisionCount() > 0)
			{
				_dashDistance = 0f;
				_moveDirection = Vector2.Zero;
				Velocity = Vector2.Zero;
				Position = SnappedPosition;
			}
		}
	}


}
