using Godot;
using System;

public partial class Monster : CharacterBody2D
{
	int hp = 3;
	private AnimatedSprite2D _animatedSprite;
	float speed = 150f;
	[Export]

	private int direction = -1;
	Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_animatedSprite.Play("Walk");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var mVelocity = Velocity;
		if (!IsOnFloor())
		{
			mVelocity += GetGravity() * (float)delta;
		}

		
		

		if (IsOnWall())
		{
			direction *= -1;
		}

	_animatedSprite.FlipH = direction == -1;

		mVelocity.X = 200* direction;	
		
		Velocity = mVelocity;
		MoveAndSlide();
	}
	
}
