using System.Collections.Generic;

namespace Hollow.Objects
{
  /// <summary>
  /// Represents a class that manages bookmarks.
  /// </summary>
  public class Bookmarks
  {
    private readonly List<string> slots;
    private readonly int maxSlots = 6;
    private int activeSlots;
    
    public Bookmarks()
    {
      activeSlots = 3;
      slots = new List<string>();
      OnSlotIncrease();
    }

    /// <summary>
    /// Retrieves the string value at the specified index in the slots array.
    /// </summary>
    /// <param name="index">The index of the slot to retrieve the value from.</param>
    /// <returns>The string value at the specified index in the slots array.</returns>
    public string Slot(int index)
    {
      return slots[index];
    }

    public List<string> Slots()
    {
      return slots;
    }
    
    /// <summary>
    /// Writes a page to a specified slot in the slot array.
    /// </summary>
    /// <param name="page">The content of the page to be written.</param>
    /// <param name="slot">The index of the slot where the page should be written to.</param>
    public void Write(string page, int slot)
    {
      slots[slot] = page;
    }

    /// <summary>
    /// Destroys the item at the specified slot.
    /// </summary>
    /// <param name="slot">The index of the slot to destroy.</param>
    public void Destroy(int slot)
    {
      slots[slot] = null;
    }
    
    /// <summary>
    /// Increases the number of active slots up to the specified maximum.
    /// </summary>
    /// <param name="max">The maximum number of slots.</param>
    public void IncreaseSlot(int max)
    {
      if (activeSlots == maxSlots) return;
      activeSlots += max;
      OnSlotIncrease(); 
    }

    /// <summary>
    /// Check whether the specified slot is written or not.
    /// </summary>
    /// <param name="slot">The index of the slot to be checked.</param>
    /// <returns>Returns true if the slot is written; otherwise, false.</returns>
    public bool IsSlotWritten(int slot)
    {
      return slots[slot] is not null;
    }

    /// <summary>
    /// This method returns the number of active slots.
    /// </summary>
    /// <returns>The number of active slots.</returns>
    public int ActiveSlots()
    {
      return activeSlots;
    }
    /// <summary>
    /// This method is called when the number of active slots increases. It adds null values to the slots list to match the new number of active slots.
    /// </summary>
    private void OnSlotIncrease()
    {
      var range = activeSlots - slots.Count;
      for (var i = 0; i < range; i++)
      {
        slots.Add(null);
      }
      // TODO : actually force a refresh of the bookmarks
    }
  }
}
