using System.Text;

namespace OpenAiApi
{
    public class CompletionRequestV1 : AModelV1
    {
        public StringOrArray prompt;
        public int? max_tokens;
        public float? temperature;
        public float? top_p;
        public int? n;
        public bool? stream;
        public int? logprobs;
        public bool? echo;
        public StringOrArray stop;
        public float? presences_penalty;
        public float? frequence_penalty;
        public int? best_of;

        /// <inheritdoc />
        public override void FromJson(JsonObject json)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public override string ToJson()
        {
            JsonBuilder jb = new JsonBuilder();

            jb.StartObject();
            jb.Add(nameof(prompt), prompt);
            jb.Add(nameof(max_tokens), max_tokens);
            jb.Add(nameof(temperature), temperature);
            jb.Add(nameof(top_p), top_p);
            jb.Add(nameof(n), n);
            jb.Add(nameof(stream), stream);
            jb.Add(nameof(logprobs), logprobs);
            jb.Add(nameof(echo), echo);
            jb.Add(nameof(stop), stop);
            jb.Add(nameof(presences_penalty), presences_penalty);
            jb.Add(nameof(frequence_penalty), frequence_penalty);
            jb.Add(nameof(best_of), best_of);
            jb.EndObject();

            return jb.ToString();
        }
    }
}
