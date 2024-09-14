using Hollow.Management;
using UnityEngine;

namespace Hollow.Actions
{
  public class StartPuzzle : ActionBase
  {

    public Canvas canvas;
    public string variable;
    public override void OnAction()
    {
      GameManager.Instance.StartPuzzle(canvas, variable);
    }
  }
}
