using OpenAi.Unity.V1;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace OpenAi.Examples
{
    [ExecuteInEditMode]
    public class ExampleImagesGenerations : MonoBehaviour
    {
        public string prompt = "";
        string openAIstatus;
        List<Texture2D> images = new List<Texture2D>();

        // example texture loading
        IEnumerator LoadImageToInstanceTexture(string fromUrl)
        {
            // Debug.LogFormat("Loading image from: {0}", fromUrl);

            using (var uwr = UnityWebRequestTexture.GetTexture(fromUrl))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    var error = string.Format("Image loading from {0} failed: {1}, {2}", fromUrl, uwr.result, uwr.error);

                    Debug.LogError(error);
                    this.openAIstatus = error;
                }
                else
                {
                    // Get downloaded texture
                    var tex = DownloadHandlerTexture.GetContent(uwr);
                    this.images.Add(tex);
                }
            }
        }

        void OnGUI()
        {

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Prompt: ", GUILayout.MaxWidth(Screen.width / 4));
                this.prompt = GUILayout.TextArea(this.prompt, 1000, GUILayout.MaxWidth(Screen.width / 4 * 3));
            }
            GUILayout.Label(this.openAIstatus);

            if (!string.IsNullOrEmpty(this.prompt))
            {
                if (GUILayout.Button("Generate image/s"))
                {
                    this.images.Clear();

                    OpenAiImagesGenerationsV1.Instance.Generate(
                        this.prompt
                        , r =>
                        {
                            var length = r.Length;
                            for (var i = 0; i < length; ++i)
                                this.StartCoroutine(this.LoadImageToInstanceTexture(r[i]));
                        }
                        , e => this.openAIstatus = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
                        );
                    ;
                }
            }

            if (this.images != null)
            {
                using (new GUILayout.HorizontalScope())
                {
                    foreach (var image in this.images)
                    {
                        if (image != null)
                            GUILayout.Label(image as Texture2D);
                    }
                }
            }
        }
    }
}