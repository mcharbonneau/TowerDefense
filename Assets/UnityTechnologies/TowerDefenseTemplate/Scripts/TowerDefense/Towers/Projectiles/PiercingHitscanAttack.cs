using System.Collections.Generic;
using ActionGameFramework.Health;
using Core.Utilities;
using UnityEngine;

namespace TowerDefense.Towers.Projectiles
{
	/// <summary>
	/// Implementation of hitscan projectile
	/// The principle behind this weapon is that it instantly attacks enemies
	/// </summary>
	[RequireComponent(typeof(Damager))]
	public class PiercingHitscanAttack : MonoBehaviour
	{
		/// <summary>
		/// The amount of time to delay
		/// </summary>
		public float delay;

		/// <summary>
		/// The delay timer
		/// </summary>
		protected Timer m_Timer;

		/// <summary>
		/// The enemies this projectile will attack
		/// </summary>
		protected IList<Targetable> m_Enemies;

		/// <summary>
		/// The Damager attached to the object
		/// </summary>
		protected Damager m_Damager;

		/// <summary>
		/// Configuration for pausing the timer delay timer
		/// without setting Time.timeScale to 0
		/// </summary>
		protected bool m_PauseTimer;

		/// <summary>
		/// The delay configuration for the attacking
		/// </summary>
		/// <param name="origin">
		/// The point the attack will be fired from
		/// </param>
		/// <param name="enemies">
		/// The enemies to attack
		/// </param>
		public void AttackEnemies(IList<Targetable> enemies)
		{
			m_Enemies = enemies;
			
			m_Timer.Reset();
			m_PauseTimer = false;
		}

		/// <summary>
		/// The actual attack of the piercing hitscan attack.
		/// Early returns from the method if the there is no enemy to attack.
		/// </summary>
		protected void DealDamage()
		{
			Poolable.TryPool(gameObject);
			
			foreach (var enemy in m_Enemies)
			{
				if (enemy == null)
				{
					continue;
				}
				
				// effects
				ParticleSystem pfxPrefab = m_Damager.collisionParticles;
				var attackEffect = Poolable.TryGetPoolable<ParticleSystem>(pfxPrefab.gameObject);
				attackEffect.transform.position = enemy.position;
				attackEffect.Play();
			
				enemy.TakeDamage(m_Damager.damage, enemy.position, m_Damager.alignmentProvider);
			}
			
			m_PauseTimer = true;
		}

		/// <summary>
		/// Cache the damager component attached to this object
		/// </summary>
		protected virtual void Awake()
		{
			m_Damager = GetComponent<Damager>();
			m_Timer = new Timer(delay, DealDamage);
		}

		/// <summary>
		/// Update the m_Timer if it is available
		/// </summary>
		protected virtual void Update()
		{
			if (!m_PauseTimer)
			{
				m_Timer.Tick(Time.deltaTime);
			}
		}
	}
}