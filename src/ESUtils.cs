using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace EnderScript
{
    /// <summary>
    /// Helper class with useful related functions. 
    /// </summary>
    public static class ESUtils
    {
        public static T[] ToSpecificArray<T>(this object[] array)
        {
            T[] t = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
                t[i] = (T)array[i];

            return t;
        }
        
        public static ESBuilder CreateBuilderFromPath(string path)
        {
            return new ESBuilder(File.ReadAllText(path));
        }

        internal static void WriteToPath(string path,string es)
        {
            File.WriteAllText(path, es);
        }
        /// <summary>
        /// Prepares a raw string into an ES-compatable string.
        /// </summary>
        /// <param name="raw">Raw string to format</param>
        /// <returns>ES compatible string</returns>
        public static string GetFormattedString(string raw)
        {
            string s = "";
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == '"')
                    s += "\\\"";
                else if (raw[i] == '\\')
                    s += "\\\\";
                else
                    s += raw[i];
            }
            return s;
        }

        public static string[] ToArray(ESBuffer buffer)
        {
            string[] array = new string[buffer.esValues.Count];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = buffer.esValues[i].data;
            }
            return array;
        }
        public static string[] ToArray(ESBuilder builder)
        {
            ESBuffer buffer = new ESBuffer(builder);
            return ToArray(buffer);
        }
    }
}
