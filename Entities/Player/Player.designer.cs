using Godot;

namespace LegendsOfLove.Entities.Player {
	public partial class Player {
		protected PlayerAnimation PlayerAnimation => GetNode<PlayerAnimation>("PlayerAnimation");
		protected Tween TeleportTween => GetNode<Tween>("TeleportTween");
		protected RayCast2D PushSensor => GetNode<RayCast2D>("PushSensor");
		protected Area2D HammerArea => GetNode<Area2D>("HammerArea");
	}
}