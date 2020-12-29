using OpenAi.Api;
using OpenAi.Api.V1;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

namespace OpenAi.Json
{
    public class JsonBuilder
    {
        private StringBuilder _sb = new StringBuilder();
        private bool _shouldAddComma = false;

        private string _prefix => _shouldAddComma ? "," : "";

        public JsonBuilder() { }

        public void StartObject() => _sb.Append("{");
        public void EndObject() => _sb.Append("}");
        public void StartList() => _sb.Append("[");
        public void EndList() => _sb.Append("]");

        public void Add(string name, int? val) => AddSimpleObject(name, val);
        public void Add(string name, float? val) => AddSimpleObject(name, val);
        public void Add(string name, bool? val)
        {
            if (val != null)
            {
                string valString = val == true ? "true" : "false";
                _sb.Append($"{_prefix}\"{name}\":{valString}");
                _shouldAddComma = true;
            }
        }

        public void AddSimpleObject(string name, object val)
        {
            if (val != null)
            {
                _sb.Append($"{_prefix}\"{name}\":{val}");
                _shouldAddComma = true;
            }
        }

        public void Add(string name, StringOrArray val)
        {
            if(val != null)
            {
                object valActual = val.Resolve();
                string valString = "";

                switch (valActual)
                {
                    case string s:
                        valString = $"\"{s}\"";
                        break;
                    case string[] a:
                        string[] arr = new string[a.Length];
                        for(int i = 0; i<a.Length; i++)
                        {
                            arr[i] = $"\"{a[i]}\"";
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

        public void AddList<T>(string name, T[] value) where T: IJsonable
        {
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

        public void AddList(string name, string[] value)
        {
            _sb.Append(_prefix);
            _sb.Append($"\"{name}\":");

            StartList();
            string[] strings = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
            {
                strings[i] = $"\"{value[i]}\"";
            }
            _sb.Append(string.Join(",", strings));
            EndList();

            _shouldAddComma = true;
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }
}