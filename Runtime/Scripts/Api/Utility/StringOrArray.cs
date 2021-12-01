using OpenAi.Json;

using System.Collections.Generic;

namespace OpenAi.Api.V1
{
    /// <summary>
    /// Can be interpreted as a string (if only 1 string added) or an array, if multiple strings added. This is to facilitate arguments in the OpenAi.Api
    /// </summary>
    public class StringOrArray
    {
        private List<string> _elements;

        /// <summary>
        /// Create a string or array using input strings. If 0 strings are provided, Resolve() = null. If
        /// 1 string is provided, Resolve() = a string, if 2+ strings are provided, Resolve() = string[]
        /// </summary>
        /// <param name="strings"></param>
        public StringOrArray(params string[] strings)
        {
            if (strings != null)
            {
                //Nullifies the empty strings in the array
                for (var i = 0; i < strings.Length; i++)
                {
                    strings[i] = string.IsNullOrEmpty(strings[i]) ? null : strings[i];
                }
                _elements = new List<string>(strings);
            }
        }


        /// <summary>
        /// Resolves the StringOrArray to the appropriate type
        /// </summary>
        /// <returns></returns>
        public object Resolve()
        {
            if (_elements == null || _elements.Count == 0) return null;
            if (_elements.Count == 1) return _elements[0];
            return _elements.ToArray();
        }
        
        /// <summary>
        /// Popualte based on json object
        /// </summary>
        public void FromJson(JsonObject json)
        {
            if(json.Type == EJsonType.List)
            {
                _elements = new List<string>();
                foreach(JsonObject obj in json.NestedValues)
                {
                    _elements.Add(obj.StringValue);
                }
            } 
            else if(json.Type == EJsonType.Value)
            {
                _elements = new List<string>();
                _elements.Add(json.StringValue);
            }
        }

        /// <summary>
        /// Implicitly make a StringOrArray from a string
        /// </summary>
        /// <param name="str"></param>
        public static implicit operator StringOrArray(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            return new StringOrArray(str);
        }

        /// <summary>
        /// Implicitly make a StringOrArray from a string array
        /// </summary>
        /// <param name="str"></param>
        public static implicit operator StringOrArray(string[] strings)
        {
            if (strings == null) return null;
            return new StringOrArray(strings);
        }
    }
}