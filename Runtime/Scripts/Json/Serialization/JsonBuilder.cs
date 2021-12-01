using OpenAi.Api.V1;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OpenAi.Json
{
    /// <summary>
    /// A bare-minimum Json string creation class geared towards creating JSON strings in the OpenAi Api. 
    /// </summary>
    public class JsonBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        private bool _shouldAddComma = false;

        private string _prefix => _shouldAddComma ? "," : "";

        /// <summary>
        /// Construct builder
        /// </summary>
        public JsonBuilder() { }

        /// <summary>
        /// Start object by adding {
        /// </summary>
        public void StartObject() => _sb.Append("{");

        /// <summary>
        /// Start object by adding }
        /// </summary>
        public void EndObject() => _sb.Append("}");

        /// <summary>
        /// Start list by adding [
        /// </summary>
        public void StartList() => _sb.Append("[");

        /// <summary>
        /// Start list by adding ]
        /// </summary>
        public void EndList() => _sb.Append("]");

        /// <summary>
        /// If not null, add int to json
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public void Add(string name, int? val) => AddSimpleObject(name, val);

        /// <summary>
        /// If not null, add float to json
        /// </summary>
        public void Add(string name, float? val)
        {
            if (val != null)
            {
                float value = (float)val;
                _sb.Append($"{_prefix}\"{name}\":{value.ToString(CultureInfo.InvariantCulture)}");
                _shouldAddComma = true;
            }
        }
        
        /// <summary>
        /// if not null, add bool to json
        /// </summary>
        public void Add(string name, bool? val)
        {
            if (val != null)
            {
                string valString = val == true ? "true" : "false";
                _sb.Append($"{_prefix}\"{name}\":{valString}");
                _shouldAddComma = true;
            }
        }

        /// <summary>
        /// if not null, add string to json
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public void Add(string name, string val)
        {
            if (val != null)
            {
                _sb.Append($"{_prefix}\"{name}\":{GetJsonString(val)}");
                _shouldAddComma = true;
            }
        }

        /// <summary>
        /// if not null, adds object to json by naively casting val to string
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public void AddSimpleObject(string name, object val)
        {
            if (val != null)
            {
                _sb.Append($"{_prefix}\"{name}\":{val}");
                _shouldAddComma = true;
            }
        }

        /// <summary>
        /// if not null, adds <see cref="StringOrArray"/> to json
        /// </summary>
        /// <param name="name"></param>
        /// <param name="val"></param>
        public void Add(string name, StringOrArray val)
        {
            if(val != null)
            {
                object valActual = val.Resolve();
                string valString = "";

                switch (valActual)
                {
                    case string s:
                        valString = GetJsonString(s);
                        break;
                    case string[] a:
                        //We send a list because we don't know how many non-null elements we have on the array
                        List<string> arr = new List<string>();
                        for(int i = 0; i<a.Length; i++)
                        {
                            if (a[i] != null)
                            {
                                arr.Add(GetJsonString(a[i]));
                            }
                        }
                        valString = $"[{string.Join(",", arr)}]";
                        break;
                    default:
                        throw new Exception("Failed to build StringOrArray");
                }

                _sb.Append($"{_prefix}\"{name}\":{valString}");
                _shouldAddComma = true;
            }
        }

        /// <summary>
        /// Adds a dictionary to json as a json object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dict"></param>
        public void Add(string name, Dictionary<string, int> dict)
        {
            if (dict == null) return;

            _sb.Append(_prefix);
            _sb.Append($"\"{name}\":");

            StartObject();
            bool isFirst = true;
            foreach(KeyValuePair<string, int> kv in dict)
            {
                if (!isFirst) _sb.Append(",");
                _sb.Append($"\"{kv.Key}\":{kv.Value}");
                if(isFirst) isFirst = false;
            }
            EndObject();
        }

        /// <summary>
        /// Adds adds an array of <see cref="IJsonable"/> objects as a json list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddArray<T>(string name, T[] value) where T: IJsonable
        {
            if (value == null) return;

            _sb.Append(_prefix);
            _sb.Append($"\"{name}\":");

            StartList();
            string[] strings = new string[value.Length];
            for(int i = 0; i<value.Length; i++)
            {
                strings[i] = value[i].ToJson();
            }
            _sb.Append(string.Join(",", strings));
            EndList();

            _shouldAddComma = true;
        }

        /// <summary>
        /// Adds an array of strings as a json list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddArray(string name, string[] value)
        {
            if (value == null) return;

            _sb.Append(_prefix);
            _sb.Append($"\"{name}\":");

            StartList();
            string[] strings = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                strings[i] = GetJsonString(value[i]);
            }
            _sb.Append(string.Join(",", strings));
            EndList();

            _shouldAddComma = true;
        }

        /// <summary>
        /// Adds an array to the json without applying a name. This is used for nested arrays
        /// </summary>
        public void AddArray(string[] values)
        {
            StartList();
            string[] strings = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                strings[i] = GetJsonString(values[i]);
            }
            _sb.Append(string.Join(",", strings));
            EndList();
        }

        /// <summary>
        /// Write JsonBuilder value as string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _sb.ToString();
        }

        private string GetJsonString(string s) => $"\"{ProcessString(s)}\"";

        private string ProcessString(string json)
        {
            StringBuilder sb = new StringBuilder();

            for(int i = 0; i<json.Length; i++)
            {
                sb.Append(ProcessJsonStringCharacter(json[i]));
            }

            return sb.ToString();
        }

        private string ProcessJsonStringCharacter(char character)
        {
            switch (character) 
            { 
                case '\a': return "\\a";
                case '\b': return "\\b";
                case '\t': return "\\t";
                case '\r': return "\\r";
                case '\v': return "\\v";
                case '\f': return "\\f";
                case '\n': return "\\n";
                case '\\': return "\\\\";
                case '\"': return "\\\"";
            }

            return $"{character}";
        }
    }
}