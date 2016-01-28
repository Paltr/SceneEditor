using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Scene
{
  static class PropertiesBuilderCache
  {
    #region Constructors
    
    static PropertiesBuilderCache()
    {
      m_Cache = new Dictionary<string, PropertiesBuilder>();
    }
    
    #endregion
    
    #region Public methods
    
    public static PropertiesBuilder Request(string filepath)
    {
      PropertiesBuilder result = null;
      if(!m_Cache.TryGetValue(filepath, out result))
      {
        result = new PropertiesBuilder(filepath);
        m_Cache[filepath] = result;
      }
      
      return result;
    }
    
    #endregion
    
    #region Private data
    
    private static readonly Dictionary<string, PropertiesBuilder> m_Cache;
    
    #endregion
  }
}
