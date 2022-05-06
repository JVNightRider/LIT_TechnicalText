using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifeIsTheGame.TechnicalTest.MainScene
{
	public class MainSceneManager : MonoBehaviour
	{
		[SerializeField]
		private MainSceneAnimationHandler animationHandler;

		[SerializeField]
		private CharacterAnimationsHandler characterAnimations;

		[Header("Load Data")]
		[SerializeField]
		private string gameplaySceneName = "Gameplay";

		private void Awake()
		{
			//Setup Handlers
			animationHandler.Setup();

			//Subscribe Events
			characterAnimations.OnRunGame += OnRunGame;
		}

		private void Start()
		{
			animationHandler.Show();
		}

		private void OnRunGame(CharacterAnimationState animationSelected)
		{
			Globals.SetCharacterAnimationState(animationSelected);
			animationHandler.Hide(LoadNextLevel);
		}

		private void LoadNextLevel()
		{
			SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
		}
	}
}