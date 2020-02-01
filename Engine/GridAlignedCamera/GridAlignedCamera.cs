using System.Collections.Generic;
using System.Linq;
using Godot;
using LegendsOfLove.Entities.Player;

namespace LegendsOfLove.Engine.GridAlignedCamera {
    public partial class GridAlignedCamera : Node2D {
        protected Queue<TransitionAction> TransitionQueue = new Queue<TransitionAction>();
        
        public override void _PhysicsProcess(float delta) {
            Camera2D.GlobalPosition = GlobalPosition.Round();

            CheckForTransitions();
        }

        protected void CheckForTransitions() {
            if (!CanTransition) return;
            if (!TransitionQueue.Any()) return;

            var action = TransitionQueue.Dequeue();
            Transition(action.Direction, action.Player);
        }

        public void _on_Player_body_entered(Node body, Vector2 direction) {
            if (!(body is Player player)) return;
            if (!CanTransition) {
                TransitionQueue.Enqueue(new TransitionAction(player, direction));
                return;
            }

            Transition(direction, player);
        }

        protected bool CanTransition => !Tween.IsActive();

        protected void Transition(Vector2 direction, Player player) {
            Tween.RemoveAll();

            var delta = direction * new Vector2(72, 48);
            Tween.InterpolateProperty(this, nameof(Position),
                Position, Position + delta, 1);

            var playerDelta = direction * new Vector2(6, 6);
            Tween.InterpolateProperty(player, nameof(player.Position),
                player.Position, player.Position + playerDelta, 1);

            Tween.Start();
        }
    }
}
