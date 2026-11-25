using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private bool _isAttacking = false;
	private Area2D _swordArea;	
	private double _attackTimer = 0.0;
	private int JumpsLeft = 0; 

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");	
		_swordArea = GetNode<Area2D>("Area2D");

		_swordArea.Monitoring = false;
		_swordArea.BodyEntered += OnSwordBodyEntered;
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
			_swordArea.SetDeferred("monitoring", true);
		}

		if (_isAttacking)
		{
			_attackTimer -= delta;
			if (_attackTimer <= 0)
			{
				_isAttacking = false;
				_swordArea.SetDeferred("monitoring", false);
			}
			MoveAndSlide();
			return;
		}

		if (Input.IsActionPressed("Left"))
		{
			mVelocity.X = -200;
			_animatedSprite.FlipH = true;
			_animatedSprite.Play("Run");
			_swordArea.Scale = new Vector2(-1, 1);
		}
		else if (Input.IsActionPressed("right"))
		{
			mVelocity.X = 200;
			_animatedSprite.FlipH = false;
			_animatedSprite.Play("Run");
			_swordArea.Scale = new Vector2(1, 1);
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
	private void OnSwordBodyEntered(Node2D body)
	{ 
		if (body is Monster monster)
			{
				GD.Print("Hit monster");
				monster.TakeDamage(1);
			}
	}

}