using Godot;
using System;

public partial class Coinman : CharacterBody2D
{
	public float StepSize = 80.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 step = Vector2.Zero;

		if(Input.IsActionJustPressed("move_right"))
		{
			step.X = StepSize;
		}
		if(Input.IsActionJustPressed("move_left"))
		{
			step.X = -StepSize;
		}
		if(Input.IsActionJustPressed("move_up"))
		{
			step.Y = -StepSize;
		}
		if(Input.IsActionJustPressed("move_down"))
		{
			step.Y = StepSize;
		}
		if(step != Vector2.Zero && !TestMove(GlobalTransform, step)){
			Position += step;
		}
	}
}
