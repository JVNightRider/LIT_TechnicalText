using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class CharacterAnimationGetter : MonoBehaviour
	{
		[SerializeField]
		private Animator characterAnimator;

		private void Start()
		{
			characterAnimator.SetTrigger(Globals.TRIGGER_PREFIX + Globals.SelectedAnimationState);
		}
	}
}