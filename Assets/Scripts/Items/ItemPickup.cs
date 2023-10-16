using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemPickup : MonoBehaviour {
    public Item item;
    public Items itemDrop;

    private void Start() {
        // Depending on the item from the enumerator selected assigned the variable Item to values of the actual item
        item = AssignItem(itemDrop);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Entity is being colided with");
        PlayerCombatManager player = other.GetComponent<PlayerCombatManager>();
        if(player != null) { 
            AddItem(player);
            Destroy(this.gameObject);
        }
    }
    void AddItem(PlayerCombatManager player) {
        foreach(ItemList i in player.items) {
            if (i.name == item.GiveName()) {
                i.stacks += 1;
                return;
            }
        }
        player.items.Add(new ItemList(item, item.GiveName(), 1));
    }

    //Assigns item depending on the value of the enumerator in the inspector.
    public Item AssignItem(Items itemToAssign) {
        switch (itemToAssign) {
            case Items.HealingCrystal:
                return new HealingCrystal();
            case Items.PoisonCloudCrystal: 
                return new PoisonCloudCrystal();
            case Items.SpeedCrystal:
                return new SpeedCrystal();
            case Items.DeffenseCrystal: 
                return new DeffenseCrystal();
            case Items.PowerCrystal: 
                return new PowerCrystal();
            case Items.RejuvenateCrystal: 
                return new RejuvenateCrystal();
            default:
                return new HealingCrystal();
        }
    }
    
}
//List of items each pickup will be assigned to.
public enum Items {
    HealingCrystal,
    PoisonCloudCrystal,
    SpeedCrystal,
    DeffenseCrystal,
    PowerCrystal,
    RejuvenateCrystal
}
