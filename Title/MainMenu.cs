using Godot;

namespace LegendsOfLove.Title {
	public partial class MainMenu : Control {
		private bool _triggered = false;

		public void StartGame() {
			GetTree().ChangeScene("res://Main.tscn");
		}

		public override void _Input(InputEvent @event) {
			if (_triggered) return;
			if (!@event.IsPressed()) return;

			_triggered = true;
			AnimationPlayer.Play("GameStart");
		}
	}
}
