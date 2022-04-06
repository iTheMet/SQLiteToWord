using SQLiteToWord.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteToWord
{
    partial class Form1
    {
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            VoroninsEnabler();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                label1.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                label2.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
                this.BackgroundImage = null;
                listView1.BackgroundImage = null;
                listView2.BackgroundImage = null;
                button3.BackgroundImage = null;
                button3.ForeColor = Color.Black;
                button3.Font = new Font("Microsoft Sans Serif", 26, FontStyle.Bold);
            }
            else 
            {
                VoroninsEnabler();
            }
        }

        public void VoroninsEnabler()
        {
            label1.ForeColor = Color.Red;
            label2.ForeColor = Color.Red;
            label1.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
            label2.Font = new Font("Comic Sans MS", 12, FontStyle.Bold);
            this.BackgroundImage = new Bitmap($@"{Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString())}/Sources/BCimage.jpg");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            listView1.BackgroundImage = new Bitmap($@"{Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString())}/Sources/LVimage1.jpg");
            listView1.BackgroundImageTiled = true;
            listView2.BackgroundImage = new Bitmap($@"{Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString())}/Sources/LVimage2.jpeg");
            listView2.BackgroundImageTiled = true;
            button3.BackgroundImage = new Bitmap($@"{Directory.GetParent(Directory.GetParent(Environment.CurrentDirectory).ToString())}/Sources/Bimage.jpg");
            button3.ForeColor = Color.Lime;
            button3.Font = new Font("Comic Sans MS", 26, FontStyle.Bold);

        }
    }
}
