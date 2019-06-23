using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BitTorrentTool
{
    public class TorrentFileInfo
    {
        public string announce;
        public List<string> announce_list;
        public DateTime creation_date;
        public string created_by;
        public string encoding;
        public string conmment;
        public string publisher;
        public string publisher_url;
        public TorrentFileInfoDetailinfo info;
    }
    public class TorrentFileInfoDetailinfo
    {
        public long piece_length;
        public byte[] pieces;
        public long length;
        public List<FileInfo> files;
        public string name;
        public string path;
        public string md5sum;
    }

    public class FileInfo
    {
        public long length;
        public string path;
    }
}
