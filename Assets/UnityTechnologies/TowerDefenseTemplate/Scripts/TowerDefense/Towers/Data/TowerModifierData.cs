using UnityEngine;

namespace TowerDefense.Towers.Data
{
	/// <summary>
	/// Data container for settings per tower level
	/// </summary>
	[CreateAssetMenu(fileName = "TowerModifierData.asset", menuName = "TowerDefense/Tower Modifier", order = 1)]
	public class TowerModifierData : ScriptableObject
	{
		/// <summary>
		/// A description of the modifier for displaying on the UI
		/// </summary>
		public string modifierDescription;
		
		/// <summary>
		/// The cost to upgrade this modifier
		/// </summary>
		public int modifierCost;
	}
}