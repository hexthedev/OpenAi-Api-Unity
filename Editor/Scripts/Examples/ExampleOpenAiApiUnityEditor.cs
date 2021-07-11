// MyEditor.cs
using OpenAi.Api.V1;
using OpenAi.Unity.V1;

using System.Threading.Tasks;

using UnityEditor;

using UnityEngine;

namespace OpenAi.Examples
{
    public class ExampleOpenAiApiUnityEditor : EditorWindow
    {
        private string _input = "Enter Prompt here";

        private string _output;

        [MenuItem("OpenAi/Examples/Completion In Editor Window")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ExampleOpenAiApiUnityEditor));
        }

        void OnGUI()
        {
            GUI.enabled = false;
            EditorGUILayout.ObjectField("Code:", MonoScript.FromScriptableObject(this), typeof(ScriptableObject), false);
            GUI.enabled = true;

            SOAuthArgsV1 auth = ScriptableObject.CreateInstance<SOAuthArgsV1>();
            OpenAiApiV1 api = new OpenAiApiV1(auth.ResolveAuth());

            _input = EditorGUILayout.TextField(_input); 

            if (api != null && GUILayout.Button("Do Completion"))
            {
                Debug.Log("Performing Completion in Editor Time");
                DoEditorTask(api);
            }

            if (!string.IsNullOrEmpty(_output))
            {
                GUI.enabled = false;
                EditorGUILayout.TextField(_output);
                GUI.enabled = true;
            }
        }

        private async Task DoEditorTask(OpenAiApiV1 api)
        {
            _output = "Performing completion...";

            ApiResult<CompletionV1> comp = await api.Engines.Engine("davinci").Completions.CreateCompletionAsync(
                    new CompletionRequestV1()
                    {
                        prompt = "test",
                        max_tokens = 8
                    }
                );

            if (comp.IsSuccess)
            {
                _output = $"{comp.Result.choices[0].text}";
            }
            else
            {
                _output = $"ERROR: StatusCode={comp.HttpResponse.responseCode} - {comp.HttpResponse.error}";
            }
        }
    }
}