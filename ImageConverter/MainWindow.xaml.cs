using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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
using ImageProcessor;

namespace ImageConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage input;
        BitmapImage output;
        ImgProcessor processor;

        public MainWindow()
        {
            InitializeComponent();
            processor = new ImgProcessor();
        }

        private void openFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            bool fileSelected = (bool)openFileDialog.ShowDialog();
            if (fileSelected) if (true)
                {
                    input = new BitmapImage(new Uri(openFileDialog.FileName));
                    inputImage.Source = input;
                    byte[,,] inputPixels = ConvertToArray(input);
                    if (processor.LoadImage(inputPixels))
                    {
                        openFileButton.IsEnabled = false;
                    }
                }
        }

        private byte[,,] ConvertToArray(BitmapImage input)
        {
            int height = input.PixelHeight;
            int width = input.PixelWidth;
            int stride = width * 4;
            int size = height * stride;
            byte[] data = new byte[size];
            byte[,,] result = new byte[height, width, 3]; 
            input.CopyPixels(data, stride, 0);
            int position;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    position = i * stride + j * 4;
                    for (int k = 0; k < 3; k++)
                    {
                        result[i, j, k] = data[position + k];
                    }
                }
            }

            return result;
        }
    }
}
