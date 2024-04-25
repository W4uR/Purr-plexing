using UnityEngine;
using UnityEditor;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEditor.Localization;
using UnityEngine.Localization;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using System.Linq;

public class LocalizationEditor : EditorWindow
{
    private StringTableCollection textTable;
    private AssetTableCollection clipTable;

    string key = "Your Key";
    Dictionary<LocaleIdentifier, string> stringValues = new Dictionary<LocaleIdentifier, string>();
    Dictionary<LocaleIdentifier, AudioClip> assetValues = new Dictionary<LocaleIdentifier, AudioClip>();

    [MenuItem("Localization/Localization Editor")]
    public static void ShowWindow()
    {
        GetWindow<LocalizationEditor>("Localization Editor");
    }

    void OnGUI()
    {
        GUILayout.Label("Select String Table", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        textTable = EditorGUILayout.ObjectField("Text table:", textTable, typeof(StringTableCollection), false) as StringTableCollection;
        clipTable = EditorGUILayout.ObjectField("Clip table:", clipTable, typeof(AssetTableCollection), false) as AssetTableCollection;
        if (textTable == null || clipTable == null) return;
        EditorGUILayout.Space();
        key = EditorGUILayout.TextField("Key:", key);
        EditorGUILayout.Space();
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (!stringValues.ContainsKey(locale.Identifier))
            {
                stringValues[locale.Identifier] = "";
                assetValues[locale.Identifier] = null;
            }
            GUILayout.Label(locale.Identifier.Code.ToUpper() + ":", EditorStyles.boldLabel);
            stringValues[locale.Identifier] = EditorGUILayout.TextField("Text", stringValues[locale.Identifier]);
            assetValues[locale.Identifier] = EditorGUILayout.ObjectField("Clip", assetValues[locale.Identifier], typeof(AudioClip), false) as AudioClip;
            EditorGUILayout.Space();
        }


        if (GUILayout.Button("Add Entry"))
        {
            if (textTable.SharedData.Entries.Select(x => x.Key).Contains(key))
            {
                Debug.Log("Key already in collection.");
                return;
            }
            foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
            {
                var localeTextTable = textTable.GetTable(locale.Identifier) as StringTable;
                var localeClipTable = clipTable.GetTable(locale.Identifier) as AssetTable;

                if (localeTextTable != null && localeClipTable != null)
                {
                        localeTextTable.AddEntry(key, stringValues[locale.Identifier]);
                        localeClipTable.AddEntry(key, AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(assetValues[locale.Identifier])));

                    // Save the changes
                    EditorUtility.SetDirty(localeTextTable);
                    EditorUtility.SetDirty(localeClipTable);
                }
            }
            EditorUtility.SetDirty(textTable.SharedData);
            EditorUtility.SetDirty(clipTable.SharedData);
            EditorUtility.SetDirty(textTable);
            EditorUtility.SetDirty(clipTable);
            AssetDatabase.SaveAssets();
            Debug.Log("Loaclization entry with key: '" + key + "' has been added.");
            stringValues = new Dictionary<LocaleIdentifier, string>();
            assetValues = new Dictionary<LocaleIdentifier, AudioClip>();
        }
    }
}
