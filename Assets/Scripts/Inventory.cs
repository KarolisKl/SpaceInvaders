using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour
{
    public static Inventory instance;
    public int space = 4;
    public List<Item> items = new List<Item>();
    public Slot[] slots;
    int[] lootChances = { 100 };// 50% - basicLoot (points, armor), 30% - weapons, 20% - powerups;  Sadly no time to develop it :(
    Item[][] lootTables;

    void Awake ()
    {
        instance = this;
        slots = new Slot[space];
        for(int i =0; i<space; i++)
        {
            slots[i] = GameObject.Find("Slot" + i).GetComponent<Slot>();
        }
    }

    private void Start()
    {
        InitializeItems();
    }

    void InitializeItems()
    {
        Object[][] objects = new Object[][]
        {
            Resources.LoadAll("Items/Basic", typeof(Basic))
           // Resources.LoadAll("Items/PowerUps", typeof(Weapon)), // not developed :(
           // Resources.LoadAll("Items/Weapons", typeof(PowerUp)) // not developed :(

        };

        List<Item> basicItems = new List<Item>();
        foreach (Object obj in Resources.LoadAll("Items/Basic", typeof(Basic)))
        {
            basicItems.Add((Basic)obj);
        }
     /*   List<Item> weaponItems = new List<Item>();
        foreach (Object obj in Resources.LoadAll("Items/Weapons", typeof(Weapon)))
        {
            weaponItems.Add((Weapon)obj);
        }
        List<Item> powerUpItems = new List<Item>();
        foreach (Object obj in Resources.LoadAll("Items/Basic", typeof(PowerUp)))
        {
            powerUpItems.Add((PowerUp)obj);
        }

        lootTables = new Item[][]{
            basicItems.ToArray(),
            weaponItems.ToArray(),
            powerUpItems.ToArray()
        };*/
    }

    public void Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Not enough space");
            return;
        }

        items.Add(item);
        foreach(Slot slot in slots)
        {
            if(!slot.item)
            {
                slot.AddItem(item);
                return;
            }
        }
    }

    public void Remove(Item item)
    {

        items.Remove(item);

    //   if (onItemChangedCallback != null)
         //   onItemChangedCallback.Invoke();
    }

    // return item from drop table
    public Item DropItem(int value)
    {
        int tableIndex = 0;
        while (tableIndex < lootChances.Length)
        {
            if (value <= lootChances[tableIndex])
            {
                break;
            }
            tableIndex++;
        }
        Debug.Log(tableIndex);
        int itemIndex = Random.Range(0, lootTables[tableIndex].Length);
        return lootTables[tableIndex][itemIndex];
    }
}

