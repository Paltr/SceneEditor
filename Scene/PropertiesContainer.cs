using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Util.Xml;
using SceneEditor.Export;

namespace SceneEditor.Scene
{
  class PropertiesContainer : IPropertiesContainer
  {
    #region Public methods
    
    public Dictionary<string, IProperty> UserProperties
    {
      get { return new Dictionary<string, IProperty>(m_UserProperties); }
    }
    
    public string UserPropertiesFilepath
    {
      get { return m_UserPropertiesFilepath; }
      set
      {
        if(this.UserPropertiesFilepath != value)
        {
          m_UserPropertiesFilepath = value;
          if(!string.IsNullOrEmpty(this.UserPropertiesFilepath))
          {
            string absFilepath = Program.FindFilepath(m_UserPropertiesFilepath);
            PropertiesBuilder propertiesBuilder = PropertiesBuilderCache.Request(absFilepath);
            m_UserProperties = propertiesBuilder.BuildProperties();
          }
          else
          {
            m_UserProperties = new Dictionary<string, IProperty>();
          }
        }
      }
    }
    
    #endregion
    
    #region Public virtual methods
    
    public virtual void Prepare()
    {
      Dictionary<string, IProperty> userProperties = this.UserProperties;
      foreach(var kvp in userProperties)
      {
        kvp.Value.Prepare();
      }
    }
    
    public virtual string Name
    {
      get { return m_Name; }
      set
      {
        if(this.Name != value)
        {
          History.Change();
          string previous = this.Name; 
          m_Name = value;
          if(this.NameChanged != null)
          {
            this.NameChanged(this, previous);
          }
        }
      }
    }
    
    public virtual Dictionary<string, IProperty> Properties
    {
      get { return null; }
    }
    
    #endregion
    
    #region Public events
    
    public delegate void NameChangedHandler(PropertiesContainer sender, string previous);
    
    public event NameChangedHandler NameChanged;
    
    #endregion
    
    #region Export.IPropertiesContainer interface implementation
    
    IDictionary<string, string> IPropertiesContainer.Properties
    {
      get
      {
        Dictionary<string, string> accum = new Dictionary<string, string>();
        IDictionary<string, IProperty> userProperties = this.UserProperties;
        if(userProperties != null)
        {
          foreach(KeyValuePair<string, IProperty> kvp in userProperties)
          {
            accum[kvp.Key] = kvp.Value.ToString();
          }
        }
        
        return accum;
      }
    }
    
    #endregion
    
    #region Private data
    
    private string m_Name;
    private string m_UserPropertiesFilepath;
    private Dictionary<string, IProperty> m_UserProperties = new Dictionary<string, IProperty>();
    
    #endregion
  }
}
