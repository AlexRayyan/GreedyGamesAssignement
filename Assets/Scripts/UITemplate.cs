using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Unity.VisualScripting;

[Serializable]
public class UITemplate
{
    public Canvas Canvas;

    public void SerializeToJson()
    {
        string filePath = "Assets/Resources/";
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        string json = JsonUtility.ToJson(this);
        File.WriteAllText(filePath + "JSONText.json", json);
    }

    public static UITemplate DeserializeFromJson(string json)
    {
        return JsonUtility.FromJson<UITemplate>(json);
    }
}

[System.Serializable]
public class Canvas
{
    public string canvasName;
    public Vector2 canvasPosition;
    public Vector2 canvasSize;
    public Panel canvasChildren;
}

[System.Serializable]
public class Panel
{
    public string panelName;
    public Vector2 panelPosition;
    public Vector2 panelSize;
    public Color panelBackgroundColor;
    public UIElement panelChildren;
}

[System.Serializable]
public class UIElement
{
    public Button Button;
    public Label Label;
}

[System.Serializable]
public class Button
{
    public string buttonName;
    public Vector2 buttonPosition;
    public Vector2 buttonSize;
    public string buttonText;
}

[System.Serializable]
public class Label
{
    public string labelName;
    public Vector2 labelPosition;
    public Vector2 labelSize;
    public string labelText;
}

