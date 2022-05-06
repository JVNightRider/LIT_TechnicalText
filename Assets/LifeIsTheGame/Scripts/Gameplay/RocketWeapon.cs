using LifeIsTheGame.TechnicalTest.Settings;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	[RequireComponent(typeof(Pool))]
	public class RocketWeapon : Weapon
	{
		public override WeaponType Type
		{
			get => WeaponType.Rocket;
		}

		public override bool CanShoot
		{
			get => canShoot;
		}

		[SerializeField]
		private Greanade greanade;

		[SerializeField]
		private Transform shootPoint;

		private Pool bulletPool;

		private float grenadeForceAmount;
		private float shootDelay;
		private float explosionPower;
		private float explosionRadius;

		private bool canShoot;

		public override void InjectSettings(WeaponSettings settings)
		{
			grenadeForceAmount = settings.rocketData.greanadeForceAmount;
			shootDelay = settings.rocketData.shootDelay;
			explosionPower = settings.rocketData.explosionPower;
			explosionRadius = settings.rocketData.explosionRadius;
		}

		public override void Configure()
		{
			bulletPool = GetComponent<Pool>();
			bulletPool.Prepare(5, greanade.transform, transform);

			canShoot = true;
		}

		public override void Shoot()
		{
			if (bulletPool.TryGetFromPool(out Transform bullet))
			{
				bullet.SetParent(null);
				bullet.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
				bullet.gameObject.SetActive(true);

				Greanade g = bullet.GetComponent<Greanade>();
				Vector3 force = (shootPoint.forward + shootPoint.up) * grenadeForceAmount;

				g.ThrowGreanade(force, explosionPower, explosionRadius, OnGreanadeExplosion);
				canShoot = false;

				LeanTween.delayedCall(gameObject, shootDelay, () => canShoot = true);
			}
		}

		private void OnGreanadeExplosion(Transform greanade)
		{
			bulletPool.ReturnToPool(greanade);
		}
	}
}