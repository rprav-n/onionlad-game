using Godot;
using System;

public class World : Node2D
{

	// Export variables
	
	// Node Variables
	private KinematicBody2D player;
	private Position2D startPosition;
	private PackedScene BulletScene;
	private Node2D bulletContainer;
	
	// Normal Variables

	public override void _Ready()
	{
		player = GetNode<KinematicBody2D>("Player");
		startPosition = GetNode<Position2D>("StartPosition");
		BulletScene = GD.Load<PackedScene>("res://scenes/Bullet.tscn");
		bulletContainer = GetNode<Node2D>("BulletContainer");
	}
	
	private void _on_DeadZone_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			player.GlobalPosition = startPosition.GlobalPosition;
		}
	}

	public void _on_Player_shootBullet(Vector2 gunPosition, Vector2 lookAtPosition) 
	{
		var newBullet = BulletScene.Instance<Bullet>();
		newBullet.GlobalPosition = gunPosition;
		bulletContainer.AddChild(newBullet);
	
		newBullet.LookAt(lookAtPosition);
		newBullet.Rotate(Mathf.Deg2Rad(90f));
	
		newBullet.direction = lookAtPosition;
		
		var bulletDirection = (gunPosition - lookAtPosition).Normalized();
		newBullet.direction = bulletDirection;
	}
}
