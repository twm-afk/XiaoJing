using UnityEngine;
using UnityEditor;

public class FindMissingScripts
{
    [MenuItem("Tools/检查场景Missing脚本")]
    public static void FindMissing()
    {
        GameObject[] objects = Object.FindObjectsByType<GameObject>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        int count = 0;

        foreach (GameObject obj in objects)
        {
            Component[] components = obj.GetComponents<Component>();

            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning(
                        "发现 Missing Script：" + GetPath(obj),
                        obj
                    );

                    count++;
                }
            }
        }

        Debug.Log(
            "检查完成，共发现 " + count + " 个 Missing Script。"
        );
    }

    private static string GetPath(GameObject obj)
    {
        string path = obj.name;

        Transform current = obj.transform.parent;

        while (current != null)
        {
            path = current.name + "/" + path;
            current = current.parent;
        }

        return path;
    }
}