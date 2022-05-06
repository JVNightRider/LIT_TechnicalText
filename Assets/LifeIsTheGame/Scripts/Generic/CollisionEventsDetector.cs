using System;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
	[RequireComponent(typeof(Collider))]
	public class CollisionEventsDetector : MonoBehaviour
	{
		public event Action<Transform, bool> OnTriggerInteraction;

		public event Action<Transform, bool> OnCollisionInteraction;

		[SerializeField]
		private List<string> allowedTags;

		private void OnTriggerEnter(Collider other)
		{
			if (allowedTags.Contains(other.tag))
				OnTriggerInteraction?.Invoke(other.transform, true);
		}

		private void OnTriggerExit(Collider other)
		{
			if (allowedTags.Contains(other.tag))
				OnTriggerInteraction?.Invoke(other.transform, false);
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (allowedTags.Contains(collision.collider.tag))
				OnCollisionInteraction?.Invoke(collision.collider.transform, true);
		}

		private void OnCollisionExit(Collision collision)
		{
			if (allowedTags.Contains(collision.collider.tag))
				OnCollisionInteraction?.Invoke(collision.collider.transform, false);
		}
	}
}