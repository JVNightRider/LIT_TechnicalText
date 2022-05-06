using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LifeIsTheGame.TechnicalTest.MainScene
{
	public class CharacterAnimationsHandler : MonoBehaviour
	{
		public event Action<CharacterAnimationState> OnRunGame;

		[SerializeField]
		private List<CharacterAnimationTrigger> animationTriggers;

		[SerializeField]
		private Animator characterAnimator;

		[SerializeField]
		private Button runButton;

		private CharacterAnimationState currentState;

		private void Start()
		{
			foreach (CharacterAnimationTrigger ct in animationTriggers)
				ct.button.onClick.AddListener(() => ChangeCharacterAnimation(ct.state));

			runButton.onClick.AddListener(OnRunClicked);
		}

		private void ChangeCharacterAnimation(CharacterAnimationState state)
		{
			currentState = state;
			characterAnimator.SetTrigger(Globals.TRIGGER_PREFIX + currentState);

			if (!runButton.interactable)
				runButton.interactable = true;
		}

		private void OnRunClicked()
		{
			OnRunGame?.Invoke(currentState);
		}
	}
}