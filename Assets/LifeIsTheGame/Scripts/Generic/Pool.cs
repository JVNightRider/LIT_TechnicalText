using System.Collections.Generic;
using UnityEngine;

namespace LifeIsTheGame.TechnicalTest
{
	public class Pool : MonoBehaviour
	{
		private List<Transform> poolObjects;

		private Transform parent;

		public void Prepare(int initialValue, Transform prototype, Transform parent)
		{
			poolObjects = new List<Transform>(initialValue);
			this.parent = parent;

			for (int i = 0; i < initialValue; i++)
			{
				Transform current = Instantiate(prototype, parent);
				poolObjects.Add(current);
			}
		}

		public void ReturnToPool(Transform item)
		{
			item.gameObject.SetActive(false);
			item.SetParent(parent);
			item.transform.localPosition = Vector3.zero;

			poolObjects.Add(item);
		}

		public bool TryGetFromPool(out Transform item)
		{
			item = null;
			if (poolObjects.Count == 0)
				return false;

			item = poolObjects[poolObjects.Count - 1];
			poolObjects.Remove(item);

			return true;
		}
	}
}