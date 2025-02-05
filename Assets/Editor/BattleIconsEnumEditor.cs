#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattleIcons))]
public class BattleIconsEnumEditor : Editor
{
    private const string ENUM_NAME = "BattleIconEnum";
    private const string FILE_PATH = "Assets/Scripts/Common/Enum/";

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        BattleIcons battleIcons = (BattleIcons)target;

        if (GUILayout.Button("Generate Battle Icons Enum"))
        {
            GenerateBattleIconsEnum(battleIcons);
        }
    }

    private static void GenerateBattleIconsEnum(BattleIcons battleIcons)
    {
        if (battleIcons.iconNames == null || battleIcons.iconNames.Length == 0)
        {
            Debug.LogError("No icon names found in BattleIcons!");
            return;
        }

        if (!Directory.Exists(FILE_PATH))
        {
            Directory.CreateDirectory(FILE_PATH);
        }

        string filePath = FILE_PATH + ENUM_NAME + ".cs";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("// Auto-generated Battle Icons Enum");
            writer.WriteLine("public enum " + ENUM_NAME);
            writer.WriteLine("{");

            foreach (string iconName in battleIcons.iconNames)
            {
                string validEnumName = SanitizeEnumName(iconName);
                writer.WriteLine($"    {validEnumName},");
            }

            writer.WriteLine("}");
        }

        Debug.Log("BattleIconEnum successfully generated at: " + filePath);
        AssetDatabase.Refresh();
    }

    private static string SanitizeEnumName(string name)
    {
        return name.Replace(" ", "_").Replace("-", "_").Replace(".", "_");
    }
}
#endif
