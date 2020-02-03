using Godot;
using LegendsOfLove.Engine.GridAlignedCamera;

namespace LegendsOfLove.Entities.Player {
	public partial class Player : BaseEntity.BaseEntity {
		protected GridAlignedCamera Camera => GetTree().GetNodesInGroup("camera")[0] as GridAlignedCamera;
		[Export] public bool DisableInput { get; set; }

		[Export] public bool HasSword { get; set; }
		[Export] public bool HasHammer { get; set; }
		[Export] public float Speed = 16.0f;
		[Export] public bool UpdateAnimation { get; set; } = true;

		protected Vector2 Facing { get; set; } = Vector2.Right;

		protected PlayerInput PlayerInput => new PlayerInput(IsFrozen || DisableInput);

		protected void MakeGravestoneVisible() {
			Gravestone.Visible = true;
		}

		public override void _Process(float delta) {
			if (IsFrozen) {
				PlayerAnimation.Stop();
			}
			else {
				PlayerAnimation.Play();
			}

			base._Process(delta);

			if (!IsFrozen && IsAlive) {
				// ReSharper disable once CompareOfFloatsByEqualityOperator
				if (PlayerInput.MoveVector.Length() == 1) {
					Facing = PlayerInput.MoveVector;
				}

				PushSensor.CastTo = Facing * 6;
				PushSensor.ForceRaycastUpdate();

				UpdatePlayerAnimation();
				UpdatePushing();

				if (DamageArea.Monitoring) {
					foreach (var body in DamageArea.GetOverlappingBodies()) {
						if (!(body is IDamageable damageable)) continue;
						damageable.Damage(Facing);
					}
				}
			}
		}

		protected override void SnapSpriteToGrid() {
			base.SnapSpriteToGrid();
			Gravestone.GlobalPosition = GlobalPosition.Round();
		}

		protected override Vector2 GetVelocity() {
			return PlayerInput.MoveVector.Normalized() * Speed;
		}

		private void UpdatePushing() {
			if (!UpdateAnimation) return;
			if (IsPushing() && PushSensor.IsColliding()) {
				var collider = PushSensor.GetCollider() as IPushable;
				collider?.Push(Facing);
			}
		}

		protected void UpdatePlayerAnimation() {
			if (!UpdateAnimation) return;
			if (PlayerInput.Attack && HasSword) {
				SetAnimation("Attack");
				return;
			}
			if (PlayerInput.Hammer && HasHammer) {
				SetAnimation("Hammer");
				return;
			}
			if (IsPushing()) {
				SetAnimation("Push");
				return;
			}
			SetAnimation(PlayerInput.MoveVector.Length() > 0 ? "Walk" : "Idle");
		}

		protected bool IsPushing() {
			return PlayerInput.MoveVector.Length() > 0
				   && TestMove(GlobalTransform, Facing);
		}

		protected void SetAnimation(string animationName) {
			PlayerAnimation.SetAnimation(animationName, Facing);
		}

		public void Play(string animation) {
			PlayerAnimation.Play(animation);
		}

		public void _on_ItemDetector_body_entered(Node other) {
			if (!(other is IItemPickup itemPickup)) return;

			itemPickup.OnPickup(this);
		}

		public void _on_EnemyDetector_body_entered(Node other) {
			if (!(other is BaseEntity.BaseEntity enemy)) return;
			var direction = enemy.GlobalPosition.DirectionTo(GlobalPosition);
			Damage(direction);
		}

		public override void OnKnockbackEnd() {
			base.OnKnockbackEnd();
			if (!IsAlive) {
				ResetPlayerTween.RemoveAll();
				UpdateAnimation = false;
				ResetPlayerTween.InterpolateCallback(this, 0.5f, nameof(MakeGravestoneVisible));
				ResetPlayerTween.InterpolateCallback(this, 1f, nameof(TeleportTo), InitialPosition);
				ResetPlayerTween.InterpolateCallback(this, 1.5f, nameof(Reset));
				ResetPlayerTween.Start();
			}
		}

		public new void SetGlobalPosition(Vector2 destination) {
			GlobalPosition = destination;
		}

		public void TeleportTo(Vector2 destination) {
			TeleportTween.ResetAll();

			TeleportTween.InterpolateCallback(this, 0f, "Freeze");
			TeleportTween.InterpolateCallback(Camera, 0, "FadeOut");

			TeleportTween.InterpolateCallback(this, 0.5f, "SetGlobalPosition", destination);
			TeleportTween.InterpolateCallback(Camera, 0.5f, "FadeIn");

			TeleportTween.InterpolateCallback(this, 1.0f, "Unfreeze");
			TeleportTween.Start();
		}

		public void DoHammerDamage() {
			var nodes = HammerArea.GetOverlappingBodies();
			foreach (var node in nodes) {
				if (!(node is IHammerable hammerable)) continue;
				hammerable.Hammer(Facing);
			}
		}

		public override void Reset() {
			base.Reset();
			Gravestone.Visible = false;
			UpdateAnimation = true;
		}
	}
}
