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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddTrener.xaml
    /// </summary>
    public partial class AddTrener : Window
    {

        public AddTrener()
        {
            InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Fio.Text != "" && Date.SelectedDate != null )
                this.DialogResult = true;
            else
                MessageBox.Show("пустые значения");

        }
    }
}
