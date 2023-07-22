using System;
using Core.Health;
using TowerDefense.Targetting;
using UnityEngine;

namespace TowerDefense.Affectors
{
	/// <summary>
	/// Affector that applies damage to all targets within a radius
	/// Don't extend PassiveAffector, because we don't want to show the radius visualization,
	/// since it will stack on top of the slow affector visualization
	/// </summary>
	public class ProximityDamageAffector : Affector
	{
		[SerializeField] private float m_DamageRate = 0.1f;
		[SerializeField] private SimpleAlignment m_Alignment;
		[SerializeField] private Targetter m_TowerTargetter;
		
		private void Update()
		{
			var targets = m_TowerTargetter.GetAllTargets();
			
			// Since targets can die and be removed from the list, we need to iterate backwards
			for (var i = targets.Count - 1; i >= 0; i--)
			{
				var target = targets[i];
				var damage = m_DamageRate * Time.deltaTime;
				target.TakeDamage(damage, target.position, m_Alignment);
			}
		}
	}
}