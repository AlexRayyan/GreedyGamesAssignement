using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using UnityEngine.UI;
using System;

public class JSONUITemplateEditorWindow : EditorWindow
{
    private List<UITemplate> templates = new List<UITemplate>();
    private UITemplate newTemplate;
    private string newTemplateName = "";
    public string canvasName;
    public Vector2 canvasPosition;
    public Vector2 canvasSize;

    public string panelName;
    public Vector2 panelPosition;
    public Vector2 panelSize;
    public Color panelBackgroundColor;

    public string buttonName;
    public Vector2 buttonPosition;
    public Vector2 buttonSize;
    public string buttonText;

    public string labelName;
    public Vector2 labelPosition;
    public Vector2 labelSize;
    public string labelText;

    private int selectedTemplateIndex = -1;

    private void OnEnable()
    {
        string filePath = "Assets/Resources/JSONText.json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            templates = JsonUtility.FromJson<List<UITemplate>>(json);
        }
        else
        {
            Debug.LogError("File 'JSONText.json' not found at path: " + filePath);
        }
    }

    [MenuItem("Window/JSON UI Template Editor")]
    public static void ShowWindow()
    {
        JSONUITemplateEditorWindow window = GetWindow<JSONUITemplateEditorWindow>();
        window.titleContent = new GUIContent("JSON UI Template Editor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("UI Template Editor", EditorStyles.boldLabel);

        string[] templateNames = templates.Select(t => t.Canvas.canvasName).ToArray();
        selectedTemplateIndex = EditorGUILayout.Popup("Select Template", selectedTemplateIndex, templateNames);

        foreach (var template in templates)
        {
            GUILayout.Label(template.Canvas.canvasName);
        }

        GUILayout.Space(10);

        GUILayout.Label("Create New Template", EditorStyles.boldLabel);
        GUILayout.Space(10);
        newTemplateName = EditorGUILayout.TextField("Template Name", newTemplateName);
        canvasName = EditorGUILayout.TextField("Canvas Name", canvasName);
        canvasPosition = EditorGUILayout.Vector2Field("Canvas Position", canvasPosition);
        canvasSize = EditorGUILayout.Vector2Field("Canvas Size", canvasSize);

        GUILayout.Space(20);

        GUILayout.Label("Enter Panel Attributes", EditorStyles.boldLabel);
        panelName = EditorGUILayout.TextField("Panel Name", panelName);
        panelPosition = EditorGUILayout.Vector2Field("Panel Position", panelPosition);
        panelSize = EditorGUILayout.Vector2Field("Panel Size", panelSize);
        panelBackgroundColor = EditorGUILayout.ColorField("Panel BG Color", panelBackgroundColor);

        GUILayout.Space(20);

        GUILayout.Label("Enter Button Attributes", EditorStyles.boldLabel);
        buttonName = EditorGUILayout.TextField("Button Name", buttonName);
        buttonPosition = EditorGUILayout.Vector2Field("Button Position", buttonPosition);
        buttonSize = EditorGUILayout.Vector2Field("Button Size", buttonSize);
        buttonText = EditorGUILayout.TextField("Button Text", buttonText);

        GUILayout.Space(20);

        GUILayout.Label("Enter Label Attributes", EditorStyles.boldLabel);
        labelName = EditorGUILayout.TextField("Label Name", labelName);
        labelPosition = EditorGUILayout.Vector2Field("Label Position", labelPosition);
        labelSize = EditorGUILayout.Vector2Field("Label Size", labelSize);
        labelText = EditorGUILayout.TextField("Label Text", labelText);

        if (GUILayout.Button("Create Template"))
        {
            newTemplate = new UITemplate
            {
                Canvas = new Canvas
                {
                    canvasName = canvasName,
                    canvasPosition = new Vector2(canvasPosition.x, canvasPosition.y),
                    canvasSize = new Vector2(canvasSize.x, canvasSize.y),
                    canvasChildren = new Panel
                    {
                        panelName = panelName,
                        panelPosition = new Vector2(panelPosition.x, panelPosition.y),
                        panelSize = new Vector2(panelSize.x, panelSize.y),
                        panelBackgroundColor = panelBackgroundColor,
                        panelChildren = new UIElement
                        {
                            Button = new Button
                            {
                                buttonName = buttonName,
                                buttonPosition = new Vector2(buttonPosition.x, buttonPosition.y),
                                buttonSize = new Vector2(buttonSize.x, buttonSize.y),
                                buttonText = buttonText
                            },
                            Label = new Label
                            {
                                labelName = labelName,
                                labelPosition = new Vector2(labelPosition.x, labelPosition.y),
                                labelSize = new Vector2(labelSize.x, labelSize.y),
                                labelText = labelText
                            }
                        }
                    }
                }
            };
        }

        GUILayout.Space(10);

        if (GUILayout.Button("Save Templates to JSON"))
        {
            templates.Add(newTemplate);
            newTemplate.SerializeToJson();


            newTemplateName = "";
            canvasName = "";
            canvasPosition = Vector2.zero;
            canvasSize = Vector2.zero;
            panelName = "";
            panelPosition = Vector2.zero;
            panelSize = Vector2.zero;
            panelBackgroundColor = Color.white;
            buttonName = "";
            buttonPosition = Vector2.zero;
            buttonSize = Vector2.zero;
            buttonText = "";
            labelName = "";
            labelPosition = Vector2.zero;
            labelSize = Vector2.zero;
            labelText = "";
        }
    }
}
