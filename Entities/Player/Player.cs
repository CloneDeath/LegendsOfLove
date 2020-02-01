using Godot;

namespace LegendsOfLove.Entities.Player {
    public partial class Player : BaseEntity.BaseEntity {
        [Export] public float Speed = 16.0f;
        [Export] public bool Frozen { get; set; } = false;

        public void Freeze() => Frozen = true;
        public void Unfreeze() => Frozen = false;
        
        protected PlayerInput PlayerInput => new PlayerInput();
        
        public override void _Process(float delta) {
            var moveVector = Frozen ? Vector2.Zero : PlayerInput.MoveVector;
            MoveAndSlide(moveVector * Speed);
            if (moveVector.x < 0) {
                Sprite.Scale = new Vector2(-1, 1);
            }
            else if (moveVector.x > 0) {
                Sprite.Scale = new Vector2(1, 1);
            }
            if (moveVector.Length() <= 0) {
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
