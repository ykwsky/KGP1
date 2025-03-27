using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Threading;

namespace D2RCharacterCopy
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        private static Mutex mutex;

        public MainWindow()
        {
            mutex = new Mutex(true, @"D2R_D2S_BACKUP");

            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("[D2R d2s Backup] program is already running.");
                Environment.Exit(0);
            }

            

            InitializeComponent();
        }

        public void SelectFile_Click(object sender, RoutedEventArgs e)
        {
            FileSelectBt.IsEnabled = false;
            
            Dispatcher.BeginInvoke(new Action(() =>
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = SharedData.GetPath();
                ofd.Filter = "D2S|*.d2s";
                if (ofd.ShowDialog() == true)
                {
                    FilePathTextBox.Text = ofd.FileName;
                    SharedData.SetPath(System.IO.Path.GetDirectoryName(ofd.FileName));
                    SharedData.BackupFile = ofd.FileName;
                }
            }), System.Windows.Threading.DispatcherPriority.Background);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                FileSelectBt.IsEnabled = true;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void Backup_Click(object sender, RoutedEventArgs e)
        {
            BackupBt.IsEnabled = false;

            DateTime systime = DateTime.Now;
            string backupTime = systime.ToString("_yyyyMMdd_HHmmss_fff");            

            Dispatcher.BeginInvoke(new Action(() =>
            {
                string filePath = SharedData.BackupFile;
                if (!string.IsNullOrEmpty(filePath))
                {
                    string dirPath = System.IO.Path.GetDirectoryName(filePath);
                    string backupPath = System.IO.Path.Combine(dirPath, "D2R_backup");

                    string searchFilter = System.IO.Path.GetFileNameWithoutExtension(filePath) + ".*";
                    string zipFileName = System.IO.Path.GetFileNameWithoutExtension(filePath) + backupTime + ".zip";
                    string zipFile = System.IO.Path.Combine(backupPath, zipFileName);                   

                    Directory.CreateDirectory(backupPath);
                    if (Directory.Exists(backupPath))
                    {
                        string[] zipTargetFileArray = Directory.GetFiles(dirPath, searchFilter, SearchOption.TopDirectoryOnly);

                        using (var fs = new FileStream(zipFile, FileMode.Create, FileAccess.ReadWrite))
                        {
                            using (var za = new ZipArchive(fs, ZipArchiveMode.Create))
                            {
                                foreach (string targetFile in zipTargetFileArray)
                                {
                                    try
                                    {
                                        za.CreateEntryFromFile(targetFile, System.IO.Path.GetFileName(targetFile));
                                    }
                                    catch
                                    {
                                        continue;
                                    }
                                }                                
                            }
                        }

                        BackupDirResult.Text = System.IO.Path.GetDirectoryName(zipFile);
                        BackupResult.Text = System.IO.Path.GetFileName(zipFile);
                    }
                }
                else
                {
                    MessageBox.Show("Select a file (.d2s)");
                }
            }), System.Windows.Threading.DispatcherPriority.Background);

            Dispatcher.BeginInvoke(new Action(() =>
            {
                BackupBt.IsEnabled = true;
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void Dir_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(BackupDirResult.Text))
                System.Diagnostics.Process.Start(BackupDirResult.Text);
        }
    }
}
