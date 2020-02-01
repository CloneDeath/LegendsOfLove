using Godot;

namespace LegendsOfLove.Entities.Player {
	public partial class Player {
		protected PlayerAnimation PlayerAnimation => GetNode<PlayerAnimation>("PlayerAnimation");
		protected Tween TeleportTween => GetNode<Tween>("TeleportTween");
	}
}