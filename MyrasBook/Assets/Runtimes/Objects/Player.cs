using System;
using UnityEngine;

namespace Hollow.Objects
{
  public class Player : MonoBehaviour
  {
    public static Player Instance { get; private set; }

    private Inventory inventory;
    private void Awake()
    {
      if (Instance != null && Instance != this)
      {
        Destroy(this);
      }
      else
      {
        Instance = this;
      }
    }

    private void Start()
    {
      inventory = new Inventory();
    }

    public Inventory Inventory()
    {
      return inventory;
    }

    /// <summary>
    /// Check if the inventory has an item with the given key.
    /// </summary>
    /// <param name="key">The key of the item to be checked.</param>
    /// <returns>True if the inventory has an item with the specified key; otherwise, false.</returns>
    public bool HasItem(string key)
    {
      return inventory.HasItem(key);
    }

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="key">The key of the item to be added.</param>
    /// <param name="slot">The slot where the item will be added.</param>
    public void AddItem(string key, int slot)
    {
      inventory.AddItem(key, slot);
    }

    public void RemoveItem(string key)
    {
      inventory.RemoveItem(key);
    }

    /// <summary>
    /// Finds the first empty slot in the inventory.
    /// </summary>
    /// <returns>
    /// The index of the first empty slot in the inventory. Returns -1 if no empty slot is found.
    /// </returns>
    public int FindEmptySlot()
    {
      for (var i = 0; i < inventory.maxSlot; i++)
      {
        if (inventory.IsSlotAvailable(i))
        {
          return i;
        }
      }
      return -1;
    }
    
  }
}
