using Godot;
using System;

public class GameOverScreen : Control
{

	private Label scoreLabel;

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("ScoreLabel");
	}
	
	public void UpdateScore(int score) 
	{
		scoreLabel.Text = "Score: " + score.ToString();
	}
	
	private void _on_MainMenuButton_pressed() 
	{
		GetTree().ChangeScene("res://scenes/StartScreen.tscn");
	}
	
	private void _on_QuitButton_pressed() 
	{
		GetTree().Quit();
	}
}
