using Godot;
using System;

public class StartScreen : Control
{
	
	private void _on_StartButton_pressed() 
	{
		GetTree().ChangeScene("res://scenes/World.tscn");
	}
	
	private void _on_QuitButton_pressed() 
	{
		GetTree().Quit();
	}

}
