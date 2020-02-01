using Godot;

namespace LegendsOfLove.Entities.Player {
	public class PlayerInput {
		public bool MoveLeft => Input.IsActionPressed("move_left");
		public bool MoveRight => Input.IsActionPressed("move_right");
		public bool MoveUp => Input.IsActionPressed("move_up");
		public bool MoveDown => Input.IsActionPressed("move_down");

		public Vector2 MoveVector {
			get {
				var left = MoveLeft ? -1 : 0;
				var right = MoveRight ? 1 : 0;
				var up = MoveUp ? -1 : 0;
				var down = MoveDown ? 1 : 0;

				return new Vector2(left + right, up + down);
			}
		}
	}
}