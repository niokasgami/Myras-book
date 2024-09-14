using System.Collections.Generic;
using System.Linq;
using Hollow.ScriptableObjects;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Hollow.Editor
{
  public class ItemTable
  {
        [TableList(IsReadOnly = true, AlwaysExpanded = true ), ShowInInspector]
        private readonly List<ItemWrapper> allItems;

        public ItemBase this[int index]
        {
          get { return this.allItems[index].Item; }
        }

        public ItemTable(IEnumerable<ItemBase> items)
        {
          this.allItems = items.Select(x => new ItemWrapper(x)).ToList();
        }

        private class ItemWrapper
        {
          private ItemBase item;

          public ItemBase Item
          {
            get { return this.item; }
          }

          public ItemWrapper(ItemBase item)
          {
            this.item = item;
          }
          
          
          [TableColumnWidth(50, false)]
          [ShowInInspector, PreviewField(45, ObjectFieldAlignment.Center)]
          public Sprite Sprite
          {
            get => this.item.image;
            set
            {
              this.item.image = value;
              EditorUtility.SetDirty(this.item);
            }
          }
          
          [TableColumnWidth(120, resizable: false)]
          [ShowInInspector]
          public string Key
          {
            get { return this.item.key; }
            set
            {
              this.item.key = value;
              EditorUtility.SetDirty(this.item);
            }
          }
          

          [TableColumnWidth(120, resizable: false)]
          [ShowInInspector]
          public string DisplayName
          {
            get { return this.item.displayName; }
            set
            {
              this.item.displayName = value;
              EditorUtility.SetDirty(this.item);
            }
          }

        }
        }
  }

