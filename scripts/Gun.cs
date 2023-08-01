using Godot;
using System;

public class Gun : Node2D
{
	// Node Variables
	private Sprite sprite;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite>("Sprite");
	}
	
	public void FlipGun(bool flag) 
	{
		sprite.FlipV = flag;
	}
}
