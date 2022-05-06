using System;
using LifeIsTheGame.TechnicalTest.Settings;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	[RequireComponent(typeof(Pool))]
	public class MagnetLazerWeapon : Weapon
	{
		public override WeaponType Type
		{
			get => WeaponType.MagnetLazer;
		}

		public override bool CanShoot
		{
			get => canShoot;
		}

		[SerializeField]
		private Transform shootPoint;

		[SerializeField]
		private LazerRay lazerRayPrototype;

		private Pool bulletPool;

		private float lazerSpeed;
		private float shootDelay;

		private bool canShoot;

		private const int SHOOT_DISTANCE = 100;

		public override void Configure()
		{
			bulletPool = GetComponent<Pool>();
			bulletPool.Prepare(20, lazerRayPrototype.transform, shootPoint);

			canShoot = true;
		}

		public override void InjectSettings(WeaponSettings settings)
		{
			lazerSpeed = settings.lazerData.lazerSpeed;
			shootDelay = settings.lazerData.shootDelay;
		}

		public override void Shoot()
		{
			if (bulletPool.TryGetFromPool(out Transform lazer))
			{
				LazerRay ray = lazer.GetComponent<LazerRay>();

				ShootLazer(lazer, shootPoint.position + shootPoint.forward * SHOOT_DISTANCE, () => ResetShoot(ray));
				ray.StartMagnet();
			}
		}

		private void ShootLazer(Transform lazer, Vector3 pointTo, Action onShoot)
		{
			lazer.SetParent(null);
			lazer.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);
			lazer.gameObject.SetActive(true);

			canShoot = false;
			LeanTween.delayedCall(gameObject, shootDelay, () => canShoot = true);

			LeanTween.move(lazer.gameObject, pointTo, 1).setSpeed(lazerSpeed).setOnComplete(onShoot);
		}

		private void ResetShoot(LazerRay lazer)
		{
			lazer.ResetShoot();
			bulletPool.ReturnToPool(lazer.transform);
		}
	}
}