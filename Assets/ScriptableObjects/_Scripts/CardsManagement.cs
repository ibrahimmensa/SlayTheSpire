using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "mYCardsManager", menuName = "MSO/Card/NewCardsManager")]
public class CardsManagement : ScriptableObject
{
    public Cards CardData;

    [Header("Relics")]
    public Relic[] aquiredRelic;


    [Header("Portions")]
    public Relic[] aquiredPortion;

    [Header("Curse")]
    public int Cursevalue;
    public bool CurseActivated;

    [Header("Defence")]
    public int defanceValue;
    public bool DefanceActivated;

    [Header("Relic and portions")]
    public Relic[] relicName;
    public Portions[] portionName;

    [Header("others")]
    public int turns;
    public bool twoMouseCardsUsed;
}
[System.Serializable]
public class Relic
{
    public string name;
    public string discription;
    public bool common, rare, unCommon, legendary, epic;
}
[System.Serializable]
public class Portions
{
    public string name;
    public string discription;
    public bool common, rare, unCommon, legendary, epic;
}

//[CustomEditor(typeof(Relic))]
//public class ScriptMainEditor : Editor
//{
//    SerializedProperty myFirstBool;
//    SerializedProperty myFirstBool1;
//    SerializedProperty myFirstBool2;
//    SerializedProperty myFirstBool3;
//    SerializedProperty myFirstBool4;

//    private void OnEnable()
//    {
//        // hook up the serialized properties
//        myFirstBool = serializedObject.FindProperty(nameof(Relic.common));
//        myFirstBool1 = serializedObject.FindProperty(nameof(Relic.unCommon));
//        myFirstBool2 = serializedObject.FindProperty(nameof(Relic.rare));
//        myFirstBool3 = serializedObject.FindProperty(nameof(Relic.legendary));
//        myFirstBool4 = serializedObject.FindProperty(nameof(Relic.epic));
//    }

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector(); // for other non-HideInInspector fields

//        // update the current values into the serialized object and propreties
//        serializedObject.Update();

//        // if the first bool is true
//        if(!myFirstBool.boolValue) 
//        {
//            EditorGUILayout.PropertyField(myFirstBool1);
//        }

//        serializedObject.ApplyModifiedProperties();
//    }
//}
