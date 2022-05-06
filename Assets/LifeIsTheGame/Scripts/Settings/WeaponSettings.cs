using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Settings
{
	[CreateAssetMenu(fileName = "Weapon Settings", menuName = "Create Settings", order = 0)]
	public class WeaponSettings : ScriptableObject
	{
		public LazerWeaponData lazerData;
		public RocketWeaponData rocketData;
		public MagnetWeaponData magnetData;
	}
}