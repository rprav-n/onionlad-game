using Godot;
using System;

public class GameOverScreen : Control
{

	public override void _Ready()
	{
		
	}
	
	private void _on_MainMenuButton_pressed() 
	{
		GetTree().ChangeScene("res://scenes/StartMenu.tscn");
	}
	
	private void _on_QuitButton_pressed() 
	{
		GetTree().Quit();
	}
}
