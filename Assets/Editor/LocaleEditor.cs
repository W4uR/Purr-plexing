using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;

public class LocalizationEditor : EditorWindow
{
    string tableName = "Narration";
    string key = "Your Key";
    Dictionary<LocaleIdentifier, string> stringValues = new Dictionary<LocaleIdentifier, string>();
    Dictionary<LocaleIdentifier, AudioClip> assetValues = new Dictionary<LocaleIdentifier, AudioClip>();

    [MenuItem("Window/Localization Editor")]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditor>("Localization Editor");
    }

    void OnGUI()
    {
        string buttonText = "Add Entry";

        GUILayout.Label("Add new localization entry", EditorStyles.whiteLargeLabel);
        EditorGUILayout.Space();
        tableName = EditorGUILayout.TextField("Table name:", tableName);

        var stringTableCollection = LocalizationEditorSettings.GetStringTableCollection(tableName);
        var assetTableCollection = LocalizationEditorSettings.GetAssetTableCollection(tableName);

        if (!stringTableCollection || !assetTableCollection)
        {
            EditorGUILayout.HelpBox("Tables not found.", MessageType.Error);
            return;
        }
        EditorGUILayout.Space();
        key = EditorGUILayout.TextField("Key:", key);
        if (stringTableCollection.SharedData.Contains(key))
        {
            EditorGUILayout.HelpBox("The key is already used in the table.", MessageType.Warning);
            buttonText = "Update Entry";
        }

        if (string.IsNullOrWhiteSpace(key)) return;

        EditorGUILayout.Space();
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (!stringValues.ContainsKey(locale.Identifier))
            {
                stringValues[locale.Identifier] = "";
                assetValues[locale.Identifier] = null;
            }
            GUILayout.Label(locale.Identifier.Code.ToUpper() + ":",EditorStyles.boldLabel);
            stringValues[locale.Identifier] = EditorGUILayout.TextField("Text", stringValues[locale.Identifier]);
            assetValues[locale.Identifier] = EditorGUILayout.ObjectField("Clip", assetValues[locale.Identifier], typeof(AudioClip), false) as AudioClip;
            EditorGUILayout.Space();
        }

        EditorGUI.BeginDisabledGroup(!AreFieldsFilled(stringValues, assetValues));
        if (GUILayout.Button(buttonText))
        {
            if (stringTableCollection != null && assetTableCollection != null)
            {
                // Add the entries to the tables for each locale
                foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
                {
                    var stringTable = stringTableCollection.GetTable(locale.Identifier) as StringTable;
                    var assetTable = assetTableCollection.GetTable(locale.Identifier) as AssetTable;
                    if (stringTable != null && assetTable != null)
                    {
                        stringTable.AddEntry(key, stringValues[locale.Identifier]);
                        assetTable.AddEntry(key, AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(assetValues[locale.Identifier])));
                        
                        // Save the changes
                        EditorUtility.SetDirty(stringTable);
                        EditorUtility.SetDirty(assetTable);
                    }
                }
                AssetDatabase.SaveAssets();
                Debug.Log("Loaclization entry with key: '" + key + "' has been added.");
                stringValues = new Dictionary<LocaleIdentifier, string>();
                assetValues = new Dictionary<LocaleIdentifier, AudioClip>();
            }
            else
            {
                Debug.LogError("Could not find the tables. Please make sure the table names are correct.");
            }
        }
        EditorGUI.EndDisabledGroup();
        GUILayout.Label("Keys:", EditorStyles.whiteLargeLabel);
        foreach (var entry in stringTableCollection.SharedData.Entries)
        {
            GUILayout.Label(entry.Key, EditorStyles.whiteMiniLabel);
        }
    }

    static bool AreFieldsFilled(Dictionary<LocaleIdentifier, string> stringValues, Dictionary<LocaleIdentifier, AudioClip> assetValues)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (!stringValues.ContainsKey(locale.Identifier) || !assetValues.ContainsKey(locale.Identifier))
                return false;
            if (string.IsNullOrWhiteSpace(stringValues[locale.Identifier]) || assetValues[locale.Identifier] == null)
                return false;
        }
        return true;
    }
}
