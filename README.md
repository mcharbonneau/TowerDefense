# TowerDefense

## Step 2, Option B
For the extension, I chose to extend the behavior of 3 of the towers with what I called modifier upgrades. These are available in the tower menu under the upgrade button and allow the player to add a modification to the tower by paying a fee, upgrading, and changing how it behaves. This design results in a less linear upgrade tree.

The upgrades are as follows:
- The assault cannon can be modified to only attack flying enemies, but with a 50% higher rate of fire at any given level, to make it into an anti-aircraft specialist. I felt that flying enemies could be an issue when they were moving in the middle of a group of other units since the AOE tower couldn’t hit them, and other towers could focus on any other towers. With this upgrade, the player now has the option to spend to make a tower that will alleviate the issue.
- The plasma lance can be modified to hit every enemy in its line of power, allowing for another unit with AOE but with a different shape.
- The EMP tower can now deal a bit of continuous damage to every enemy in its slowing range. This damage scales a bit with the level of the tower. This will allow for another type of AOE, this time all around a tower with limited range.

## Step 3
After testing the design, it does allow for fun combinations with the shapes of the maze that the player can make. A plasma lance with the modification can deal a lot of damage if it’s at one end of a long corridor. A modified EMP tower can be very useful if it’s placed at a corner, allowing it to attack any number of units around itself. It also gives synergy with its slowing effect since slowing a unit will make it move slower in its range, allowing for more damage. It won’t single-handedly kill many units, but can weaken a bunch of them before they get picked up by other towers. As for the assault cannon, it covers a role that was lacking before. However, I think both the plasma lance and the EMP tower throw off the balancing of the game a bit. A well-positioned plasma lance can be absolutely devastating, same for the EMP tower, so their cost or their strength should probably be balanced more adequately. Also, since the modification effects scale with the level, upgrading a level should be more expensive with the modification, and the modification should be more expensive with higher levels. I also added two towers with AOE, which may be a bit much in the current state of the game.

As for the code, in order to fit into the code structure of the existing project, I added modified versions of each tower level’s prefab, so it effectively doubles the number of prefabs. I feel this system would not be the best option for a real project. The number of different prefabs would become untenable, so a major refactor of the entire upgrade system would be needed. I also followed the naming convention of the existing project, but it feels a bit all over the place, so that would probably need to be refactored as well. I also added code to already sprawling classes like Tower and TowerUI, and those would ideally benefit from being split in order to respect the single responsibility principle.

## Notes
It took a bit more time than I thought it would, because some of my original ideas were too complicated to implement in the existing project for the scope of this test, so they were abandoned. It took me approximately 8 hours of active work to complete this.

On top of multiple prefabs and scriptable objects I modified and created for the 3 different towers (EMP, MachineGun, Laser) and TowerControllerUI.prefab, here are lists of script changes I did.

I modified:
- TowerUI.cs
- Tower.cs
- GameUI.cs

I created:
- PiercingHitscanAttack.cs
- PiercingHitscanLauncher.cs
- ProximityDamageAffector.cs
- TowerModifierData.cs

You can also check the git commits I made for this test, but please note that the initial commit already had some changes made for the modifier upgrade system.
