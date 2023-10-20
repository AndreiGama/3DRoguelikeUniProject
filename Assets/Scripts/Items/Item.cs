using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public abstract class Item
{
    public abstract string GiveName();
    public abstract string GiveDescription();
    public virtual void CreateVFX(PlayerCombatManager player) {

    }
    public virtual void OnSpawn(PlayerCombatManager player, int stacks) {

    }
    public virtual void Update(PlayerCombatManager player, int stacks) {
        // Items that require update goes here like healing.
    }
    public virtual void OnHit(PlayerCombatManager player, IDamagable damagable, int stacks) {
        //On Hit Effect items
    }
    public virtual void OnAoeHit(PlayerCombatManager player, IDamagable damageable, int stacks) {

    }
    public virtual void OnStatChange(PlayerCombatManager player, int stacks) {
    }
    public virtual void OnKill(PlayerCombatManager player, int stacks) {

    }
}

public class HealingCrystal : Item {
    float cooldown;
    public override string GiveName() {
        return "Crystal of Healing";
    }

    public override string GiveDescription() {
        return "The crystal of healing has the power to heal the user for 5 health per item collected of the same type.";
    }

    public override void Update(PlayerCombatManager player, int stacks) {
        cooldown -= 1;
        if(cooldown <= 0) {
            player.health += 5 * stacks;
            cooldown = 5;
        }
        
    }
}

public class PoisonCloudCrystal : Item {
    GameObject effect;
    float cooldown;
    int damage = 5;
    public override string GiveName() {
        return "Crystal of Poison Clouds";
    }

    public override string GiveDescription() {
        return "Create a cloud of poison around the player which damages enemies who enter for 5 damage per stack every second";
    }

    // Create poisonCloud script and add a reference to it and call doDamage from there.
    public override void CreateVFX(PlayerCombatManager player) {
        if (effect == null) effect = (GameObject)Resources.Load("ItemEffects/VFXCrystalPoisonCloud", typeof(GameObject));
        GameObject poisonCloud = GameObject.Instantiate(effect, player.transform.position, Quaternion.Euler(Vector3.zero));
    }

    public override void Update(PlayerCombatManager player, int stacks) {
        cooldown -= 1;
    }
    public override void OnAoeHit(PlayerCombatManager player, IDamagable damageable, int stacks) {
        if(cooldown <= 0) {
            damageable.doDamage(damage * stacks);
            cooldown = 2;
        }
        
    }
}

public class SpeedCrystal : Item {
    public override string GiveName() {
        return "Crystal of Speed";
    }

    public override string GiveDescription() {
        return "The crystal of speed grants the user the ability to move quickly and attack faster by 10% per stack";
    }

    public override void OnStatChange(PlayerCombatManager player, int stacks) {
        player.movementSpeedAmplifier = player.baseAplifierValue + stacks / 10f;
        player.attackSpeedAmplifier = player.baseAplifierValue + stacks / 10f;
        player.AmplifyStats();
    }
}

public class DeffenseCrystal : Item {
    public override string GiveName() {
        return "Crystal of Deffense";
    }

    public override string GiveDescription() {
        return "The crystal of deffense will amplify the user's maximum health points, maximum shield capacity and armor by 10% per stack";
    }

    public override void OnStatChange(PlayerCombatManager player, int stacks) {
        Debug.Log("Stats are being adjusted");
        player.healthAmplifier = player.baseAplifierValue + stacks / 10f;
        player.shieldAmplifier = player.baseAplifierValue + stacks / 10f;
        player.armorAmplifier = player.baseAplifierValue + stacks / 10f;
        player.AmplifyStats();
    }
}

public class PowerCrystal : Item {
    public override string GiveName() {
        return "Crystal of Power";
    }

    public override string GiveDescription() {
        return "The crystal of Power grants the user amplified damage for both abilities and their main attacks";
    }

    public override void OnStatChange(PlayerCombatManager player, int stacks) {
        player.abilityDamageAmplifier = player.baseAplifierValue + stacks / 10f;
        player.abilityDamageAmplifier = player.baseAplifierValue + stacks / 10f;
    }
}

public class RejuvenateCrystal : Item {
    int healthPerKill;
    public override string GiveName() {
        return "Crystal of rejuvenation";
    }
    public override string GiveDescription() {
        return "The crystal of rejuvenation will grant the user the ability to gain 5 health per enemy slayn. Every stack grants 5 more hp";
    }

    public override void OnKill(PlayerCombatManager player, int stacks) {
        player.Heal(healthPerKill);
    }
}
