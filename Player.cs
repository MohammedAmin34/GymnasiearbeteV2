using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private bool _isAttacking = false;
	private double _attackTimer = 0.0;
	private int JumpsLeft = 0; 

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		var mVelocity = Velocity;
		mVelocity.X = 0;

		if (IsOnFloor())
		{
			JumpsLeft = 1;
		} else
		{
			mVelocity.Y += GetGravity().Y * (float)delta;
		}

		if (Input.IsActionJustPressed("Attack") && !_isAttacking)
		{
			_isAttacking = true;
			_attackTimer = 0.5;
			mVelocity.X = 0;
			_animatedSprite.Play("Attack");
		}

		if (_isAttacking)
		{
			_attackTimer -= delta;
			if (_attackTimer <= 0)
			{
				_isAttacking = false;
			}
			MoveAndSlide();
			return;
		}

		if (Input.IsActionPressed("Left"))
		{
			mVelocity.X = -200;
			_animatedSprite.FlipH = true;
			_animatedSprite.Play("Run");
		}
		else if (Input.IsActionPressed("right"))
		{
			mVelocity.X = 200;
			_animatedSprite.FlipH = false;
			_animatedSprite.Play("Run");
		}
		else
		{
			mVelocity.X = 0;
			_animatedSprite.Play("Idle");
		}

		if (Input.IsActionJustPressed("jump") && JumpsLeft > 0)
		{
			mVelocity.Y = -500;
			JumpsLeft--;
		}

		

		Velocity = mVelocity;
		MoveAndSlide();
	}
}