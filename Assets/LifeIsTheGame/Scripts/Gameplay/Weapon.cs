using LifeIsTheGame.TechnicalTest.Settings;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public abstract class Weapon : MonoBehaviour
	{
		public abstract WeaponType Type { get; }

		public abstract bool CanShoot { get; }

		public abstract void Shoot();

		public abstract void Configure();

		public abstract void InjectSettings(WeaponSettings settings);

		public virtual void Release()
		{
		}

		public virtual void InjectPlayer(MainPlayerController player)
		{
		}

		public void Discard()
		{
			Destroy(gameObject);
		}
	}
}