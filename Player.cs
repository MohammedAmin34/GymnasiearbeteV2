using Godot;

public partial class Player : CharacterBody2D
{
	private AnimatedSprite2D _animatedSprite;
	private bool _isAttacking = false;
	private double _attackTimer = 0.0;

	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("Attack") && !_isAttacking)
		{
			_isAttacking = true;
			_attackTimer = 0.5;
			Velocity = new Vector2(0, Velocity.Y);
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
			Velocity = new Vector2(-200, Velocity.Y);
			_animatedSprite.FlipH = true;
			_animatedSprite.Play("Run");
		}
		else if (Input.IsActionPressed("right"))
		{
			Velocity = new Vector2(200, Velocity.Y);
			_animatedSprite.FlipH = false;
			_animatedSprite.Play("Run");
		}
		else
		{
			Velocity = new Vector2(0, Velocity.Y);
			_animatedSprite.Play("Idle");
		}

		MoveAndSlide();
	}
}