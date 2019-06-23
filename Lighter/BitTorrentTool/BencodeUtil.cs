using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BitTorrentTool
{
    public class BencodeUtil
    {
        public static long Bint(BinaryReader binaryReader)
        {
            binaryReader.ReadChar();
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

        public static byte[] Bbyte(BinaryReader binaryReader)
        {
            string number = "";
            char ch;
            while ((ch = binaryReader.ReadChar()) != ':')
            {
                number += ch;
            }
            byte[] byteStr = binaryReader.ReadBytes(int.Parse(number));
            return byteStr;
        }

        public static Dictionary<object, object> Bdictionary(BinaryReader binaryReader, TorrentFileInfo torrentFileInfo)
        {
            Dictionary<object, object> dic = new Dictionary<object, object>();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                object Value;
                switch (ch)
                {
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
                switch (Value)
                {
                    case "announce": torrentFileInfo.announce = Bstring(binaryReader); break;
                    case "announce-list":
                        torrentFileInfo.announce_list = new List<string>();
                        torrentFileInfo.announce_list = Blist(binaryReader,torrentFileInfo.announce_list); break;
                    case "creation date": torrentFileInfo.creation_date = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)).AddSeconds(Bint(binaryReader)); break;
                    case "created by": torrentFileInfo.created_by = Bstring(binaryReader); break;
                    case "encoding": torrentFileInfo.encoding = Bstring(binaryReader); break;
                    case "conmment": torrentFileInfo.conmment = Bstring(binaryReader); break;
                    case "info":
                        torrentFileInfo.info = new TorrentFileInfoDetailinfo();
                        torrentFileInfo.info = Bdictionary(binaryReader,torrentFileInfo.info); break;
                    case "publisher": torrentFileInfo.publisher = Bstring(binaryReader); break;
                    case "publisher url": torrentFileInfo.publisher_url = Bstring(binaryReader); break;
                }
            }
            binaryReader.ReadChar();
            return dic;
        }

        public static TorrentFileInfoDetailinfo Bdictionary(BinaryReader binaryReader, TorrentFileInfoDetailinfo torrentFileInfoDetailinfo)
        {
            binaryReader.ReadChar();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                object Value;
                switch (ch)
                {
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
                switch (Value)
                {
                    case "piece length": torrentFileInfoDetailinfo.piece_length = Bint(binaryReader); break;
                    case "pieces": torrentFileInfoDetailinfo.pieces = Bbyte(binaryReader); break;
                    case "length": torrentFileInfoDetailinfo.length = Bint(binaryReader); break;
                    case "files":
                        torrentFileInfoDetailinfo.files = new List<FileInfo>();
                        torrentFileInfoDetailinfo.files = Blist(binaryReader,torrentFileInfoDetailinfo.files); break;
                    case "name": torrentFileInfoDetailinfo.name = Bstring(binaryReader); break;
                    case "path": torrentFileInfoDetailinfo.path = Bstring(binaryReader); break;
                    case "md5sum": torrentFileInfoDetailinfo.md5sum = Bstring(binaryReader); break;
                }
            }
            binaryReader.ReadChar();
            return torrentFileInfoDetailinfo;
        }

        public static FileInfo Bdictionary(BinaryReader binaryReader)
        {
            FileInfo fileInfo = new FileInfo();
            binaryReader.ReadChar();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                object Value;
                switch (ch)
                {
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
                switch (Value)
                {
                    case "length": fileInfo.length = Bint(binaryReader); break;
                    case "path": fileInfo.path = Blist(binaryReader); break;
                }
            }
            binaryReader.ReadChar();
            return fileInfo;
        }

        public static List<string> Blist(BinaryReader binaryReader, List<string> announce_list)
        {
            binaryReader.ReadChar();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                string Value;
                switch (ch)
                {
                    case 'l':
                        // List
                        binaryReader.ReadChar();
                        Value = Bstring(binaryReader);
                        binaryReader.ReadChar(); break;
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
                announce_list.Add(Value);
            }
            binaryReader.ReadChar();
            return announce_list;

        }

        public static List<FileInfo> Blist(BinaryReader binaryReader, List<FileInfo> files)
        {
            binaryReader.ReadChar();
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                object Value;
                switch (ch)
                {
                    case 'd':
                        // Dictionary
                        files.Add(Bdictionary(binaryReader)); break;
                    case 'l':
                        // Dictionary
                        binaryReader.ReadChar();
                        files.Add(Bdictionary(binaryReader)); break;
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
            }
            binaryReader.ReadChar();
            return files;
        }

        public static string Blist(BinaryReader binaryReader)
        {
            binaryReader.ReadChar();
            string path = "";
            char ch;
            while ((ch = (char)binaryReader.PeekChar()) != 'e')
            {
                string Value;
                switch (ch)
                {
                    case 'l':
                        // List
                        binaryReader.ReadChar();
                        Value = Bstring(binaryReader); break;
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
                path = Value + "/";
            }
            binaryReader.ReadChar();
            return path.TrimEnd('/');

        }

        public static TorrentFileInfo GetTorrentFileInfo(BinaryReader binaryReader)
        {
            TorrentFileInfo torrentFileInfo = new TorrentFileInfo();
            char next = (char)binaryReader.PeekChar();
            object fileInfo;
            switch (next)
            {
                case 'i':
                    // Integer
                    binaryReader.ReadChar();
                    fileInfo = Bint(binaryReader); break;
                case 'l':
                    // List
                    binaryReader.ReadChar();
                    fileInfo = Blist(binaryReader); break;
                case 'd':
                    // Dictionary
                    binaryReader.ReadChar();
                    fileInfo = Bdictionary(binaryReader, torrentFileInfo); break;
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
                    fileInfo = Bstring(binaryReader); break;
                default:
                    fileInfo = null; break;
            }
            return torrentFileInfo;
        }
    }
}
