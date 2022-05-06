using System;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.MainScene
{
	public class MainSceneAnimationHandler : MonoBehaviour
	{
		[Header("Components")]
		[SerializeField]
		private Transform camTransform;

		[SerializeField]
		private RectTransform btnAnim1, btnAnim2, btnAnim3, btnPlay, anim1Text, anim2Text, anim3Text, blackScreen;

		[Header("Settings")]
		[SerializeField]
		[Range(5, 15)]
		private float cameraPosition = 13;

		[SerializeField]
		[Range(0.1f, 3f)]
		private float cameraMovementTime = 2;

		[SerializeField]
		[Range(0.1f, 3f)]
		private float buttonAnimationTime = 1;

		[SerializeField]
		[Range(0.1f, 3f)]
		private float scaleTextTime = 0.25f;

		[SerializeField]
		[Range(0.1f, 3f)]
		private float blackAlphaTime = 0.25f;

		private Vector3 initialCameraPos;

		private Action onHide;

		public void Setup()
		{
			initialCameraPos = Vector3.up * cameraPosition - Vector3.forward * 10;
			camTransform.position = initialCameraPos;

			anim1Text.localScale = Vector3.zero;
			anim2Text.localScale = Vector3.zero;
			anim3Text.localScale = Vector3.zero;

			LeanTween.alpha(btnAnim1, 0, 0);
			LeanTween.alpha(btnAnim2, 0, 0);
			LeanTween.alpha(btnAnim3, 0, 0);
			LeanTween.alpha(btnPlay, 0, 0);
		}

		public void Show()
		{
			LeanTween.moveY(camTransform.gameObject, 1, cameraMovementTime).setEaseLinear();

			LeanTween.alpha(btnAnim1, 1, buttonAnimationTime).setDelay(cameraMovementTime).setOnComplete(() => ScaleText(true, anim1Text));
			LeanTween.alpha(btnAnim2, 1, buttonAnimationTime).setDelay(cameraMovementTime + 0.25f).setOnComplete(() => ScaleText(true, anim2Text));
			LeanTween.alpha(btnAnim3, 1, buttonAnimationTime).setDelay(cameraMovementTime + 0.5f).setOnComplete(() => ScaleText(true, anim3Text));

			LeanTween.alpha(btnPlay, 1, buttonAnimationTime).setDelay(cameraMovementTime + 0.75f);
		}

		private void ScaleText(bool isShow, RectTransform text)
		{
			LeanTween.scale(text, isShow ? Vector3.one : Vector3.zero, scaleTextTime);
		}

		public void Hide(Action onHide)
		{
			this.onHide = onHide;

			LeanTween.moveY(btnAnim1, btnAnim1.anchoredPosition.y + 100, buttonAnimationTime);
			LeanTween.moveY(btnAnim2, btnAnim1.anchoredPosition.y + 100, buttonAnimationTime);
			LeanTween.moveY(btnAnim3, btnAnim1.anchoredPosition.y + 100, buttonAnimationTime);

			LeanTween.moveX(btnPlay, btnPlay.anchoredPosition.x + 200, buttonAnimationTime);

			LeanTween.move(camTransform.gameObject, initialCameraPos, cameraMovementTime).setOnComplete(FadeToBlack);
		}

		private void FadeToBlack()
		{
			blackScreen.gameObject.SetActive(true);
			blackScreen.LeanAlpha(1, blackAlphaTime).setOnComplete(onHide);
		}
	}
}