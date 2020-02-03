using Godot;

namespace LegendsOfLove.Entities.Enemies.Bat {
	public partial class Bat : BaseEntity.BaseEntity {
		protected Vector2 _direction;

		public override void Unfreeze() {
			base.Unfreeze();
			_direction = GetRandomDirection();
		}

		public override void _Ready() {
			base._Ready();
			_direction = GetRandomDirection();
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
				//GD.Print(Position);
				AnimationPlayer.Play();
				MoveAndSlide(_direction * 16);
				if (IsOnWall()) {
					_direction = GetRandomDirection();
				}
			}
			base._Process(delta);
		}
	}
}
