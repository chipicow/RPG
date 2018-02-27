using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

   new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public Rarity rarity;


    
    public virtual void Use()
    {

    }

    public  void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary
}