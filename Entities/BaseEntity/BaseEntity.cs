using Godot;

namespace LegendsOfLove.Entities.BaseEntity {
	public partial class BaseEntity : KinematicBody2D, IDamageable {
		[Export] public bool CanBeDamaged { get; set; }
		[Export]  public int MaxHealth { get; set; } = 2;
		public int Health { get; set; } = 2;
		public bool IsInvulnerable { get; set; }
		public bool IsAlive => !CanBeDamaged || Health > 0;

		[Export] public bool IsFrozen { get; set; } = true;
		public virtual void Freeze() => IsFrozen = true;
		public virtual void Unfreeze() => IsFrozen = false;

		protected Vector2 InitialPosition { get; set; }
		protected uint InitialCollisionLayer { get; set; }
		protected uint InitialCollisionMask { get; set; }
		protected Vector2 Velocity { get; set; }
		[Export] public bool DoesNotReset { get; set; }

		public override void _Ready() {
			InitialPosition = Position;
			InitialCollisionLayer = CollisionLayer;
			InitialCollisionMask = CollisionMask;
			Reset();
		}

		public override void _Process(float delta) {
			if (!IsFrozen && IsAlive) {
				MoveAndSlide(Velocity.Length() > 0 ? Velocity : GetVelocity());
			}

			SnapSpriteToGrid();
		}

		protected virtual void SnapSpriteToGrid() {
			Sprite.GlobalPosition = GlobalPosition.DistanceTo(Sprite.GlobalPosition) > 0.7
				? GlobalPosition.Round()
				: Sprite.GlobalPosition.Round();
			DeathSprite.GlobalPosition = GlobalPosition.Round();
		}

		protected virtual Vector2 GetVelocity() {
			return Vector2.Zero;
		}

		public virtual void Reset() {
			Position = InitialPosition;
			Health = MaxHealth;
			Sprite.Visible = true;
			DeathSprite.Visible = false;
			CollisionLayer = InitialCollisionLayer;
			CollisionMask = InitialCollisionMask;
		}

		public virtual void Damage(Vector2 direction) {
			if (!CanBeDamaged) return;
			if (IsInvulnerable) return;
			if (!IsAlive) return;

			IsInvulnerable = true;
			Velocity = direction * 30;
			InvulnerabilityTimer.Start();
			KnockbackAnimation.Play("Knockback");
			Health -= 1;
		}

		public virtual void OnKnockbackEnd() {
			Velocity = Vector2.Zero;
			if (!IsAlive) {
				KnockbackAnimation.Play("Death");
				CollisionLayer = 0;
				CollisionMask = 0;
			}
		}

		protected void _on_InvulnerabilityTimer_timeout() {
			IsInvulnerable = false;
		}
	}
}

