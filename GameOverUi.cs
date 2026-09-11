using Godot;
using System;

public partial class GameOverUi : CanvasLayer
{
	private Button _restartButton;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		_restartButton = FindChild("Button", true, false) as Button;
		_restartButton.Pressed += OnRestartButtonPressed;
	}


	public void ShowGameOver()
	{
		Visible = true;
		GetTree().Paused = true;
	}
	private void OnRestartButtonPressed()
	{
		// Handle restart button press event
		GetTree().Paused = false;
		GetTree().ReloadCurrentScene();
	}
}
