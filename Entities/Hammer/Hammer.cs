namespace LegendsOfLove.Entities.Hammer {
    public class Hammer : BaseEntity.BaseEntity, IItemPickup
    {
        public void OnPickup(Player.Player player) {
            player.HasHammer = true;
        }
    }
}
