using Godot;

namespace LegendsOfLove.Entities.Enemies.Worm {
	public partial class Worm : BaseEntity.BaseEntity, IHammerable
	{
		protected bool IsAboveGround => Animation.CurrentAnimation == "Above";
		protected bool IsBelowGround => Animation.CurrentAnimation == "Below";
		protected bool IsDigging => Animation.CurrentAnimation == "Dig";
		protected bool IsRising => Animation.CurrentAnimation == "Rise";

		private float _changeDirection;
		private Vector2 _direction;

		private float _modeChange = 10;

		public override void Reset() {
			base.Reset();
			RandomizeDirection();
		}

		protected override Vector2 GetVelocity() {
			if (IsRising || IsDigging) return Vector2.Zero;
			var speed = IsAboveGround ? 16.0f : 4.0f;
			return _direction.Normalized() * speed;
		}

		public override void _Process(float delta) {
			base._Process(delta);

			if (IsFrozen) {
				Animation.Stop();
			}
			else {
				if (!(IsRising || IsDigging)) Animation.Play();
			}

			if (!IsFrozen) {
				_changeDirection -= delta;
				if (_changeDirection <= 0) {
					RandomizeDirection();
				}

				if (IsAboveGround || IsBelowGround) {
					_modeChange -= delta;
					if (_modeChange <= 0) {
						_modeChange = (GD.Randi() % 5) + 5;
						if (IsAboveGround) {
							Animation.Play("Dig");
						}
						else {
							Animation.Play("Rise");
						}
					}
				}
			}
		}

		private void RandomizeDirection() {
			_direction = GetRandomDirection();
			_changeDirection = GD.Randi() % 2 + 3;
		}

		public override void Damage(Vector2 direction) {
			if (IsBelowGround || IsDigging) return;
			base.Damage(direction);
			_modeChange = 0;
		}

		public void Hammer(Vector2 direction) {
			base.Damage(direction);
			_modeChange = 0;
		}
	}
}
