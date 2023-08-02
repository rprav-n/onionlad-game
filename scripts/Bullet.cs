using Godot;
using System;

public class Bullet : Area2D
{
	
	[Export]
	private int speed = 100;

	public Vector2 direction = new Vector2(0, 0);
	
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		var newPosition = -direction * speed * delta;
		this.GlobalPosition += newPosition;
	}

}
