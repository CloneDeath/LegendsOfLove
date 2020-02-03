using Godot;
using LegendsOfLove.Entities.Items.Heart;

namespace LegendsOfLove.Entities.Enemies.Slime {
	public class GiantSlime : Slime
	{
		public override void OnDeath() {
			var heart = (Heart)ResourceLoader.Load<PackedScene>("res://Entities/Items/Heart/Heart.tscn").Instance();
			GetParent().AddChild(heart);
			heart.Position = Position;
			QueueFree();
		}
	}
}
