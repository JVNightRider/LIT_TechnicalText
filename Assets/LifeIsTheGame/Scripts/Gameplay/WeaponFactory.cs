using System;
using System.Collections.Generic;
using LifeIsTheGame.TechnicalTest.Gameplay.Popups;
using LifeIsTheGame.TechnicalTest.Settings;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	[Serializable]
	public class WeaponTrigger
	{
		public WeaponType type;
		public CollisionEventsDetector eventsDetector;

		[TextArea]
		public string description;
	}

	public class WeaponFactory : MonoBehaviour
	{
		[SerializeField]
		private WeaponSettings weaponSettings;

		[SerializeField]
		private List<WeaponTrigger> weaponTriggers;

		[SerializeField]
		private List<Weapon> weapons;

		private Weapon selectedWeapon, playerWeapon;

		private void Start()
		{
			weaponTriggers.ForEach(wt => wt.eventsDetector.OnTriggerInteraction += (player, isEntry) => OnWeaponObtained(player, wt));
		}

		private void OnWeaponObtained(Transform player, WeaponTrigger wt)
		{
			wt.eventsDetector.gameObject.SetActive(false);

			if (player.TryGetComponent(out MainPlayerController playerController))
				playerController.SetWeapon(GetWeapon(wt.type));
			else
				Debug.LogWarningFormat("Object {0} doesn't contain Player Controller", player.name);

			SimpleToast.Show(wt.description, 4f);
		}

		private Weapon GetWeapon(WeaponType type)
		{
			selectedWeapon = weapons.Find(w => w.Type == type);

			playerWeapon = Instantiate(selectedWeapon);
			playerWeapon.InjectSettings(weaponSettings);

			return playerWeapon;
		}
	}
}