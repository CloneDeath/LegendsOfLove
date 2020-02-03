using Godot;

namespace LegendsOfLove.Entities.Enemies.Slime {
	public partial class Slime : BaseEntity.BaseEntity {
		private float _changeDirection;
		private Vector2 _direction;

		public override void Reset() {
			base.Reset();
			RandomizeDirection();
		}

		protected override Vector2 GetVelocity() {
			return _direction.Normalized() * 4.0f;
		}

		public override void _Process(float delta) {
			base._Process(delta);

			if (IsFrozen) {
				Idle.Stop();
			}
			else {
				Idle.Play();
			}

			if (!IsFrozen) {
				_changeDirection -= delta;
				if (_changeDirection <= 0) {
					RandomizeDirection();
				}
			}
		}

		private void RandomizeDirection() {
			_direction = GetRandomDirection();
			_changeDirection = GD.Randi() % 2 + 3;
		}
	}
}
