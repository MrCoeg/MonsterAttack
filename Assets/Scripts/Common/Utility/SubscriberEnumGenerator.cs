using System.IO;
using UnityEditor;
using UnityEngine;

public static class SubscriberEnumGenerator
{
#if UNITY_EDITOR
    private const string FilePath = "Assets/Scripts/Common/Enum/GeneratedSubscribers.cs";

    [MenuItem("Tools/Generate Subscribers Enum")]
    public static void GenerateEnum()
    {
        var manager = SubscriberManager.Instance;
        if (manager == null)
        {
            Debug.LogError("No SubscriberRegistry found in the scene!");
            return;
        }

        SubscriberManager.ScanAllSubscribersInScene();

        using StreamWriter writer = new(FilePath);
        writer.WriteLine("public enum SubscriberEnum");
        writer.WriteLine("{");

        foreach (var obj in manager.subscriberObjects)
        {
            if (obj != null)
            {
                writer.WriteLine($"    {obj.name.Replace(" ", "_")},");
            }
        }

        writer.WriteLine("}");
        Debug.Log("Generated Subscribers Enum!");

        AssetDatabase.Refresh();
    }
#endif
}
