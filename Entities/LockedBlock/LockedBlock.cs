using System.Linq;
using Godot;

namespace LegendsOfLove.Entities.LockedBlock {
	public class LockedBlock : BaseEntity.BaseEntity, IPushable
	{
		public Player.Player GetPlayer() => GetTree().GetNodesInGroup("player").Cast<Player.Player>().FirstOrDefault();

		public void Push(Vector2 direction) {
			var player = GetPlayer();
			if (player == null || !player.HasKey1) return;

			QueueFree();
		}
	}
}
