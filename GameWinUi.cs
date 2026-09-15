using Godot;
using System;

public partial class GameWinUi : CanvasLayer
{
	private Button _restartButton;

	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		_restartButton = FindChild("Button", true, false) as Button;
		_restartButton.Pressed += OnRestartButtonPressed;
	}


	public void ShowGameWin()
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
