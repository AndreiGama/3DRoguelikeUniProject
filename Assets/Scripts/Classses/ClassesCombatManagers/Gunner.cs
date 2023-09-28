using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : PlayerCombatManager
{
    
    public override void PrimaryAttackLogic() {
        // Primary Attack Logic
        animator.PlayTargetActionAnimation("HandgunShoot", true);
    }

    public override void SecondaryAttackLogic() {
        // Secondary Attack Logic
        animator.PlayTargetActionAnimation("HandGunTrippleShoot", true);
    }

    public override void Abillity1Logic() {
        // First Abilitty Logic
    }

    public override void Abillity2Logic() {
        // Second Abillity logic
    }

    public override void UltimateAbillityLogic() {
        // Ultimate Abillity logic
    }
}
