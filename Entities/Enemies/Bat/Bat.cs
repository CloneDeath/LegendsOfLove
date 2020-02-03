using Godot;

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
		}

		protected override Vector2 GetVelocity() {
			return Direction.Normalized() * Speed;
		}

		public void Hammer(Vector2 direction) {
			Damage(direction);
		}
	}
}
