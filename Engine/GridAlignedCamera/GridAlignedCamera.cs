using System.Collections.Generic;
using System.Linq;
using Godot;
using LegendsOfLove.Entities.BaseEntity;
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

        protected List<BaseEntity> GetCurrentEntitiesOnScreen() {
            return ResetArea2D.GetOverlappingBodies().Cast<BaseEntity>().ToList();
        }

        protected void Transition(Vector2 direction, Player player) {
            Tween.RemoveAll();

            var oldEntities = GetCurrentEntitiesOnScreen();
            foreach (var entity in oldEntities) {
                entity.Freeze();
            }

            var delta = direction * new Vector2(72, 48);
            Tween.InterpolateProperty(this, nameof(Position),
                Position, Position + delta, 1);

            var playerDelta = direction * new Vector2(6, 6);
            Tween.InterpolateProperty(player, nameof(player.Position),
                player.Position, player.Position + playerDelta, 1);
            Tween.InterpolateCallback(player, 1, nameof(player.Unfreeze));

            foreach (var entity in oldEntities) {
                Tween.InterpolateCallback(entity, 1, nameof(entity.Reset));
            }
            Tween.InterpolateCallback(this, 1, nameof(UnfreezeNew));
            

            player.Freeze();
            Tween.Start();
        }

        public void UnfreezeNew() {
            var newEntities = GetCurrentEntitiesOnScreen();
            foreach (var entity in newEntities) {
                entity.Unfreeze();
            }
        }

        public void FadeIn() => AnimationPlayer.Play("FadeIn");
        public void FadeOut() => AnimationPlayer.Play("FadeOut");

        public void _on_ContentsArea2D_body_exited(Node body) {
            if (body is Player player) {
                var playerPosition = player.GlobalPosition;
                var playerCell = new Vector2(playerPosition.x / 72, playerPosition.y / 48).Floor();
                GlobalPosition = playerCell * new Vector2(72, 48);
            }
        }
    }
}
