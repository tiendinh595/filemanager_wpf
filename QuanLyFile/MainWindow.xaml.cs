using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace QuanLyFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DriveInfo[] drives;
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                drives = DriveInfo.GetDrives();
                //foreach(string str in Directory.GetLogicalDrives()){
                foreach(DriveInfo item in drives)
                {
                    StackPanel stp = new StackPanel();
                    stp.Orientation = Orientation.Horizontal;

                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri("Images/Drive.png",UriKind.Relative));
                    img.Width = img.Height = 16;

                    stp.Children.Add(img);
                    TextBlock text = new TextBlock();
                    text.Text = item.Name;
                    text.Margin = new Thickness(10, 0, 0, 0);

                    stp.Children.Add(text);
                    cmbDisk.Items.Add(stp);
                }
                cmbDisk.SelectedIndex = 0;
                LoadContent(@"C:\", "Folder");
                LoadContent(@"C:\", "File");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }


        private string[] LoadSubDir(string dirName)
        {
            try
            {
                return Directory.GetDirectories(dirName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }

        private string[] LoadSubFiles(string dirName)
        {
            try
            {
                return Directory.GetFiles(dirName);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return null; }
        }


        private void LoadContent(string dirName, string type)
        {
            string[] list;
            if (type == "File")
                list = LoadSubFiles(dirName);
            else
                list = LoadSubDir(dirName);
            string[] arrExt = {".png", ".jpg", ".jpeg"};
            if(list != null)
            {
                foreach (string fileName in list)
                {
                    StackPanel stp = new StackPanel();
                    stp.Orientation = Orientation.Horizontal;

                    Image img = new Image();

                    stp.Children.Add(img);
                    TextBlock text = new TextBlock();
                    if (type == "File")
                    {
                        FileInfo fi = new FileInfo(fileName);
                        text.Text = fi.Name;
                        if (Array.IndexOf(arrExt, fi.Extension.ToLower()) >= 0)
                        {
                            img.Source = new BitmapImage(new Uri(fi.FullName, UriKind.Absolute));
                            img.Width = img.Height = 24;
                        }
                        else
                        {
                            img.Source = new BitmapImage(new Uri("Images/" + type + ".png", UriKind.Relative));
                            img.Width = img.Height = 16;
                        }
                    }
                    else
                    {
                        DirectoryInfo fo = new DirectoryInfo(fileName);
                        text.Text = fo.Name;
                        img.Source = new BitmapImage(new Uri("Images/" + type + ".png", UriKind.Relative));
                        img.Width = img.Height = 16;
                    }
                    

                    text.Margin = new Thickness(10, 0, 0, 0);

                    stp.Children.Add(text);
                    lstListFile.Items.Add(stp);
                }
            }
        }

        private void cmbDisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstListFile.Items.Clear();
            string strPath = ((TextBlock)((StackPanel)cmbDisk.SelectedItem).Children[1]).Text;
            LoadContent(strPath, "Folder");
            LoadContent(strPath, "File");
        }

        private void lstListFile_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string dirPath = ((TextBlock)((StackPanel)cmbDisk.SelectedItem).Children[1]).Text;
            string strPath = dirPath + ((TextBlock)((StackPanel)lstListFile.SelectedItem).Children[1]).Text;
            lstListFile.Items.Clear();
            LoadContent(strPath, "Folder");
            LoadContent(strPath, "File");
        }


    }
}
