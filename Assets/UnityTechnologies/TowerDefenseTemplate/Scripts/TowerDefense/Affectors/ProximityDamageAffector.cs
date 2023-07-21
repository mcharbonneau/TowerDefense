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
		[SerializeField] private float m_damageRate = 0.1f;
		[SerializeField] private SimpleAlignment m_alignment;
		[SerializeField] private Targetter m_towerTargetter;
		
		private void Update()
		{
			Debug.Log(m_towerTargetter.GetAllTargets().Count);
			foreach (var target in m_towerTargetter.GetAllTargets())
			{
				var damage = m_damageRate * Time.deltaTime;
				target.TakeDamage(damage, target.position, m_alignment);
			}
		}
	}
}