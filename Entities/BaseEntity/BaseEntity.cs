using Godot;

namespace LegendsOfLove.Entities.BaseEntity {
    public partial class BaseEntity : KinematicBody2D {
        public override void _Process(float delta) {
            if ((Sprite.GlobalPosition - GlobalPosition).Length() > 0.7) {
                Sprite.GlobalPosition = GlobalPosition.Round();
            }
            else {
                Sprite.GlobalPosition = Sprite.GlobalPosition.Round();
            }
        }
    }
}
