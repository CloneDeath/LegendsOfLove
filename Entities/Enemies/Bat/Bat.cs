using Godot;
using LegendsOfLove.Entities.Items.Heart;

namespace LegendsOfLove.Entities.Enemies.Bat {
	public partial class Bat : BaseEntity.BaseEntity, IHammerable {
		[Export] public float Speed = 16f;
		protected Vector2 Direction;

		public override void Unfreeze() {
			base.Unfreeze();
			Direction = GetRandomDirection();
		}

		public override void _Ready() {
			base._Ready();
			Direction = GetRandomDirection();
		}

		protected Vector2 GetRandomDirection() {
			switch (GD.Randi() % 8) {
				case 0: return Vector2.Up + Vector2.Right;
				case 1: return Vector2.Down + Vector2.Right;
				case 2: return Vector2.Down + Vector2.Left;
				case 3: return Vector2.Up + Vector2.Left;
				case 4: return Vector2.Up;
				case 5: return Vector2.Right;
				case 6: return Vector2.Down;
				default: return Vector2.Left;
			}
		}

		public override void _Process(float delta) {
			if (IsFrozen) {
				AnimationPlayer.Stop();
			}
			else {
				AnimationPlayer.Play();
			}
			base._Process(delta);
			if (IsOnWall()) {
				Direction = GetRandomDirection();
			}
		}

		public override void Damage(Vector2 direction) {
			base.Damage(direction);

			const float variance = 0.25f;
			const float varianceRadians = 2 * Mathf.Pi * variance;
			var delta = (((GD.Randi() % 100)/100.0f) * varianceRadians) - (varianceRadians / 2.0f);
			Direction = direction.Rotated(delta);

			if (!IsAlive && (GD.Randi() % 4) == 0) {
				var heart = (Heart)ResourceLoader.Load<PackedScene>("res://Entities/Items/Heart/Heart.tscn").Instance();
				GetParent().AddChild(heart);
				heart.Position = Position;
			}
		}

		protected override Vector2 GetVelocity() {
			return Direction.Normalized() * Speed;
		}

		public void Hammer(Vector2 direction) {
			Damage(direction);
		}
	}
}
