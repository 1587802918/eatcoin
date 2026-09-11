using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public partial class WwMan : CharacterBody2D
{

	[Export] public Node2D Player;
	public float StepSize = 80.0f;
	private AnimatedSprite2D _sprite;
	private double _timer;
	private readonly Vector2[] _directions = new Vector2[]{Vector2.Up,Vector2.Down,Vector2.Left,Vector2.Right};
	private readonly Vector2 _gridOffset = new Vector2(40f, 40f);
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("WWMan");
	}

	public override void _PhysicsProcess(double delta)
	{
		if(Player == null || (_timer += delta) < 0.5)
		{
			return;
		}
		{
			_timer = 0;
			Vector2 start = (GlobalPosition-_gridOffset).Snapped(Vector2.One * StepSize)+_gridOffset;
			Vector2 end = (Player.GlobalPosition-_gridOffset).Snapped(Vector2.One * StepSize)+_gridOffset;
			GD.Print($"start: {start}, end: {end}");
			if(start == end)
			{
				return;
			}
			Vector2 step = Vector2.Zero;
			var queue = new Queue<Vector2>();
			var cameFrom = new Dictionary<Vector2,Vector2>{[start] = start};
			queue.Enqueue(start);
			while(queue.Count > 0)
			{
				var current = queue.Dequeue();
				if(current == end)
				{
					break;
				}
				foreach(var dir in _directions)
				{
					var next = current + dir * StepSize;
					if(next.X < _gridOffset.X || next.Y < _gridOffset.Y || next.X > 720f || next.Y > 1280f)
					{
						continue;
					}

					if(!cameFrom.ContainsKey(next) && (next == end ||!TestMove(new Transform2D(0,current),dir * StepSize)))
					{
						
						cameFrom[next] = current;
						queue.Enqueue(next);
					}
				}
			}
			if(cameFrom.ContainsKey(end))
			{
				var node = end;
				while(cameFrom[node] != start)
				{
					node = cameFrom[node];
				}

				
				step = node - start;
			}
			if(step != Vector2.Zero)
			{
				Position += step;
				if(step.X < 0)
				{
					_sprite.FlipH = true;
					_sprite.Rotation = 0;
				}
				else {
					_sprite.FlipH = false;
				}
			}
		}
	}


}
