using System;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class Greanade : MonoBehaviour
	{
		private Action<Transform> onGreanadeExplosion;

		[SerializeField]
		private CollisionEventsDetector collisionDetector;

		private float power;
		private float radius;

		private void Start()
		{
			collisionDetector.OnTriggerInteraction += CollisionDetector_OnTriggerInteraction;
		}

		private void CollisionDetector_OnTriggerInteraction(Transform obstacle, bool entry)
		{
			if (!entry)
				return;

			if (obstacle.TryGetComponent(out Rigidbody rb))
			{
				rb.isKinematic = false;
				rb.AddExplosionForce(power, transform.position, radius);
			}
		}

		public void ThrowGreanade(Vector3 force, float explosionPower, float explosionRadius, Action<Transform> onGreanadeExplosion)
		{
			this.onGreanadeExplosion = onGreanadeExplosion;
			radius = explosionRadius;
			power = explosionPower;

			GetComponent<Rigidbody>().AddForceAtPosition(force, transform.position, ForceMode.VelocityChange);
			Invoke(nameof(Explosion), 5);
		}

		private void Explosion()
		{
			collisionDetector.gameObject.SetActive(true);
			LeanTween.delayedCall(gameObject, 0.25f, ResetGreanade);
		}

		private void ResetGreanade()
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			collisionDetector.gameObject.SetActive(false);

			onGreanadeExplosion?.Invoke(transform);
		}
	}
}