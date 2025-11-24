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
using Airlines_Shashin.Classes;
using MySql.Data.MySqlClient;

namespace Airlines_Shashin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow mainWindow;
        public List<TicketClass> ticketsClasses = new List<TicketClass>();
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            frame.Navigate(new Pages.Main());
        }

        public void LoadTickets()
        {
            ticketsClasses.Clear();
            string connection = "server=localhost;port=3306;database=pr26;uid=root;pwd=root";
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            mySqlConnection.Open();

            MySqlDataReader ticket_query = 
        }
    }
}
