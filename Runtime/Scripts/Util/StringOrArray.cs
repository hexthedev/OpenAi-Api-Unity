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
            if(strings != null)
            {
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
        /// Implicitly make a StringOrArray from a string
        /// </summary>
        /// <param name="str"></param>
        public static implicit operator StringOrArray(string str) => new StringOrArray(str);

        /// <summary>
        /// Implicitly make a StringOrArray from a string array
        /// </summary>
        /// <param name="str"></param>
        public static implicit operator StringOrArray(string[] strings) => new StringOrArray(strings);
    }
}