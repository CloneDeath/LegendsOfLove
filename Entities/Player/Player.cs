using Godot;

namespace LegendsOfLove.Entities.Player {
    public partial class Player : BaseEntity.BaseEntity {
        [Export] public float Speed = 16.0f;
        
        protected PlayerInput PlayerInput => new PlayerInput();
        
        public override void _Process(float delta) {
            MoveAndSlide(PlayerInput.MoveVector * Speed);
            if (PlayerInput.MoveVector.x < 0) {
                Sprite.Scale = new Vector2(-1, 1);
            }
            else if (PlayerInput.MoveVector.x > 0) {
                Sprite.Scale = new Vector2(1, 1);
            }
            if (PlayerInput.MoveVector.Length() <= 0) {
                PlayAnimation("Idle");
            }
            else {
                PlayAnimation("Walk");
            }
            base._Process(delta);
        }

        protected void PlayAnimation(string animationName) {
            if (AnimationPlayer.CurrentAnimation == animationName) return;
            AnimationPlayer.Play(animationName);
        }
    }
}
