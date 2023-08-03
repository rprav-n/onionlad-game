using Godot;
using System;

public class HUD : Control
{
	private Label scoreLabel;
	private Label timeLabel;
	private Label lifesLabel;
	

	public override void _Ready()
	{
		scoreLabel = GetNode<Label>("Score/ScoreLabel");
		timeLabel = GetNode<Label>("Time/TimeLabel");
		lifesLabel = GetNode<Label>("Lifes/LifesLabel");
	}

	public void UpdateScoreLabel(int score) 
	{
		scoreLabel.Text = score.ToString();
	}
	
	public void UpdateTimeLabel(int time) 
	{
		timeLabel.Text = time.ToString();
	}
	
	public void UpdateLifesLabel(int life) 
	{
		lifesLabel.Text = life.ToString();
	}
}
