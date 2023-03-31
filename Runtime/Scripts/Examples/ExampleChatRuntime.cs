using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;

namespace OpenAi.Examples
{
    public class ExampleChatRuntime : MonoBehaviour
    {
        public Dropdown role;
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
            OpenAiChatCompleterV1.Instance.Complete(
                text,
                s => Output.text = s,
                e => Output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );
        }

        public void DoAddToDialogue()
        {
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), role.options[role.value].text);
            message.content = Input.text;
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void QuitApp()
        {
            Application.Quit();
        }
    }
}