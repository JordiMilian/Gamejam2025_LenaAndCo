using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "GamePreset", menuName = "ScriptableObjects/GamePreset", order = 1)]
public class GamePresetObject : ScriptableObject
{
    public string gamePresetName = "";

    public List<CardColumn> level;

}

[Serializable]
public class CardColumn
{
    public List<CardObject> position;
}
