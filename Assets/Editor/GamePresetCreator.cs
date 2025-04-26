using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class GamePresetCreator : EditorWindow
{
    private int gameRows = 1;
    private List<CardObject> gameCards = new List<CardObject>();

    [MenuItem("Tools/Game Preset Creator")]
    public static void ShowWindow()
    {
        GetWindow<GamePresetCreator>("GamePreset Creator");
    }
}
