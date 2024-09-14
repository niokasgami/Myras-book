using System;
using Hollow.Objects;
using UnityEngine;

namespace Hollow.Management
{
  public class GameManager : MonoBehaviour
  {
    public static GameManager Instance { get; private set; }

    public event Action OnGuiRefresh;
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

    /// <summary>
    /// Will throw a signal and request an refresh from all the subscribed GUI elements
    /// </summary>
    // TODO : might be a good idea to not depends to much on this?
    public void RequestGuiRefresh()
    {
      OnGuiRefresh?.Invoke();
    }

    public void StartPuzzle(Canvas canvas, string variable)
    {
      Instantiate(canvas);
    }
  }
}
