using Godot;
using System;

public class Enemy : Area2D
{
	
	[Signal]
	private delegate void enemyDied(Vector2 enemyDiedLocation);
	
	[Export]
	private int speed = 100;

	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		var newPosition = new Vector2(0, speed * delta);
		this.GlobalPosition += newPosition;	
	}
	
	private void _on_Enemy_area_entered(Area2D area) 
	{
		if (area is Bullet bullet) 
		{
			
			this.QueueFree();
			bullet.QueueFree();
			this.EmitSignal("enemyDied", GlobalPosition);
		}
	}
	
	
}
