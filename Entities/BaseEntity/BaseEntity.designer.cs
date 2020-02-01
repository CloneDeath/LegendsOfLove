using Godot;

namespace LegendsOfLove.Entities.BaseEntity {
	public partial class BaseEntity {
		private Sprite Sprite => GetNode<Sprite>("Sprite");
	}
}