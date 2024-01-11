using System.Collections;
using System.Collections.Generic;
using Hollow.Management;
using Hollow.Objects;
using Hollow.ScriptableObjects.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Hollow
{
    public class TEST : MonoBehaviour
    {
      void Start()
      {
       // Player.Instance.Inventory().OnInventoryChanged += Refresh;
      }

      void Refresh()
      {
        Debug.Log("The item has been updated!");
      }
      [Button]
      void TestData()
      {
        Player.Instance.AddItem("Lantern",1);
      }
      
    }
}
