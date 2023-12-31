using System.Security;
using Godot;
using System;
using System.Collections.Generic;

public class World : Node2D
{

	// Export variables
	[Export]
	private List<PackedScene> enemies = new List<PackedScene>();
	
	// Node Variables
	private KinematicBody2D player;
	private Position2D startPosition;
	private PackedScene BulletScene;
	private Node2D bulletContainer;
	private Node2D enemyContainer;
	private AnimatedSprite explosion;
	private HUD hud;
	private GameOverScreen gameOverScreen;
	private AudioStreamPlayer shootSound;
	private AudioStreamPlayer explosionSound;
	
	// Normal Variables
	private int score = 0;
	private int time = 30;
	private const int totalLife = 3;
	private int lifes = 3;
	private bool spawnEnemy = true;

	public override void _Ready()
	{
		player = GetNode<KinematicBody2D>("Player");
		startPosition = GetNode<Position2D>("StartPosition");
		BulletScene = GD.Load<PackedScene>("res://scenes/Bullet.tscn");
		bulletContainer = GetNode<Node2D>("BulletContainer");
		enemyContainer = GetNode<Node2D>("EnemySpawner/EnemyContainer");
		explosion = GetNode<AnimatedSprite>("Effects/Explosion");
		explosion.Visible = false;
		explosion.Frame = 0;
		
		hud = GetNode<HUD>("HUD");
		hud.UpdateScoreLabel(0);
		hud.UpdateTimeLabel(time);
		hud.UpdateLifesLabel(totalLife);
		gameOverScreen = GetNode<GameOverScreen>("UILayer/GameOverScreen");
		gameOverScreen.Visible = false;
		
		shootSound = GetNode<AudioStreamPlayer>("ShootSound");
		explosionSound = GetNode<AudioStreamPlayer>("ExplosionSound");
	}
	
	private void _on_DeadZone_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			player.GlobalPosition = startPosition.GlobalPosition;
			lifes--;
			if (lifes == 0) 
			{
				gameOverScreen.Visible = true;
				gameOverScreen.UpdateScore(score);
				spawnEnemy = false;
				player.QueueFree();
			}
			hud.UpdateLifesLabel(lifes);
			
		}
	}
	
	private void _on_DeadZone_area_entered(Area2D area) 
	{
		if (area is Enemy enemy) 
		{
			enemy.QueueFree();
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
		shootSound.Play();
	}
	
	public void _on_EnemySpawner_Timer_timeout() 
	{
		if (!spawnEnemy) 
		{
			return;
		}
		Random rand = new Random();
		var randomEnemyIndex = rand.Next(0, enemies.Count);
		
		var EnemyScene = enemies[randomEnemyIndex];
		
		var enemy = EnemyScene.Instance<Enemy>();
		var randomEnemyXPosition = getRadomValueFromTotalScreenWidth();
		enemy.GlobalPosition = new Vector2(randomEnemyXPosition, 0);
		enemyContainer.AddChild(enemy);
		enemy.Connect("enemyDied", this, "_on_Sushi_enemyDied");
	}
	
	private int getRadomValueFromTotalScreenWidth() 
	{
		Random rand = new Random();
		var screenSize = GetViewportRect().Size;
		return rand.Next(10, (int)screenSize.x - 10);
	}
	
	private void _on_Sushi_enemyDied(Vector2 enemyDiedPosition) 
	{
		explosion.GlobalPosition = enemyDiedPosition;
		explosion.Visible = true;
		explosion.Play("poof");
		score++;
		time += 2;
		explosionSound.Play();
		hud.UpdateTimeLabel(time);
		hud.UpdateScoreLabel(score);
	}
	
	private void _on_Explosion_animation_finished() 
	{
		explosion.Visible = false;
		explosion.Stop();
	}
	
	private void _on_WorldTimer_timeout() 
	{
		if (!gameOverScreen.Visible) 
		{
			time--;	
		}
		
		if (time < 0) 
		{
			gameOverScreen.Visible = true;
		}
		hud.UpdateTimeLabel(time);
	}

}
