using UnityEditor;

namespace OpenAi.Api.V1
{
    [CustomEditor(typeof(OpenAiApiAuthArgs))]
    public class OpenAiApiAuthArgsEditor : Editor
    {
        SerializedProperty AuthType;
        SerializedProperty PrivateApiKey;

        void OnEnable()
        {
            AuthType = serializedObject.FindProperty("AuthType");
            PrivateApiKey = serializedObject.FindProperty("PrivateApiKey");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(AuthType);

            switch((EAuthType)AuthType.enumValueIndex)
            {
                case EAuthType.LocalFile:
                    EditorGUILayout.HelpBox("This auth method will attempt to find the private key at `~/.openai/key.txt` (Linux/Mac) or `%USERPROFILE%/.openai/key.txt` (Windows). If this file does not exist or the key is not present, api calls will fail", MessageType.Warning);
                    break;
                case EAuthType.String:
                    EditorGUILayout.PropertyField(PrivateApiKey);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}