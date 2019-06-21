using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lighter
{
    public class BencodeUtil
    {
        public static long Bint(BinaryReader binaryReader)
        {
            string number = "";
            char ch;
            while ((ch = binaryReader.ReadChar()) != 'e')
            {
                number += ch;
            }
            return long.Parse(number);
        }

        public static string Bstring(BinaryReader binaryReader)
        {
            string number = "";
            char ch;
            while ((ch = binaryReader.ReadChar()) != ':')
            {
                number += ch;
            }
            byte[] byteStr = binaryReader.ReadBytes(int.Parse(number));
            return Encoding.UTF8.GetString(byteStr);
        }

        public static Dictionary<object, object> Bdictionary(BinaryReader binaryReader)
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            char ch;
            int i = 0;
            object Key = null;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                i++;
                object Value;
                switch (ch)
                {
                    case 'i':
                        // Integer
                        binaryReader.ReadChar();
                        Value = Bint(binaryReader); break;
                    case 'l':
                        // List
                        binaryReader.ReadChar();
                        Value = Blist(binaryReader); break;
                    case 'd':
                        // Dictionary
                        binaryReader.ReadChar();
                        Value = Bdictionary(binaryReader); break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        Value = Bstring(binaryReader); break;
                    default:
                        Value = null; break;
                }
                if (i % 2 == 0)
                {
                    dic.Add(Key, Value);
                }
                else
                {
                    Key = Value;
                }
            }
            binaryReader.ReadChar();
            return dic;
        }

        public static List<object> Blist(BinaryReader binaryReader)
        {
            List<object> list = new List<object>();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                object Value;
                switch (ch)
                {
                    case 'i':
                        // Integer
                        binaryReader.ReadChar();
                        Value = Bint(binaryReader); break;
                    case 'l':
                        // List
                        binaryReader.ReadChar();
                        Value = Blist(binaryReader); break;
                    case 'd':
                        // Dictionary
                        binaryReader.ReadChar();
                        Value = Bdictionary(binaryReader); break;
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        Value = Bstring(binaryReader); break;
                    default:
                        Value = null; break;
                }
                list.Add(Value);
            }
            binaryReader.ReadChar();
            return list;
            
        }
    }
}
