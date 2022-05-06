using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest.Gameplay
{
	public class LazerRay : MonoBehaviour
	{
		[SerializeField]
		private CollisionEventsDetector collisionEvents;

		private List<Transform> orbitObjects;

		private bool magnetProcess;

		private void Start()
		{
			collisionEvents.OnTriggerInteraction += OnTriggerInteraction;
		}

		private void OnTriggerInteraction(Transform interactor, bool entry)
		{
			if (entry && !orbitObjects.Contains(interactor))
			{
				if (interactor.TryGetComponent(out Rigidbody rb))
					rb.isKinematic = true;

				interactor.parent = transform;
				orbitObjects.Add(interactor);
			}
		}

		public void StartMagnet()
		{
			magnetProcess = true;
			orbitObjects = new List<Transform>();

			StartCoroutine(OrbitObjectsAroundMe());
		}

		private IEnumerator OrbitObjectsAroundMe()
		{
			while (magnetProcess)
			{
				for (int i = 0; i < orbitObjects.Count; i++)
					orbitObjects[i].RotateAround(transform.position, Vector3.right, 1);

				yield return null;
			}
		}

		public void ResetShoot()
		{
			magnetProcess = false;
			foreach (Transform t in orbitObjects)
			{
				if (t.TryGetComponent(out Rigidbody rb))
					rb.isKinematic = false;

				t.parent = null;
				Destroy(t.gameObject, 2f);
			}
			orbitObjects.Clear();
		}
	}
}