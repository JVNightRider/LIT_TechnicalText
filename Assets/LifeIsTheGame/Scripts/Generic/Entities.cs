using System;
using UnityEngine;
using UnityEngine.UI;

namespace LifeIsTheGame.TechnicalTest
{
	public enum CharacterAnimationState
	{
		HouseDancing = 0,
		MacarenaDance = 1,
		WaveHipHopDance = 2
	}

	public enum WeaponType
	{
		MagnetLazer = 0,
		Rocket = 1,
		Magnet = 2
	}

	[Serializable]
	public class CharacterAnimationTrigger
	{
		public Button button;
		public CharacterAnimationState state;
	}

	[Serializable]
	public class RocketWeaponData
	{
		[Tooltip("Impulse which grenade will be thrown with")]
		[Range(1, 20)]
		public float greanadeForceAmount = 5;

		[Tooltip("Time that weapol will wait until throw next grenade")]
		[Range(0.1f, 5)]
		public float shootDelay = 1;

		[Tooltip("Explosion force amount")]
		[Range(1000, 10000)]
		public float explosionPower = 8000;

		[Tooltip("Radius of grenade explosion")]
		[Range(1, 30)]
		public float explosionRadius = 8;
	}

	[Serializable]
	public class MagnetWeaponData
	{
		[Tooltip("Size of magnet field")]
		[Range(1, 15)]
		public float magnetFieldRadius = 8;

		[Tooltip("Speed which coins will be attracted")]
		[Range(0.1f, 50)]
		public float attractionSpeed = 10;
	}

	[Serializable]
	public class LazerWeaponData
	{
		[Tooltip("Speed which lazer bullets will be thrown at")]
		[Range(1, 300)]
		public float lazerSpeed = 20;

		[Tooltip("Delay between one shoot and other")]
		[Range(0.1f, 10)]
		public float shootDelay = 3;
	}
}