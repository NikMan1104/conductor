using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    public partial class Form1 : Form
    {
        DirectoryInfo dir;
        public Form1()
        {
            InitializeComponent();
            AddDisk();
            
        }
        public void AddDisk()
        {
            var drive = DriveInfo.GetDrives();
            foreach (var disk in drive)
            {
                TreeNode node = new TreeNode();
                node.Tag = disk;
                node.Text = disk.ToString();
                node.Nodes.Add("");
                treeView.Nodes.Add(node);
            }
        }
        public FileInfo[] ReturnFileInfo(DirectoryInfo dir) => dir.GetFiles();
        public void AddDiectory(DirectoryInfo dir, TreeNode SelectNode)
        {

            var getdir = dir.GetDirectories();
            foreach (var d in getdir)
            {
                TreeNode node = new TreeNode();
                node.Tag = d;
                node.Text = d.ToString();
                node.Nodes.Add("");
                SelectNode.Nodes.Add(node);
            }

        }
        public void AddFile(TreeNode SelectNode)
        {
            foreach (var d in ReturnFileInfo(dir))
            {
                TreeNode node = new TreeNode();
                node.Tag = d;
                node.Text = d.ToString();
                node.Nodes.Add("");
                SelectNode.Nodes.Add(node);
            }
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
         
            try
            {
                TreeNode SelectNode = e.Node;
                SelectNode.Nodes.Clear();

                if (SelectNode.Tag is DriveInfo)
                {
                    DriveInfo drive = (DriveInfo)SelectNode.Tag;
                    dir = drive.RootDirectory;
                }
                else
                {
                    dir = (DirectoryInfo)SelectNode.Tag;
                }
                AddDiectory(dir, SelectNode);
                AddFile(SelectNode);
            }
            catch { }
        }

        private void treeView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            
            try
            {
                var selected = treeView.SelectedNode.Text;
                richTextBoxFileItem.Text = null;
                foreach (var el in ReturnFileInfo(dir))
                {
                    if (el.ToString() == selected)
                    {
                        using (StreamReader reader = el.OpenText())
                        {
                            richTextBoxFileItem.Text = reader.ReadToEnd();
                        }
                    }

                }
            }
            catch { };

        }

        private void свернутьToolStripMenuItem_Click(object sender, EventArgs e)=>WindowState = FormWindowState.Minimized;

        private void развернутьToolStripMenuItem_Click(object sender, EventArgs e)=>WindowState = FormWindowState.Maximized;

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)=>Close();

        

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var selected = treeView.SelectedNode.Text;
                richTextBoxFileItem.Text = null;
                foreach (var el in ReturnFileInfo(dir))
                {
                    if (el.ToString() == selected)
                    {
                        using (StreamWriter writer = el.CreateText())
                        {
                            writer.WriteLine(richTextBoxFileItem.Text);
                        }
                    }

                }
            }
            catch { };

        }

        private void buttonChangeOrAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var selected = treeView.SelectedNode.Text;
                foreach (var el in ReturnFileInfo(dir))
                {
                    if (el.ToString() == selected)
                    {
                        using (StreamWriter writer = el.CreateText())
                        {
                            writer.WriteLine(richTextBoxFileItem.Text);
                        }
                    }

                }
            }
            catch { };
        }
    }

}
