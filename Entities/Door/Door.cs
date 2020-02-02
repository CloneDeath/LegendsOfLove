using Godot;

namespace LegendsOfLove.Entities.Door {
    public class Door : BaseEntity.BaseEntity {
        [Export] public bool InitiallyActive { get; set; } = true;

        public override void _Ready() {
            base._Ready();
            
            if (InitiallyActive) Activate();
            else Deactivate();
        }
        
        public void Activate() {
            Sprite.Visible = true;
            SetCollisionLayerBit(2, true);
            SetCollisionLayerBit(6, true);
        }

        public void Deactivate() {
            Sprite.Visible = false;
            SetCollisionLayerBit(2, false);
            SetCollisionLayerBit(6, false);
        }

        public override void Reset() {
            if (InitiallyActive) Activate();
            else Deactivate();
        }

        public void _on_Switch_body_entered() {
            GD.Print("XX");
        }
    }
}
