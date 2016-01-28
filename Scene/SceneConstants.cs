using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SceneEditor.Scene
{
  static class SceneConstants
  {
    public static float SceneScale
    {
      get { return m_SceneScale; }
      set { m_SceneScale = value; }
    }
    
    public static float ManipRadius
    {
      get { return 7.0f * 1.0f / SceneConstants.SceneScale; }
    }
    
    public static float AngleManipLength
    {
      get { return 100.0f; }
    }
    
    public static float PenWidth
    {
      get { return 3.0f; }
    }

    public static Pen GridPen
    {
      get
      {
        Color color = Color.FromArgb(31, Color.Black);
        Pen pen = new Pen(color);
        return pen;
      }
    }
    
    public static Pen HighlightPen
    {
      get
      {
        Color color = Color.FromArgb(127, Color.Blue);
        Pen pen = new Pen(color);
        pen.Width = SceneConstants.PenWidth;
        return pen;
      }
    }
    
    public static Pen ManipPen
    {
      get
      {
        Color color = Color.FromArgb(127, Color.Blue);
        Pen pen = new Pen(color);
        pen.Width = SceneConstants.PenWidth;
        return pen;
      }
    }
    
    private static float m_SceneScale = 1.0f;
  }
}
