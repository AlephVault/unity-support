using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace AlephVault.Unity.Support
{
    namespace Types
    {
        /// <summary>
        ///   <para>
        ///     This layer class is needed so the default rendering of it
        ///     is the custom LayerField-implementing editor.
        ///   </para>
        /// </summary>
        [Serializable]
        public struct Layer
        {
            /// <summary>
            ///   The layer to cast.
            /// </summary>
            [SerializeField]
            private int layerIndex;
            
            /// <summary>
            ///   Casts the Layer to an integer implicitly
            /// </summary>
            /// <param name="layer">The layer to cast</param>
            /// <returns>The corresponding int value</returns>
            public static implicit operator int(Layer layer)
            {
                return layer.layerIndex;
            }
            
            /// <summary>
            ///   Casts the layer from an integer implicitly.
            /// </summary>
            /// <param name="layerIndex">the int to cast</param>
            /// <returns>The corresponding Layer value</returns>
            public static implicit operator Layer(int layerIndex)
            {
                return new Layer { layerIndex = layerIndex };
            }
        }
        
        #if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(Layer))]
        public class LayerPropertyDrawer : PropertyDrawer
        {
            public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
            {
                EditorGUI.BeginProperty(_position, GUIContent.none, _property);
                SerializedProperty layerIndex = _property.FindPropertyRelative("layerIndex");
                _position = EditorGUI.PrefixLabel(_position, GUIUtility.GetControlID(FocusType.Passive), _label);
                if (layerIndex != null)
                {
                    layerIndex.intValue = EditorGUI.LayerField(_position, layerIndex.intValue);
                }
                EditorGUI.EndProperty();
            }
        }
        #endif
    }
}
