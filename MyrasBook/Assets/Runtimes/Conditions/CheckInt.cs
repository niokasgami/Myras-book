using System;
using Hollow.Management;
using Hollow.ScriptableObjects;
using Sirenix.OdinInspector;

namespace Hollow.conditions
{
  
  // is there an easier way to do this?
  public enum Operand
  {
    [LabelText("==")]
    EqualTo,
    [LabelText(">")]
    GreaterThan,
    [LabelText("<")]
    LesserThan,
    [LabelText(">=")]
    EqualOrGreaterThan,
    [LabelText("<=")]
    EqualOrLesserThan,
    
  }
  
  /// <summary>
  /// the condition class that check if an integer GameVariable meets the condition or not
  /// </summary>
  public class CheckInt : ConditionBase
  {

    [PropertySpace(4)]
    [HorizontalGroup("VarGroup"),HideLabel]
    public string variable;
    
    [PropertySpace(4)]
    [HorizontalGroup("VarGroup")]
    [HideLabel]
    public Operand operand;
    
    [PropertySpace(4)]
    [HorizontalGroup("VarGroup"),HideLabel]
    public int valueCondition;
    
    public CheckInt()
    {
      this.name = "CheckInt";
    }
    
    /// <summary>
    /// check if the variable meets the conditions
    /// </summary>
    /// <returns>true if the variable meets the conditions</returns>
    /// <exception cref="Exception"> it will throw an exception error if the variable type is not int </exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public override bool IsFulfilled()
    {
      #if UNITY_EDITOR
      return true;
      #endif
      var data = GameManager.Instance.Variables.Get(variable);
      if (data.type is not VariableType.Int)
      {
        throw new Exception($"The variable {variable} is not of type int but of type {data.type} ");
      }
      // since we know the variable type is int we can safely force an cast
      var castedVariable = int.Parse(data.value);

      // here we use the operand to return the math condition
      return operand switch
      {
        Operand.EqualTo => castedVariable == valueCondition,
        Operand.GreaterThan => castedVariable > valueCondition,
        Operand.LesserThan => castedVariable < valueCondition,
        Operand.EqualOrGreaterThan => castedVariable >= valueCondition,
        Operand.EqualOrLesserThan => castedVariable <= valueCondition,
        _ => throw new ArgumentOutOfRangeException()
      };
    }
  }
}
