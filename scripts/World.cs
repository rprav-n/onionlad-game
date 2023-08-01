using Godot;
using System;

public class World : Node2D
{

	// Export variables
	
	// Node Variables
	private KinematicBody2D player;
	private Position2D startPosition;
	
	// Normal Variables

	public override void _Ready()
	{
		player = GetNode<KinematicBody2D>("Player");
		startPosition = GetNode<Position2D>("StartPosition");
	}
	
	private void _on_DeadZone_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			player.GlobalPosition = startPosition.GlobalPosition;
		}
	}

}
