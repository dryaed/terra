using UnityEngine;
using UnityEditor;
/// <summary>
/// This class contains a custom drawer for the ReadOnly attribute
/// </summary>
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    /// <summary>
    /// Unity method for drawing GUI in the Editor
    /// </summary>
    /// <param name="position">Position.</param> 
    /// <param name="property">Property.</param>
    /// <param name="label">Label.</param>
    /// 

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Saving previous GUI enabled value
        var previousGUIState = GUI.enabled;
        // Disabling edit for property
        GUI.enabled = false;
        // Drawing property
        EditorGUI.PropertyField(position, property, label);
        // Setting old GUI enabled value
        GUI.enabled = previousGUIState;
    }
}
