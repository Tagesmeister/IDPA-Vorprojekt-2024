using IDPA_Vorprojekt_2024.Classes;
using System.Globalization;
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
        private UserValues _userValue;
        private ValueCalculator _valuecalculator;

        private readonly SolidColorBrush red = new SolidColorBrush(Color.FromRgb(243, 96, 96));
        private readonly SolidColorBrush brown = new SolidColorBrush(Color.FromRgb(105, 58, 27));

        private List<TextBox> userInputFields = new List<TextBox>();
        private bool allInputsAreDouble = false;
        private bool allInputsMakeSense = false;

        public MainWindow()
        {
            InitializeComponent();
            ButtonCalculate.IsEnabled = false;

            userInputFields.Add(TextBoxJahresgewinn);
            userInputFields.Add(TextBoxAktienUndPartizipationskapital);
            userInputFields.Add(TextBoxGesetzlicheReserven);
            userInputFields.Add(TextBoxGewinnOderVerlustvortrag);
            userInputFields.Add(TextBoxGewünschteDividende);
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            _userValue = new UserValues(TextBoxJahresgewinn.Text, TextBoxAktienUndPartizipationskapital.Text, TextBoxGesetzlicheReserven.Text, TextBoxGewinnOderVerlustvortrag.Text, TextBoxGewünschteDividende.Text);
            _valuecalculator = new ValueCalculator(_userValue);
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

        private void TextBoxJahresgewinn_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxJahresgewinn.Text)) { UnderlineJahresgewinn.Stroke = red; }
            else { UnderlineJahresgewinn.Stroke = brown; }

            Validate();        
        }

        private void TextBoxAktienundPartizipationskapital_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxAktienUndPartizipationskapital.Text)) { UnderlineAK.Stroke = red; }
            else { UnderlineAK.Stroke = brown; }

            Validate();
        }

        private void TextBoxGesetzlicheReserven_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxGesetzlicheReserven.Text)) { UnderlineGesReserven.Stroke = red; }
            else { UnderlineGesReserven.Stroke = brown; }

            Validate();
        }

        private void TextBoxGewinnOderVerlustvortrag_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxGewinnOderVerlustvortrag.Text)) { UnderlineGewinnOderVerlustvortrag.Stroke = red; }
            else { UnderlineGewinnOderVerlustvortrag.Stroke = brown; }

            Validate();
        }

        private void TextBoxGewünschteDividende_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsDouble(TextBoxGewünschteDividende.Text)) { UnderlineGewünschteDividende.Stroke = red; }
            else { UnderlineGewünschteDividende.Stroke = brown; }

            Validate();
        }

        private void Validate()
        {
            int validationViolations = 0;

            //Alle Textboxen müssen double und nicht leer sein
            foreach(TextBox textBox in userInputFields)
            {
                if(!IsDouble(textBox.Text))
                {
                    validationViolations++;
                }
            }

            //Ges Reserven dürfen nicht grösser sein als 20% AK
            if(IsDouble(TextBoxAktienUndPartizipationskapital.Text) && IsDouble(TextBoxGesetzlicheReserven.Text))
            {
                if (Convert.ToDouble(TextBoxGesetzlicheReserven.Text) > Convert.ToDouble(TextBoxAktienUndPartizipationskapital.Text) * 0.2)
                {
                    UnderlineAK.Stroke = red;
                    UnderlineGesReserven.Stroke = red;
                    validationViolations++;
                }
                else
                {
                    UnderlineAK.Stroke = brown;
                    UnderlineGesReserven.Stroke = brown;
                }
            }

            //Gewinnvortrag muss grösser als 0 sein.
            if(IsDouble(TextBoxGewinnOderVerlustvortrag.Text))
            {
                if (Convert.ToDouble(TextBoxGewinnOderVerlustvortrag.Text) <= 0)
                {
                    UnderlineGewinnOderVerlustvortrag.Stroke = red;
                    validationViolations++;
                }
            }

            //Gewinnvortrag muss grösser als 0 sein.
            if (IsDouble(TextBoxJahresgewinn.Text))
            {
                if (Convert.ToDouble(TextBoxJahresgewinn.Text) <= 0)
                {
                    UnderlineJahresgewinn.Stroke = red;
                    validationViolations++;
                }
            }

            if (validationViolations > 0)
            {
                ButtonCalculate.IsEnabled = false;
                return;
            }

            ButtonCalculate.IsEnabled = true;
        }
    }
}