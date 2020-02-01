using Godot;
using LegendsOfLove.Entities.Player;

namespace LegendsOfLove.Engine.GridAlignedCamera {
    public partial class GridAlignedCamera : Node2D
    {
        public override void _PhysicsProcess(float delta) {
            Camera2D.GlobalPosition = GlobalPosition.Round();
        }

        public void _on_Player_body_entered(Node body, Vector2 direction) {
            if (!(body is Player player)) return;
            if (Tween.IsActive()) return;

            Transition(direction);
            player.Position += direction * 12;
        }

        protected void Transition(Vector2 direction) {
            Tween.RemoveAll();

            var delta = direction * new Vector2(72, 48);
            Tween.InterpolateProperty(this, nameof(Position),
                Position, Position + delta, 1);
            Tween.Start();
        }
    }
}
