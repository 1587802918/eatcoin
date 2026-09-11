using Godot;
using System;

public partial class Coinman : CharacterBody2D
{
	public float StepSize = 80.0f;

	public override void _UnhandledInput(InputEvent @event)
	{
		Vector2 step = Vector2.Zero;

		if(Input.IsActionPressed("move_right"))
		{
			step.X = StepSize;
		}
		if(Input.IsActionPressed("move_left"))
		{
			step.X = -StepSize;
		}
		if(Input.IsActionPressed("move_up"))
		{
			step.Y = -StepSize;
		}
		if(Input.IsActionPressed("move_down"))
		{
			step.Y = StepSize;
		}
		if(step != Vector2.Zero){
			Position += step;
		}
	}
}
