using Hollow.Management;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

namespace Hollow.Actions
{
  // TODO : maybe I should allow an list of settable variable? 
  // it seems to be the most proper way so we can avoid to create multiple variables
  public class SetVariable : ActionBase
  {

    [PropertySpace(4)]
    [HorizontalGroup("VarGroup"),LabelWidth(70)]
    public string variable;
    
    [PropertySpace(4)]
    [HorizontalGroup("VarGroup"),LabelWidth(70)]
    public string value;
    
    public SetVariable()
    {
      this.name = "SetVariable";
    }
    public override void OnAction()
    {
      GameManager.Instance.Variables.Set(variable,value);
    }
  }
}
