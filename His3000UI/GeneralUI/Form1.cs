using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralApp.ControlesWinForms
{
    public partial class Form1 : Form
    {

        string strDirectorio =  "2011" + "//" + "2012" + "//";  //aqui reemplazar con variables
        string strPathGeneral = "C://Users//Usuario//Desktop//a1//";  //TRAER DESDE BASE DE DATOS PARAMETROS

        DirectoryInfo directoryInfo = new DirectoryInfo(@"C://Users//Usuario//Desktop//a1//");

        public Form1()
        {
            InitializeComponent();
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //folderBrowserDialog1.SelectedPath = txtDirectoryPath.Text;
            DialogResult drResult = folderBrowserDialog1.ShowDialog();
            if (drResult == System.Windows.Forms.DialogResult.OK)
            {
               // txtDirectoryPath.Text = folderBrowserDialog1.SelectedPath;

                DirectoryCopy(folderBrowserDialog1.SelectedPath, directoryInfo.ToString(), true);
            }
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            refresh();
        }

        public void refresh() {

            treeView1.Nodes.Clear();

            directoryInfo = new DirectoryInfo(strPathGeneral + strDirectorio);

            if (!(Directory.Exists(directoryInfo.ToString())))
            {
                Directory.CreateDirectory(directoryInfo.ToString());
            }


            if (directoryInfo.Exists)
            {
                try
                {
                    treeView1.Nodes.Add(LoadDirectory(directoryInfo));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            treeView1.Nodes[0].Expand();

        }

        private string[] findFile()
        {
            var filePath = string.Empty;
            var fileName = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                //openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;   //path completo
                    fileName = openFileDialog.SafeFileName;   //solo nombre del archivo
                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    
                }
            }
            return new string[] {filePath, fileName} ;
        }

        private TreeNode LoadDirectory(DirectoryInfo di)
        {
            if (!di.Exists)
                return null;

            TreeNode output = new TreeNode(di.Name, 0, 0);

            foreach (var subDir in di.GetDirectories())
            {
                try
                {
                    output.Nodes.Add(LoadDirectory(subDir));
                }
                catch (IOException ex)
                {
                    //handle error
                }
                catch { }
            }

            foreach (var file in di.GetFiles())
            {
                if (file.Exists)
                {
                    output.Nodes.Add(file.FullName, file.Name);
                }
            }

            return output;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                Process p = new Process();
                            p.StartInfo.FileName = treeView1.SelectedNode.Name;
                            p.Start();
            }
            catch (Exception)
            {   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                string[] x = findFile();

                //DirectoryCopy(x[0],directoryInfo.ToString(),false);

                File.Copy(x[0], directoryInfo.ToString() + x[1]);

                refresh();
            }
            catch (Exception)
            {
            }
           

        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.StartInfo.FileName = treeView1.SelectedNode.Name;
                p.Start();
            }
            catch (Exception)
            {
            }

        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);

                if (treeView1.SelectedNode != null)
                {
                    myContextMenuStrip.Show(treeView1, e.Location);

                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                File.Delete(treeView1.SelectedNode.Name);
                refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

