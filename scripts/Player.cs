using Godot;
using System;

public class Player : KinematicBody2D
{
	// Signals
	[Signal]
	private delegate void shootBullet(Vector2 gunPosition, Vector2 lookAtPosition);
	
	// Export variables
	[Export]
	private int gravity = 400;
	
	[Export]
	private int speed = 100;
	
	[Export]
	private int jumpForce = 100;
	
	// Node Variables
	private AnimatedSprite animatedSprite;
	private Gun gun;
	
	// Normal Variables
	private Vector2 velocity = Vector2.Zero;
	private float direction = 0f;
	private const int playerGunGap = 20;

	public override void _Ready()
	{
		base._Ready();
		
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		gun = GetNode<Gun>("Gun");
	}

	public override void _PhysicsProcess(float delta)
	{
		playerGravity(delta);
		playerMovement(delta);
		playerGunMouseMovement();
		playerAnimation();
		playerShoot();
		
		velocity = MoveAndSlide(velocity, Vector2.Up);
	}
	
	private void playerGravity(float delta) 
	{

		velocity += new Vector2(0, gravity * delta);
		if (velocity.y > 500) 
		{
			velocity.y = 500;
		}
	}
	
	private void playerMovement(float delta) 
	{
		
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) 
		{
			velocity.y = -jumpForce;
		}
		
		direction = Input.GetAxis("move_left", "move_right");

		velocity.x = direction * speed;
	}
	
	private void playerAnimation() 
	{
		// Flip direction
		if (direction != 0) 
		{
			animatedSprite.FlipH = direction == -1;	
		}
		
		if (IsOnFloor()) 
		{
			if (direction != 0) 
			{
				animatedSprite.Play("run");
			} else 
			{
				animatedSprite.Play("idle");
			}	
		} else 
		{
			if (velocity.y < 0) 
			{
				animatedSprite.Play("jump");
			} else 
			{
				animatedSprite.Play("fall");
			}	
		}
	}

	private void playerGunMouseMovement()
	{
		Vector2 mousePosition = GetGlobalMousePosition();
		Vector2 playerPosition = GlobalPosition;

		Vector2 direction = (mousePosition - playerPosition).Normalized();

		Vector2 gunPosition = playerPosition + direction * playerGunGap;
		gun.GlobalPosition = gunPosition;
		
		gun.FlipGun(mousePosition < playerPosition);
		
		gun.LookAt(mousePosition);
	}

	public void playerShoot() 
	{
		if (Input.IsActionJustPressed("shoot")) 
		{
			this.EmitSignal("shootBullet", gun.GlobalPosition, GetGlobalMousePosition());
		}
	}

}
