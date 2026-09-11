using Godot;
using System;

public partial class Coinman : CharacterBody2D
{
	public float StepSize = 80.0f;
	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("CoinMan");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 step = Vector2.Zero;

		if(Input.IsActionJustPressed("move_right"))
		{
			step.X = StepSize;
		}
		else if(Input.IsActionJustPressed("move_left"))
		{
			step.X = -StepSize;
		}
		else if(Input.IsActionJustPressed("move_up"))
		{
			step.Y = -StepSize;
		}
		else if(Input.IsActionJustPressed("move_down"))
		{
			step.Y = StepSize;
		}
		if(step != Vector2.Zero && !TestMove(GlobalTransform, step)){
			Position += step;
			if(step.X < 0)
			{
				_sprite.FlipH = true;
				_sprite.Rotation = 0;
			}
			else {
				_sprite.FlipH = false;
				_sprite.Rotation = step.Angle();	
			}
		}
	}
}
