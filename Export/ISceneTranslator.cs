using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Export
{
  public interface ISceneTranslator
  {
    string Translate(IScenesSet scenesSet, IScene scene);
  }
}
