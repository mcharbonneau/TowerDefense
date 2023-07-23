using System.Collections.Generic;
using ActionGameFramework.Health;
using TowerDefense.Targetting;
using TowerDefense.Towers.Projectiles;
using UnityEngine;

namespace TowerDefense.Towers.TowerLaunchers
{
	/// <summary>
	/// An implementation of the tower launcher for piercing hitscan attacks
	/// </summary>
	public class PiercingHitscanLauncher : Launcher
	{
		/// <summary>
		/// The particle system used for providing launch feedback
		/// </summary>
		[SerializeField] private ParticleSystem m_FireParticleSystem;
		
		[SerializeField] private float m_CapsuleHalfHeight = 2f;
		[SerializeField] private float m_CapsuleRadius = 0.1f;
		
		/// <summary>
		/// Custom max distance for the attack, since it can go further than the tower's targeting range
		/// </summary>
		[SerializeField] private float m_Distance = 20;
		
		[SerializeField] private LayerMask m_LayerMask;
		
		// Reuse the same arrays to avoid allocations
		private RaycastHit[] m_Hits = new RaycastHit[256];
		private List<Targetable> m_Targetables = new List<Targetable>();
		
		/// <summary>
		/// Assigns the correct damage to the hitscan object and every enemy in the line of fire.
		/// </summary>
		/// <param name="enemy">
		/// The enemy this tower is targeting
		/// </param>
		/// <param name="attack">
		/// The attacking component used to damage the enemy
		/// </param>
		/// <param name="firingPoint"></param>
		public override void Launch(Targetable enemy, GameObject attack, Transform firingPoint)
		{
			var hitscanAttack = attack.GetComponent<PiercingHitscanAttack>();
			if (hitscanAttack == null)
			{
				return;
			}
			
			var origin = firingPoint.position;
			var targetPosition = enemy.position;
			
			// The VFX needs to go in a straight horizontal line,
			// so the VFX and the capsule cast are not influenced by the target's height
			targetPosition.y = origin.y;
			
			RunCapsuleCast(targetPosition, origin);
			
			hitscanAttack.transform.position = origin;
			hitscanAttack.AttackEnemies(m_Targetables);
			
			PlayParticles(m_FireParticleSystem, origin, targetPosition);
		}

		private void RunCapsuleCast(Vector3 targetPosition, Vector3 origin)
		{
			var direction = Vector3.Normalize(targetPosition - origin);

			var capsuleSide = Vector3.up * m_CapsuleHalfHeight;
			
			var point1 = origin + capsuleSide;
			var point2 = origin - capsuleSide;

			// The capsule cast is used so the cast goes in a straight horizontal line, but hits enemies at any height.
			// The capsule that is cast is oriented vertically for this reason.
			var count = Physics.
				CapsuleCastNonAlloc(point1, point2, m_CapsuleRadius, direction, m_Hits, m_Distance, m_LayerMask.value);
			
			FillTargetablesList(count);
		}

		private void FillTargetablesList(int count)
		{
			m_Targetables.Clear();
			
			for (int i = 0; i < count; i++)
			{
				var hit = m_Hits[i];
				var targetable = hit.collider.GetComponent<Targetable>();
				if (targetable != null)
				{
					m_Targetables.Add(targetable);
				}
			}
		}
	}
}