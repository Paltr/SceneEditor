using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;
using System.IO;
using Util.Xml;
using GLRenderer;

namespace SceneEditor
{
  static class Program
  {
    #region Static contructor
    
    static Program()
    {
      m_SettingsFilepath = FindFilepath("EditorSettings.xml");
      m_TextureManager = new TextureManager();
      m_MaterialFactory = new MaterialFactory(m_TextureManager);
    }
    
    #endregion
    
    #region Public static methods
    
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      ParseArgs(args);
      Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }
    
    public static MaterialFactory MaterialFactory
    {
      get { return m_MaterialFactory; }
    }
    
    public static string FindFilepath(string filepath)
    {
      if(File.Exists(filepath))
      {
        return filepath;
      }
      
      string absFilepath = AppDomain.CurrentDomain.BaseDirectory + filepath;
      if(File.Exists(absFilepath))
      {
        return absFilepath;
      }
      
      absFilepath = AppDomain.CurrentDomain.BaseDirectory + "../../" + filepath;
      if (File.Exists(absFilepath))
      {
        return absFilepath;
      }
      
      return null;
    }
    
    public static Document FindDocument(string filepath)
    {
      filepath = FindFilepath(filepath);
      if(filepath != null)
      {
        return Document.CreateFromFile(filepath);
      }
      
      return null;
    }
    
    public static string SettingsFilepath
    {
      get { return m_SettingsFilepath; }
    }
    
    #endregion
    
    #region Private methods
    
    private static void ParseArgs(string[] args)
    {
      if(args.Length % 2 != 0)
      {
        throw new ArgumentException("Invalid arguments amount: must be a multiple of 2");
      }
      
      int index = 0;
      while(index < args.Length)
      {
        string key = args[index++];
        string value = args[index++];
        switch(key)
        {
          case "-S":
          {
            m_SettingsFilepath = FindFilepath(value);
            break;
          }
          
          default:
          {
            throw new ArgumentException("Unknown argument key " + key);
          }
        }
      }
    }
    
    #endregion
    
    #region Private static data
    
    private static string m_SettingsFilepath;
    private static TextureManager m_TextureManager;
    private static readonly MaterialFactory m_MaterialFactory;
    
    #endregion
  }
}
