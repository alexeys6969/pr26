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
using Airlines_Shashin.Elements;
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

        public List<TicketClass> LoadTickets(string from, string to)
        {
            ticketsClasses.Clear();
            string connection = "server=127.0.0.1;port=3306;database=Airlines;uid=root;";
            MySqlConnection mySqlConnection = new MySqlConnection(connection);
            mySqlConnection.Open();

            MySqlDataReader ticket_query = WorkingBd.Connection.Query($"SELECT price, `from`, `to`, time_start, time_way FROM Airlines.Tickets where `from` = '{from}' or `to` = '{to}'", mySqlConnection);
            while(ticket_query.Read())
            {
                string price = ticket_query.IsDBNull(0) ? "" : ticket_query.GetString(0);
                string fromCity = ticket_query.IsDBNull(1) ? "" : ticket_query.GetString(1);
                string toCity = ticket_query.IsDBNull(2) ? "" : ticket_query.GetString(2);

                DateTime timeStart = DateTime.MinValue;
                TimeSpan timeWay = TimeSpan.Zero;

                if (!ticket_query.IsDBNull(3))
                {
                    try
                    {
                        timeStart = ticket_query.GetDateTime(3);
                    }
                    catch
                    {
                        // Если это время, а не дата
                        if (ticket_query.GetFieldType(3) == typeof(TimeSpan))
                            timeStart = DateTime.Today.Add(ticket_query.GetTimeSpan(3));
                    }
                }

                if (!ticket_query.IsDBNull(4))
                {
                    timeWay = ticket_query.GetTimeSpan(4);
                }

                TicketClass tickets = new TicketClass(
                    price,
                    fromCity,
                    toCity,
                    timeStart,
                    timeWay
                );

                ticketsClasses.Add(tickets);
            }
            mySqlConnection.Close();
            return ticketsClasses;
        }
    }
}
