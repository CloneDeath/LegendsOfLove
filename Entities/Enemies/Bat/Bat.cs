using Godot;

namespace LegendsOfLove.Entities.Enemies.Bat {
	public partial class Bat : BaseEntity.BaseEntity, IHammerable {
		protected Vector2 Direction;

		public override void Unfreeze() {
			base.Unfreeze();
			Direction = GetRandomDirection();
		}

		public override void _Ready() {
			base._Ready();
			Direction = GetRandomDirection();
		}

		private Vector2 GetRandomDirection() {
			switch (GD.Randi() % 4) {
				case 0: return Vector2.Up + Vector2.Right;
				case 1: return Vector2.Down + Vector2.Right;
				case 2: return Vector2.Down + Vector2.Left;
				default: return Vector2.Up + Vector2.Left;
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
			GD.Print("Hello 001");

			const float variance = 0.25f;
			const float varianceRadians = 2 * Mathf.Pi * variance;
			GD.Print("Hello 002");

			var delta = (((GD.Randi() % 100)/100.0f) * varianceRadians) - (varianceRadians / 2.0f);
			GD.Print("Hello 003");

			Direction = direction.Rotated(delta);
			GD.Print("Hello 004");
		}

		protected override Vector2 GetVelocity() {
			return Direction * 24;
		}

		public void Hammer(Vector2 direction) {
			Damage(direction);
		}
	}
}
