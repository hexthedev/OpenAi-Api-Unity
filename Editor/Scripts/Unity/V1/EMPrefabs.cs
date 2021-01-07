using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace OpenAi.Unity.V1
{
    public static class EMPrefabsV1
    {
        [MenuItem("OpenAi/V1/CreateGateway")]
        public static void CreateGateway() => SpawnPrefab("OpenAiApiGatewayV1");

        [MenuItem("OpenAi/V1/CreateCompleter")]
        public static void CreateCompleter() => SpawnPrefab("OpenAiCompleterV1");


        private static void SpawnPrefab(string name)
        {
            string[] assets = AssetDatabase.FindAssets(name);

            GameObject obj = null;

            foreach (string guid in assets)
            {
                string apath = AssetDatabase.GUIDToAssetPath(guid);
                obj = AssetDatabase.LoadAssetAtPath<GameObject>(apath);

                if (obj != null) break;
            }

            if (obj == null)
            {
                Debug.LogError($"Cannot find the prefab: {name}");
                return;
            }

            Object inst = PrefabUtility.InstantiatePrefab(obj);
            Selection.activeObject = inst;
            return;
        }
    }
}