﻿using System.Globalization;
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
using System.Xml;

namespace IDPA_Vorprojekt_2024
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SolidColorBrush red = new SolidColorBrush(Color.FromRgb(243, 96, 96));
        private readonly SolidColorBrush brown = new SolidColorBrush(Color.FromRgb(105, 58, 27));

        public MainWindow()
        {
            InitializeComponent();
            ButtonCalculate.IsEnabled = false;
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBoxJahresgewinn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxJahresgewinn.Text)) { UnderlineJahresgewinn.Stroke = red; }
            else { UnderlineJahresgewinn.Stroke = brown; }
        }

        private bool IsDouble(string userInput)
        {
            try
            {
                Convert.ToDouble(userInput);
                string[] parts = userInput.Split('.');
                if (parts.Length > 1 && parts[1].Length > 2)
                {
                    throw new Exception();
                }
                return true;
            }
            catch { return false; }
        }
    }
}