using Godot;

namespace LegendsOfLove.Entities.BaseEntity {
    public partial class BaseEntity : KinematicBody2D {
        public bool IsFrozen { get; set; }
        public void Freeze() => IsFrozen = true;
        public void Unfreeze() => IsFrozen = false;
        
        protected Vector2 InitialPosition { get; set; }

        public override void _Ready() {
            InitialPosition = Position;
        }

        public override void _Process(float delta) {
            if ((Sprite.GlobalPosition - GlobalPosition).Length() > 0.7) {
                Sprite.GlobalPosition = GlobalPosition.Round();
            }
            else {
                Sprite.GlobalPosition = Sprite.GlobalPosition.Round();
            }
        }

        public virtual void Reset() {
            Position = InitialPosition;
        }
    }
}
