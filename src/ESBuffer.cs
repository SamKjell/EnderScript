using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnderScript
{
    /// <summary>
    /// Object which can be used to write data to the ES format.
    /// </summary>
    public class ESBuffer
    {
        public bool suppressLastLineEnding = true;
        public List<ESObject> esValues = new List<ESObject>();

        public ESBuffer(){}
        public ESBuffer(ESBuilder builder)
        {
            esValues = builder.esValues;
        }

        public void Add(string parameterName, string data, string lineEnding = "\n", bool autoFormat = true)
        {
            ESObject esObject = new ESObject(parameterName, "\"" + (autoFormat?ESUtils.GetFormattedString(data):data) + "\"", lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, int data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, float data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        public void Add(string parameterName, object[] data)
        {
            
            ESObject esObject = new ESObject(parameterName, ConjoinArray(data));
            Add(esObject);
        }
        public void Add(string parameterName, bool data, string lineEnding = "\n")
        {
            ESObject esObject = new ESObject(parameterName, data.ToString(), lineEnding);
            Add(esObject);
        }
        private void Add(ESObject obj)
        {
            ESObject old = esValues.Find(x => x.parameterName == obj.parameterName);
            if (old != null)
                esValues[esValues.IndexOf(old)] = obj;
            else
                esValues.Add(obj);
        }
        
        public void AddComment(string comment)
        {
            ESComment esObject = new ESComment(comment);
            Add(esObject);
        }

        public void Remove(string parameterName)
        {
            esValues.RemoveAll(x => x.parameterName == parameterName);
        }

        private string ConjoinArray(object[] data)
        {
            List<string> d = new List<string>();
            foreach (object obj in data)
            {
                if (obj is string)
                    d.Add("\"" + obj + "\"");
                else if (obj is IEnumerable<object>)
                    d.Add(ConjoinArray(obj as object[]));
                else
                    d.Add(obj.ToString());
            }
            return $"[{string.Join(",", d)}]";
        }

        /// <summary>
        /// Function used return the buffer as a string.
        /// </summary>
        /// <returns>String containing data formatted in ES form</returns>
        public string GetES()
        {
            string es = "";
            for (int i = 0; i < esValues.Count; i++) {
                bool flag = i + 1 != esValues.Count;
                bool comment = esValues[i] is ESComment;

                es += esValues[i].GetES() + (!comment&&flag? "," : "") + (!flag&&suppressLastLineEnding?"":esValues[i].lineEnding);
            }
            return es;
        }

        /// <summary>
        /// Utility function which creates dumps the ES data, in text form, to the specified path.
        /// </summary>
        /// <param name="path">File to write to; conventionally a .es file.</param>
        public void WriteToPath(string path)
        {
            ESUtils.WriteToPath(path, GetES());
        }
    }

    public class ESParseException : Exception
    {
        public ESParseException()
        {

        }
        public ESParseException(string message) : base(message)
        {

        }
    }
}
