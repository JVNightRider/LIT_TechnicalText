namespace LifeIsTheGame.TechnicalTest
{
	public sealed class Globals
	{
		public static CharacterAnimationState SelectedAnimationState { get; private set; }

		public const string TRIGGER_PREFIX = "Tgr_";

		public static void SetCharacterAnimationState(CharacterAnimationState state)
		{
			SelectedAnimationState = state;
		}
	}
}