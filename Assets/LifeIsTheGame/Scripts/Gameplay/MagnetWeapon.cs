using System.Collections;
using System.Collections.Generic;
using LifeIsTheGame.TechnicalTest.Settings;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class MagnetWeapon : Weapon
	{
		[SerializeField]
		private CollisionEventsDetector eventsDetector;

		[SerializeField]
		private Renderer weaponRenderer, magnetFieldRenderer;

		[SerializeField]
		private Color disableMagnetColor, enableMagnetColor;

		private List<Transform> objectsInMagnet;

		private Transform playerTransform;

		public override WeaponType Type
		{
			get => WeaponType.Magnet;
		}

		public override bool CanShoot
		{
			get => true;
		}

		private bool MagnetEnabled
		{
			set
			{
				magnetEnabled = value;

				weaponRenderer.material.color = magnetEnabled ? enableMagnetColor : disableMagnetColor;
				magnetFieldRenderer.enabled = magnetEnabled;
			}
			get { return magnetEnabled; }
		}

		private float magnetSpeed;

		private bool magnetEnabled;

		public override void InjectSettings(WeaponSettings settings)
		{
			eventsDetector.transform.localScale = Vector3.one * settings.magnetData.magnetFieldRadius;
			magnetSpeed = settings.magnetData.attractionSpeed;
		}

		public override void Configure()
		{
			MagnetEnabled = false;
			objectsInMagnet = new List<Transform>();

			eventsDetector.OnTriggerInteraction += MagnetObject;
		}

		public override void InjectPlayer(MainPlayerController player)
		{
			base.InjectPlayer(player);

			playerTransform = player.transform;
			player.OnCoinObtained += CoinObtained;
		}

		private void CoinObtained(Transform coin)
		{
			if (objectsInMagnet.Contains(coin))
				objectsInMagnet.Remove(coin);
		}

		private void MagnetObject(Transform attractedObj, bool entry)
		{
			if (entry)
				objectsInMagnet.Add(attractedObj);
			else
				objectsInMagnet.Remove(attractedObj);
		}

		public override void Shoot()
		{
			if (MagnetEnabled)
				return;

			MagnetEnabled = true;
			StartCoroutine(MagnetUpdate());
		}

		private IEnumerator MagnetUpdate()
		{
			while (MagnetEnabled)
			{
				for (int i = 0; i < objectsInMagnet.Count; i++)
					objectsInMagnet[i].position = Vector3.MoveTowards(objectsInMagnet[i].position, playerTransform.position, Time.deltaTime * magnetSpeed);

				yield return null;
			}
		}

		public override void Release()
		{
			base.Release();
			MagnetEnabled = false;
		}
	}
}