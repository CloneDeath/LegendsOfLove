using Godot;
using LegendsOfLove.Engine.GridAlignedCamera;

namespace LegendsOfLove.Entities.Player {
    public partial class Player : BaseEntity.BaseEntity {
        protected GridAlignedCamera Camera => GetTree().GetNodesInGroup("camera")[0] as GridAlignedCamera;
        [Export] public bool DisableInput { get; set; }
        
        [Export] public bool HasSword { get; set; }
        [Export] public bool HasHammer { get; set; }
        [Export] public float Speed = 16.0f;
        [Export] public bool UpdateAnimation { get; set; } = true;

        protected Vector2 Facing { get; set; } = Vector2.Right;

        
        protected PlayerInput PlayerInput => new PlayerInput(IsFrozen || DisableInput);

        public override void _Process(float delta) {
            if (!IsFrozen) {
                PlayerAnimation.Play();

                MoveAndSlide(PlayerInput.MoveVector * Speed);

                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if (PlayerInput.MoveVector.Length() == 1) {
                    Facing = PlayerInput.MoveVector;
                }

                PushSensor.CastTo = Facing * 6;

                UpdatePlayerAnimation();
                UpdatePushing();
            }
            else {
                PlayerAnimation.Stop();
            }

            base._Process(delta);
        }
        
        private void UpdatePushing() {
            if (!UpdateAnimation) return;
            if (IsPushing() && PushSensor.IsColliding()) {
                var collider = PushSensor.GetCollider() as IPushable;
                collider?.Push(Facing);
            }
        }

        protected void UpdatePlayerAnimation() {
            if (!UpdateAnimation) return;
            if (PlayerInput.Attack && HasSword) {
                SetAnimation("Attack");
                return;
            }
            if (IsPushing()) {
                SetAnimation("Push");
                return;
            }
            SetAnimation(PlayerInput.MoveVector.Length() > 0 ? "Walk" : "Idle");
        }

        protected bool IsPushing() {
            return PlayerInput.MoveVector.Length() > 0
                   && TestMove(GlobalTransform, Facing);
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

        public new void SetGlobalPosition(Vector2 destination) {
            GlobalPosition = destination;
        }

        public void TeleportTo(Vector2 destination) {
            TeleportTween.ResetAll();

            TeleportTween.InterpolateCallback(this, 0f, "Freeze");
            TeleportTween.InterpolateCallback(Camera, 0, "FadeOut");
            
            TeleportTween.InterpolateCallback(this, 0.5f, "SetGlobalPosition", destination);
            TeleportTween.InterpolateCallback(Camera, 0.5f, "FadeIn");
            
            TeleportTween.InterpolateCallback(this, 1.0f, "Unfreeze");
            TeleportTween.Start();
        }
    }
}
