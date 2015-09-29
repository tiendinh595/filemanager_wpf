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
using System.Windows.Media;

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
            
            drives = DriveInfo.GetDrives();
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
        }


    }
}
