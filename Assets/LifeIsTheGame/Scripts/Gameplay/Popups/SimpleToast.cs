using UnityEngine;
using UnityEngine.UI;

namespace LifeIsTheGame.TechnicalTest.Gameplay.Popups
{
	public class SimpleToast : MonoBehaviour
	{
		private static SimpleToast inst;

		[SerializeField]
		private Text messageTxt;

		[SerializeField]
		private RectTransform rect;

		public void Setup()
		{
			inst = this;

			rect.anchoredPosition += Vector2.up * 200;
		}

		public static void Show(string message, float showTime)
		{
			if (!inst)
			{
				Debug.LogWarning("Simple toast instance has not been assigned");
				return;
			}
			inst.OnShow(message, showTime);
		}

		private void OnShow(string message, float showTime)
		{
			if (IsInvoking(nameof(Hide)))
				CancelInvoke(nameof(Hide));

			LeanTween.cancel(gameObject);

			messageTxt.text = message;

			LeanTween.moveY(rect, -5, 0.25f);

			Invoke(nameof(Hide), showTime);
		}

		private void Hide()
		{
			LeanTween.moveY(rect, rect.anchoredPosition.y + 200, 0.25f);
		}
	}
}