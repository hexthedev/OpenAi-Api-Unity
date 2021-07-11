using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;

namespace OpenAi.Examples
{
    public class ExampleOpenAiApiRuntime : MonoBehaviour
    {
        public InputField Input;
        public Text Output;

        public void DoApiCompletion()
        {
            string text = Input.text;

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogError("Example requires input in input field");
                return;
            }

            Debug.Log("Performing Completion in Play Mode");

            Output.text = "Perform Completion...";
            OpenAiCompleterV1.Instance.Complete(
                text,
                s => Output.text = s,
                e => Output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}