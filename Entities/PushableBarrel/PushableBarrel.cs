using Godot;

namespace LegendsOfLove.Entities.PushableBarrel {
    public partial class PushableBarrel : BaseEntity.BaseEntity, IPushable {
        protected bool BeingPushed;
        public void Push(Vector2 direction) {
            if (MovementTween.IsActive()) return;
            
            AnimationPlayer.Play("Roll");
            StartMovementTween(direction);
        }

        protected void StartMovementTween(Vector2 direction) {
            MovementTween.RemoveAll();
            
            const float duration = 0.5f;
            MovementTween.InterpolateProperty(this, nameof(Position),
                Position, Position + direction * 6,  duration);
            MovementTween.Start();
        }
    }
}
