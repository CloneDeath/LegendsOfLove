using Godot;

namespace LegendsOfLove.Entities.Player {
	public partial class Player {
		protected AnimationPlayer AnimationPlayer => GetNode<AnimationPlayer>("AnimationPlayer");
	}
}