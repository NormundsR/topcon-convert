using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Windows.Forms;

using TopconConvert.Model;
using TrimbleDC.Writer;

namespace TopconConvert.App
{
    public partial class Form1 : Form
    {
        TopconJob job;
        string convertedJob;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void FileOpen()
        {
            openFileDialog1.DefaultExt = ".xml";
            openFileDialog1.Filter = "XML files (.xml)|*.xml";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConvertJob(openFileDialog1.FileName);
            }
        }

        private void ConvertJob(string fileName)
        {
            XmlSerializer sr = new XmlSerializer(typeof(TopconJob));
            TextReader tr = new StreamReader(fileName);
            job = (TopconJob)sr.Deserialize(tr);

            int controlPoints = job._points.Count(p => p.control != TopconConvert.Model.Control.None);
            int stations = job._stations.Count();
            int observations = job._stations.Sum(s => s._obs.Count(o => o.obsType == ObservationType.Shotsight));
            toolStripStatusLabel1.Text = String.Format("Job: {0} Control points: {1} Stations: {2} Observations: {3}",
                job.Project.JobName,
                controlPoints,
                stations,
                observations);

            convertedJob = TrimbleDCWriter.ConvertTopconJob(job);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FileSave();
        }

        private void FileSave()
        {
            saveFileDialog1.DefaultExt = ".dc";
            saveFileDialog1.Filter = "Trimble DC file (*.dc)|*.dc";
            saveFileDialog1.FileName = job.Project.JobName;
            saveFileDialog1.InitialDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                StreamWriter sw1 = new StreamWriter(saveFileDialog1.FileName, false);
                sw1.Write(convertedJob);
                sw1.Close();
            }
        }

        private void Test1()
        {
            // Test1: load a sample XML file
            string fileName = @"Samples\Sample1.xml";
            XDocument doc1;

            doc1 = XDocument.Load(fileName);

            IEnumerable<XElement> p = from el in doc1.Elements("data").Elements("Point")
                                      select el;

            //label1.Text = string.Format("{0} items.", p.Count());

            foreach (XElement el in p)
            {
                //listBox1.Items.Add(el.Element("PointNumber").Value);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Count() > 1)
            {
                ConvertJob(args[1]);
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileOpen();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileSave();
        }
    }
}
