using Godot;

namespace LegendsOfLove.Entities.BaseEntity {
	public partial class BaseEntity {
		protected Sprite Sprite => GetNode<Sprite>("Sprite");
	}
}