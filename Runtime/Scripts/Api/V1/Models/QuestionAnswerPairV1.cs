using OpenAi.Json;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// <see cref="https://beta.openai.com/docs/api-reference/answers/create#answers/create-examples"/>
    /// </summary>
    public class QuestionAnswerPairV1 : AModelV1
    {
        /// <summary>
        /// The example that you are performing a search on
        /// </summary>
        public string question;

        /// <summary>
        /// The label that is used when trying to classify a prompt against an example
        /// </summary>
        public string answer;

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            foreach (JsonObject jo in json.NestedValues)
            {
                switch (jo.Name)
                {
                    case nameof(question):
                        question = jo.StringValue;
                        break;
                    case nameof(answer):
                        answer = jo.StringValue;
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(question), question);
            jb.Add(nameof(answer), answer);
            jb.EndObject();

            return jb.ToString();
        }
    }
}