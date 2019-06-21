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
            char next = (char)binaryReader.PeekChar();
            object fileInfo;
            switch (next)
            {
                case 'i':
                    // Integer
                    binaryReader.ReadChar();
                    fileInfo = BencodeUtil.Bint(binaryReader); break;
                case 'l':
                    // List
                    binaryReader.ReadChar();
                    fileInfo = BencodeUtil.Blist(binaryReader); break;
                case 'd':
                    // Dictionary
                    binaryReader.ReadChar();
                    fileInfo = BencodeUtil.Bdictionary(binaryReader); break;
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
                    fileInfo = BencodeUtil.Bstring(binaryReader); break;
                default:
                    fileInfo = null; break;
            }
        }
    }
}
