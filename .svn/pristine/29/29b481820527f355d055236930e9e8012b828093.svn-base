using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ChatSimulator
{
    public partial class SimulatorForm : Form
    {
        public delegate void writeDel(string s);

        private string fileRead = @"C:\Users\Michal\Desktop\ChatUn\5.txt";
        private readonly string fileWrite = @"C:\Users\Michal\Desktop\ChatUn\test.txt";

        private string[] chatText;
        private int index = 0;
        private int speed = 1;

        private System.Timers.Timer timer = new System.Timers.Timer(2000);

        public SimulatorForm()
        {
            chatText = File.ReadAllLines(fileRead, Encoding.UTF8);
            File.WriteAllText(fileWrite, "", Encoding.UTF8);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            InitializeComponent();

            readAdresTextBox.Text = fileRead;
            lineLabel.Text = index.ToString();
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            writeChat(chatText[index++]);
        }

        void writeChat(string s)
        {
            if (listView1.InvokeRequired)
            {
                writeDel del = new writeDel(writeChat);
                this.listView1.Invoke(del, new object[] { s });
            }
            else
            {
                listView1.Items.Add(s);
                lineLabel.Text = index.ToString();
                File.AppendAllLines(fileWrite, new string[] { s }, Encoding.UTF8);
            }
        }










        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.List;
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void cleanButton_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            fileRead = readAdresTextBox.Text;
            chatText = File.ReadAllLines(fileRead, Encoding.UTF8);
            index = 0;
            lineLabel.Text = index.ToString();
            listView1.Items.Clear();
            File.WriteAllText(fileWrite, "", Encoding.UTF8);
        }

        private void skipButton_Click(object sender, EventArgs e)
        {
            int.TryParse(this.skipTextBox.Text, out index);
            listView1.Items.Clear();
            lineLabel.Text = index.ToString();

            string[] lines = chatText.Take(index).ToArray();

            foreach (string line in lines)
                listView1.Items.Add(line);

            File.WriteAllLines(fileWrite, lines, Encoding.UTF8);
        }

        private void decreaseSpeedButton_Click(object sender, EventArgs e)
        {
            timer.Interval = timer.Interval * 2;
            speed = speed / 2;
            speedLabel.Text = speed.ToString();
        }

        private void increaseSpeedButton_Click(object sender, EventArgs e)
        {
            timer.Interval = timer.Interval / 2;
            speed = speed * 2;
            speedLabel.Text = speed.ToString();
        }

        private void stepButton_Click(object sender, EventArgs e)
        {

        }

        private void goButton_Click(object sender, EventArgs e)
        {

        }
    }
}
