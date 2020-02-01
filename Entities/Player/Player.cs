using Godot;

namespace LegendsOfLove.Entities.Player {
    public class Player : BaseEntity.BaseEntity {
        [Export] public float Speed = 16.0f;
        
        protected PlayerInput PlayerInput => new PlayerInput();
        
        public override void _Process(float delta) {
            MoveAndSlide(PlayerInput.MoveVector * Speed);
            base._Process(delta);
        }
    }
}
