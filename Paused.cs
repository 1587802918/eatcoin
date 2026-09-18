using Godot;
using System;

public partial class Paused : CanvasLayer
{
	private Button _continueButton;
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
		Visible = false;
		_continueButton = FindChild("Continue") as Button;
		_continueButton.Pressed += ContinueGame;

	}
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			GD.Print("检测到按键，当前暂停状态: " + GetTree().Paused);
			GetTree().Paused = true;
			Visible = true;
		}
	}

	private void ContinueGame()
	{
		GetTree().Paused = false;
		Visible = false;
	}
}
