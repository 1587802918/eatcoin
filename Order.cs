using Godot;
using System;

public partial class Order : Node2D
{
	[Export] private Button _yesButton;
	[Export] private Button _noButton;

	public override void _Ready()
	{
		_yesButton.Pressed += OnYesButtonPressed;
		_noButton.Pressed += OnNoButtonPressed;
	}

	private void OnYesButtonPressed()
	{
		QueueFree();
	}
	private void OnNoButtonPressed()
	{
		QueueFree();
	}
}
