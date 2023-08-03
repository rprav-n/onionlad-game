using Godot;
using System;

public class HUD : Control
{
	private Label scoreLabel;
	private Label timeLabel;

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("Score/ScoreLabel");
		timeLabel = GetNode<Label>("Time/TimeLabel");
	}

	public void UpdateScoreLabel(int score) 
	{
		scoreLabel.Text = score.ToString();
	}
	
	public void UpdateTimeLabel(int time) 
	{
		timeLabel.Text = time.ToString();
	}
}
