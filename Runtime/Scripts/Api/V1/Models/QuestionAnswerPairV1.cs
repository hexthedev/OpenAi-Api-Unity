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

        public QuestionAnswerPairV1(string question, string answer)
        {
            this.question = question == null ? "" : question;
            this.answer = answer == null ? "" : answer;
        }

        public QuestionAnswerPairV1() { }

        /// <inheritdoc/>
        public override void FromJson(JsonObject json)
        {
            if (json.NestedValues.Count != 2)
                throw new OpenAiJsonException($"Received badly formated {nameof(QuestionAnswerPairV1)} array");

            question = json.NestedValues[0].StringValue;
            answer = json.NestedValues[1].StringValue;
        }

        /// <inheritdoc/>
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();
            jb.AddArray(new string[] { question, answer });
            return jb.ToString();
        }
    }
}