using UnityEngine;
using UnityEngine.SceneManagement;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class GameplayManager : MonoBehaviour
	{
		[SerializeField]
		private UIController uiController;

		private void Awake()
		{
			uiController.OnResetLevel += ResetLevel;
		}

		private void ResetLevel()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}