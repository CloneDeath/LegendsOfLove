using Godot;

namespace LegendsOfLove.Entities.Door {
	public class Door : BaseEntity.BaseEntity {
		[Export] public bool ActivationOpens { get; set; } = false;
		[Export] public int ActivationThreshold { get; set; } = 1;

		protected int ActivationLevel;

		public override void _Process(float delta) {
			base._Process(delta);

			if (ActivationOpens) {
				Sprite.Visible = ActivationLevel < ActivationThreshold;
				SetCollisionLayerBit(2, ActivationLevel < ActivationThreshold);
				SetCollisionLayerBit(6, ActivationLevel < ActivationThreshold);
			}
			else {
				Sprite.Visible = ActivationLevel >= ActivationThreshold;
				SetCollisionLayerBit(2, ActivationLevel >= ActivationThreshold);
				SetCollisionLayerBit(6, ActivationLevel >= ActivationThreshold);
			}
		}

		public void Activate() {
			ActivationLevel += 1;
		}

		public void Deactivate() {
			ActivationLevel -= 1;
		}

		public override void Reset() {
			base.Reset();
			if (!DoesNotReset) {
				ActivationLevel = 0;
			}
		}

		public void _on_Switch_body_entered() {
		}
	}
}
