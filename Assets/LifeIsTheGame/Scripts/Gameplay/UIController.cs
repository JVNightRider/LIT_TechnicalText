using System;
using LifeIsTheGame.TechnicalTest.Gameplay.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class UIController : MonoBehaviour
	{
		public event Action OnResetLevel;

		[SerializeField]
		private SimpleToast toast;

		[SerializeField]
		private Button resetBtn;

		[SerializeField]
		private RectTransform blackScreen;

		[TextArea]
		[SerializeField]
		private string initialMessage;

		private void Start()
		{
			toast.Setup();

			resetBtn.onClick.AddListener(ResetLevel);

			LeanTween.delayedCall(gameObject, 1, ShowInitialToast);
		}

		private void ShowInitialToast()
		{
			SimpleToast.Show(initialMessage, 2);
		}

		private void ResetLevel()
		{
			blackScreen.gameObject.SetActive(true);
			blackScreen.LeanAlpha(1, 0.5f).setOnComplete(OnResetLevel);
		}
	}
}