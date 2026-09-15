using Godot;
using System;

public partial class GameOver : CanvasLayer
{
	[Export] private Button _restartButton;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		_restartButton.Pressed += OnRestartButtonPressed;
	}
	private void OnRestartButtonPressed()
	{
		// Handle restart button press event
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}


}
