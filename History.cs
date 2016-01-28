using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor
{
  static class History
  {
    #region Public static methods
    
    public static void Change()
    {
      m_Changed = true;
    }
    
    public static void ResetChanges()
    {
      m_Changed = false;
    }
    
    public static bool ProjectChanged
    {
      get { return m_Changed; }
    }
    
    #endregion
    
    #region Private static methods
    
    private static bool m_Changed;
    
    #endregion
  }
}
