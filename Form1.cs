using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Analyzer
{
    public partial class Form1 : Form
    {
        private string filename;
        string fileText;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            fileText = System.IO.File.ReadAllText(filename);
            textBox1.Text = fileText;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, textBox2.Text);
        }            
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            if (textBox1.Text != "")
            {
                float[] alph = new float[32];
                List<string> LongWords = new List<string>();
                int wordsCounter = 1;
                int count = 0;
                string word = "";
                foreach (char sym in textBox1.Text.ToLower())
                    if (sym <= 'я' && sym >= 'а')
                    {
                        alph[sym - 'а']++;
                        word += sym; 
                        count++;
                    }
                    else
                    {
                        if (sym == ' ' || sym == '\n'||sym=='.'||sym==','||sym==';'||sym=='-'||sym==':'||sym=='!'||sym=='?')
                            wordsCounter++;
                        if ((sym == ' ' || sym == '\n' || sym == '.' || sym == ',' || sym == ';' || sym == '-' || sym == ':' || sym == '!' || sym == '?') && word != "")
                        {
                            if (!LongWords.Contains(word))
                            {
                                if (LongWords.Count < 10)
                                    LongWords.Add(word);
                                else
                                    foreach (var w in LongWords)
                                    {
                                        if (w.Length < word.Length)
                                        {
                                            LongWords.Remove(w);
                                            LongWords.Add(word);
                                            break;
                                        }
                                    }
                            }
                        }
                        word = "";
                    }

                for (int i = 0; i < 32; i++)
                    alph[i] = (float)Math.Round(alph[i] / count * 100, 2);
                textBox2.AppendText("Процентное соотношение букв: \r\n");
                char elem = 'а';
                foreach (float sym in alph)
                    textBox2.AppendText(elem++ + " - " + sym + " %\r\n");
                textBox2.AppendText("Количество слов: " + wordsCounter+"\r\n");
                textBox2.Text += "10 самых длинных слов:\r\n";
                foreach (var w in LongWords)
                    textBox2.Text += w + "\r\n";
                
            }
            
        }
    }
}
