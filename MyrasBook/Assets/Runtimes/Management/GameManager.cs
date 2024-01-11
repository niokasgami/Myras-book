using System;
using Hollow.Objects;
using UnityEngine;

namespace Hollow.Management
{
  public class GameManager : MonoBehaviour
  {
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
      // in this case we making an singleton since the GameManager is kinda supposed to be accessible everywhere.
      if (Instance != null && Instance != this)
      {
        Destroy(this);
      }
      else
      {
        Instance = this;
      }
    }

    public GameVariables Variables { get; set; }

    private void Start()
    {
      Database.LoadDatabase();
      Variables = new GameVariables();
    }
  }
}
