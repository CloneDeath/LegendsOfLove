using Godot;

namespace LegendsOfLove.Entities.Player {
	public partial class Player {
		protected PlayerAnimation PlayerAnimation => GetNode<PlayerAnimation>("PlayerAnimation");
	}
}