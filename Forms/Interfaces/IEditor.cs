using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneEditor.Scene;

namespace SceneEditor.Forms.Interfaces
{
  delegate void EditorSelectedSceneChangedHandler(IEditor sender, Scene.Scene previous);
  delegate void EditorSelectedShapeChangedHandler(IEditor sender, Shape previous);
  delegate void EditorActiveTemplateChangedHandler(IEditor sender, ShapeTemplate previous);
  delegate void EditorShapeSelectionModeHandler(IEditor sender, Shape shape);
  
  interface IEditor
  {
    Scene.Scene SelectedScene { get; set; }
    Shape SelectedShape { get; set; }
    ShapeTemplate ActiveTemplate { get; set; }
    
    void StartShapeSelection(EditorShapeSelectionModeHandler handler);
    
    event EditorSelectedSceneChangedHandler SelectedSceneChanged;
    event EditorSelectedShapeChangedHandler SelectedShapeChanged;
    event EditorActiveTemplateChangedHandler ActiveTemplateChanged;
  }
}
