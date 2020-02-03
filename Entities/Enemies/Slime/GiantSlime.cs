using Godot;
using LegendsOfLove.Entities.Items.Keys;

namespace LegendsOfLove.Entities.Enemies.Slime {
	public class GiantSlime : Slime
	{
		public override void OnDeath() {
			base.OnDeath();
			var heart = (Key1)ResourceLoader.Load<PackedScene>("res://Entities/Items/Keys/Key1.tscn").Instance();
			GetParent().AddChild(heart);
			heart.Position = Position;
			QueueFree();
		}
	}
}
