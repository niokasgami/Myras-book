using System;
using System.Linq;
using Hollow.Management;
using Hollow.ScriptableObjects;
using UnityEngine;

namespace Hollow.Objects
{
  /// <summary>
  /// Represents an inventory that can store items.
  /// </summary>
  public class Inventory
  {
    
    public event Action OnInventoryChanged;
    
    private readonly ItemBase[] slots;
    public readonly int maxSlot = 8;

    public Inventory()
    {
      slots = new ItemBase[maxSlot];
    }
    
    /// <summary>
    /// Adds an item to the slot at the specified index, if it is available.
    /// </summary>
    /// <param name="key">The key of the item to add.</param>
    /// <param name="index">The index of the slot to add the item.</param>
    /// <remarks>
    /// If the item with the specified key is already present in any other slot, this method will do nothing.
    /// If the specified index is invalid or the slot is already occupied, an error message will be logged.
    /// </remarks>
    public void AddItem(string key, int index)
    {
      if (HasItem(key)) return;

      if (IsSlotAvailable(index))
      {
        var data = GetData(key);
        slots[index] = data;
        OnInventoryChanged?.Invoke();
      }
      else
      {
        Debug.LogError("the slot isn't available!");
      }
    }
    
    /// <summary>
    /// Remove an item from the collection using the specified key.
    /// </summary>
    /// <param name="key">The key of the item to be removed.</param>
    public void RemoveItem(string key)
    {
      if (!HasItem(key)) return;
      for (var i = 0; i < slots.Length; i++)
      {
        if (slots[i].key == key)
        {
          slots[i] = null;
          OnInventoryChanged?.Invoke();
          break;
        }
      }
    }

    /// <summary>
    /// Retrieves an item from the slots based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the item to retrieve.</param>
    /// <returns>The item with the provided ID if found; otherwise, returns null.</returns>
    public ItemBase Get(string id)
    {
      foreach (var item in slots)
      {
        if (item.key == id)
        {
          return item;
        }
      }
      return null;
    }
    
    /// <summary>
    /// Checks if there is an item with the given key in the collection.
    /// </summary>
    /// <param name="key">The key to search for.</param>
    /// <returns>True if an item with the given key exists; otherwise, false.</returns>
    public bool HasItem(string key)
    {
      return slots.Where(item => item != null).Any(item => item.key == key);
    }

    /// <summary>
    /// Checks if a slot is available at a given index.
    /// </summary>
    /// <param name="index">The index of the slot to check.</param>
    /// <returns>True if the slot at the given index is available, otherwise false.</returns>
    public bool IsSlotAvailable(int index)
    {
      return slots[index] is null;
    }

    /// <summary>
    /// Retrieves the key data from the slots array for saving.
    /// </summary>
    /// <returns>An array of string containing the key data.</returns>
    public string[] DataForSave()
    {
      var data = new string[maxSlot];
      for (var i = 0; i < slots.Length; i++)
      {
        if (slots[i] is not null)
        {
          data[i] = slots[i].key;
        }
      }
      return data;
    }

    public void RestoreData(string[] data)
    {
      for (var i = 0; i < data.Length; i++)
      {
        if (!string.IsNullOrEmpty(data[i]))
        {
          slots[i] = Database.items[data[i]];
        }
      }
    }

    /// <summary>
    /// retrieves the item descriptions at the given positions
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public string Description(int slot)
    {
      return slots[slot] is null ? "" : slots[slot].description;
    }

    public string DisplayName(int slot)
    {
      return slots[slot] is null ? "" : slots[slot].displayName;
    }

    public Sprite Sprite(int slot)
    {
      return slots[slot] is null ? null : slots[slot].image;
    }
    
    /// <summary>
    /// Retrieves the item associated with the specified key.
    /// </summary>
    /// <param name="key">The key used to retrieve the item.</param>
    /// <returns>The item associated with the specified key.</returns>
    private ItemBase GetData(string key)
    {
      return Database.items[key];
    }
  }
}
