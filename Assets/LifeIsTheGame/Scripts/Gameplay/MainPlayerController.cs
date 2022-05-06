using System;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class MainPlayerController : MonoBehaviour
	{
		public event Action<Transform> OnCoinObtained;

		[Header("Attributes")]
		[SerializeField]
		private Transform weaponPoint;

		private Weapon playerWeapon;

		private const string COIN_TAG = "Coin";

		public void SetWeapon(Weapon weapon)
		{
			playerWeapon?.Discard();
			playerWeapon = weapon;

			weapon.transform.SetPositionAndRotation(weaponPoint.position, weaponPoint.rotation);
			weapon.transform.SetParent(weaponPoint);

			weapon.InjectPlayer(this);
			weapon.Configure();
		}

		private void Update()
		{
			TryShoot();
		}

		private void TryShoot()
		{
			if (Input.GetMouseButton(0))
			{
				if (playerWeapon != null && playerWeapon.CanShoot)
					playerWeapon.Shoot();
			}
			else if (Input.GetMouseButtonUp(0))
				playerWeapon?.Release();
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(COIN_TAG))
			{
				OnCoinObtained?.Invoke(other.transform);
				Destroy(other.gameObject);
			}
		}
	}
}