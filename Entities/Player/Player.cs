using Godot;

namespace LegendsOfLove.Entities.Player {
    public partial class Player : BaseEntity.BaseEntity {
        [Export] public bool HasSword { get; set; }
        [Export] public float Speed = 16.0f;
        [Export] public bool Frozen { get; set; }
        [Export] public bool UpdateAnimation { get; set; } = true;

        protected Vector2 Facing { get; set; } = Vector2.Right;

        public void Freeze() => Frozen = true;
        public void Unfreeze() => Frozen = false;
        
        protected PlayerInput PlayerInput => new PlayerInput(Frozen);

        public override void _Process(float delta) {
            MoveAndSlide(PlayerInput.MoveVector * Speed);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (PlayerInput.MoveVector.Length() == 1) {
                Facing = PlayerInput.MoveVector;
            }
            UpdatePlayerAnimation();

            base._Process(delta);
        }

        protected void UpdatePlayerAnimation() {
            if (!UpdateAnimation) return;
            if (PlayerInput.Attack && HasSword) {
                SetAnimation("Attack");
                return;
            }
            if (PlayerInput.MoveVector.Length() > 0) {
                if (TestMove(Transform, Facing)) {
                    SetAnimation("Push");
                    return;
                }
            }
            SetAnimation(PlayerInput.MoveVector.Length() > 0 ? "Walk" : "Idle");
        }

        protected void SetAnimation(string animationName) {
            PlayerAnimation.SetAnimation(animationName, Facing);
        }

        public void _on_ItemDetector_body_entered(Node other) {
            if (!(other is IItemPickup itemPickup)) return;
            
            itemPickup.OnPickup(this);
            PlayerAnimation.Play("GetSword");
            other.QueueFree();
        }
    }
}
