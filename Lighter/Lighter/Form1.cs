using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BitTorrentTool;
using System.Net.Sockets;
using System.Security.Cryptography;

namespace Lighter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "torrent文件|*.torrent";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            string filepath = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                filepath = openFileDialog1.FileName;
            StreamReader sr = new StreamReader(filepath);
            string s = sr.ReadToEnd();
            FileStream file = File.OpenRead(filepath);
            BinaryReader binaryReader = new BinaryReader(file);
            TorrentFileInfo torrentFileInfo = BencodeUtil.GetTorrentFileInfo(binaryReader);
            SHA1 hash = new SHA1CryptoServiceProvider();

        }
    }
}
