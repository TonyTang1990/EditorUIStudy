/*
 * Description:             TPreviewDrawer.cs
 * Author:                  TONYTANG
 * Create Date:             2022/02/21
 */

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// TPreviewDrawer.cs
/// 预览绘制
/// </summary>
[CustomPropertyDrawer(typeof(TPreviewAttribute))]
public class TPreviewDrawer : PropertyDrawer
{
    /// <summary>
    /// 调整整体高度
    /// </summary>
    /// <param name="property"></param>
    /// <param name="label"></param>
    /// <returns></returns>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return base.GetPropertyHeight(property, label) + 64f;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.PropertyField(position, property, label);

        Texture2D previewTexture = GetAssetPreview(property);
        if (previewTexture != null)
        {
            Rect previewRect = new Rect()
            {
                x = position.x + GetIndentLength(position),
                y = position.y + EditorGUIUtility.singleLineHeight,
                width = position.width,
                height = 64
            };
            GUI.Label(previewRect, previewTexture);
        }
        EditorGUI.EndProperty();
    }
    
    /// <summary>
    /// 获取显示缩进间隔
    /// </summary>
    /// <param name="sourceRect"></param>
    /// <returns></returns>
    private float GetIndentLength(Rect sourceRect)
    {
        Rect indentRect = EditorGUI.IndentedRect(sourceRect);
        float indentLength = indentRect.x - sourceRect.x;

        return indentLength;
    }

    /// <summary>
    /// 获取Asset预览显示纹理
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    private Texture2D GetAssetPreview(SerializedProperty property)
    {
        if (property.propertyType == SerializedPropertyType.ObjectReference)
        {
            if (property.objectReferenceValue != null)
            {
                Texture2D previewTexture = AssetPreview.GetAssetPreview(property.objectReferenceValue);
                return previewTexture;
            }
            return null;
        }
        return null;
    }
}