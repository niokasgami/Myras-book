using System;
using System.Collections.Generic;
using System.Data;
using Hollow.Management;
using Hollow.ScriptableObjects;

namespace Hollow.Objects
{
  /// <summary>
  /// The class that manage and store in Game variables.
  /// The game handle both savefile 
  /// </summary>
  public class GameVariables
  {

    private Dictionary<string, VariableContainer> locals = new Dictionary<string, VariableContainer>();
    private Dictionary<string, VariableContainer> globals = new Dictionary<string, VariableContainer>();
    
    /// <summary>
    /// will try to fetch both from the global and local scope for a variable
    /// </summary>
    /// <param name="name">the variable name</param>
    /// <returns></returns>
    /// <exception cref="Exception">Will throw an Exception error if not variables are declared</exception>
    public VariableContainer Get(string name)
    {
      if (locals.ContainsKey(name)) return locals[name];
      if (globals.ContainsKey(name)) return globals[name];
      throw new Exception($"The variable {name} is not defined!");
    }

    public void Set(string name, string value)
    {
      var data = Get(name);
      data.value = value;
    }

    public GameVariables()
    {
      BuildLocalVariables();
      BuildGlobalVariables();
    }

    private void BuildLocalVariables()
    {
      var container = Database.localVariables;
      foreach (var variable in container)
      {
        locals.Add(variable.name,variable);
      }
    }

    private void BuildGlobalVariables()
    {
      var container = Database.globalVariables;
      foreach (var variable in container)
      {
        globals.Add(variable.name,variable);
      }
    }
  }
}
