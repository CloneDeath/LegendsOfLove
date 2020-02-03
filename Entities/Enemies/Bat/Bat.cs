namespace LegendsOfLove.Entities.Enemies.Bat {
	public partial class Bat : BaseEntity.BaseEntity
	{
		public override void _Process(float delta) {
			base._Process(delta);

			if (IsFrozen) {
				AnimationPlayer.Stop();
			}
			else {
				AnimationPlayer.Play();
			}
		}
	}
}
