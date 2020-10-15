using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace SpravaStudentov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DateTime> dates { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            data.Columns[0].IsReadOnly = true;
            data.Columns[1].IsReadOnly = true;
            data.Columns[2].IsReadOnly = true;
            this.dates = new List<DateTime>();

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (data.Items.Count > 0)
            {
                data.ItemsSource = null;
                data.Items.Clear();
                data.Items.Refresh();
                data.Columns.RemoveAt(3);
                data.Columns.RemoveAt(3);
            }

            if (IsNaruralScience())
            {
                List<StudentOfNaturalScience> students = getDataStudentOfNaturalScience();
                data.ItemsSource = students.ToList();
                students.Clear();
            }
            else
            {
                List<StudentOfHumanities> students = getDataStudentOfHumanities();
                data.ItemsSource = students.ToList();
                students.Clear();
            }
        }

        private List<StudentOfNaturalScience> getDataStudentOfNaturalScience()
        {
            using (StreamReader r = new StreamReader(selectScience()))
            {
                string json = r.ReadToEnd();
                r.Close();
                List<StudentOfNaturalScience> items = JsonConvert.DeserializeObject<List<StudentOfNaturalScience>>(json);

                if (this.dates != null) this.dates.Clear();

                foreach (var item in items)
                {
                    item.Date.ToString("dd/MM/yyyy");
                    this.dates.Add(item.Date);
                }
                return items;
            }
        }

        private List<StudentOfHumanities> getDataStudentOfHumanities()
        {
            using (StreamReader r = new StreamReader(selectScience()))
            {
                string json = r.ReadToEnd();
                r.Close();
                List<StudentOfHumanities> items = JsonConvert.DeserializeObject<List<StudentOfHumanities>>(json);

                if(this.dates != null) this.dates.Clear();

                foreach (var item in items)
                {
                    item.Date.ToString("dd/MM/yyyy");
                    this.dates.Add(item.Date);
                }
                return items;
            }
        }

        private string selectScience()
            {

            string text = science.SelectedItem.ToString();
            if (text.Equals("System.Windows.Controls.ListBoxItem: Prirodne"))
            {
                string file = "Prirodne.json";
                DataGridTextColumn c1 = new DataGridTextColumn();
                c1.Binding = new Binding("Math");
                c1.IsReadOnly = false;
                data.Columns.Add(c1);

                DataGridTextColumn c2 = new DataGridTextColumn();
                c2.Binding = new Binding("Physics");
                c2.IsReadOnly = false;
                data.Columns.Add(c2);

                data.Columns[3].Header = "Matika";
                data.Columns[4].Header = "Fyzika";
                return file;
            }
            else
            {
                
                DataGridTextColumn c3 = new DataGridTextColumn();
                c3.Binding = new Binding("Philosophy");
                c3.IsReadOnly = false;
                data.Columns.Add(c3);

                DataGridTextColumn c4 = new DataGridTextColumn();
                c4.Binding = new Binding("Latin");
                c4.IsReadOnly = false;
                data.Columns.Add(c4);

                data.Columns[3].Header = "Filozofia";
                data.Columns[4].Header = "Latina";
                return "Humanitne.json";
            }
        }

        private bool IsNaruralScience()
        {
            if (science.SelectedItem.ToString().Equals("System.Windows.Controls.ListBoxItem: Prirodne"))
            {
                return true;
            }
            else
                return false;    
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<StudentOfNaturalScience> _studentsOfNatural = new List<StudentOfNaturalScience>();
            List<StudentOfHumanities> _studentOfHumanities = new List<StudentOfHumanities>();
            var d = DateTime.Now.ToString("mm/dd/yyyy");
            Debug.WriteLine(d);

            if (IsNaruralScience())
            {
                using (var writer = new StreamWriter("Prirodne.json"))
                {
                    for (int i = 0; i <= data.Items.Count - 2; i++)
                    {
                        TextBlock firstName = data.Columns[0].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock secondName = data.Columns[1].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock date = data.Columns[2].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock first = data.Columns[3].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock second = data.Columns[4].GetCellContent(data.Items[i]) as TextBlock;

                        //date.Text = "23/12/1995";
                        //DateTime dateTime = DateTime.Parse(date.Text);

                        DateTime dateTime = dates[i];
                        byte math = Convert.ToByte(first.Text);
                        byte physics = Convert.ToByte(second.Text);

                        _studentsOfNatural.Add(new StudentOfNaturalScience()
                        {

                            FirstName = firstName.Text,
                            SecondName = secondName.Text,
                            Date = dateTime,
                            Math = math,
                            Physics = physics
                        });
                        if (i == data.Items.Count - 2)
                        {
                            string json = JsonConvert.SerializeObject(_studentsOfNatural.ToArray());
                            writer.WriteLine(json);
                        }

                    }
                }
            }
            else
            {
                using (var writer = new StreamWriter("Humanitne.json"))
                {
                    for (int i = 0; i <= data.Items.Count - 2; i++)
                    {
                        TextBlock firstName = data.Columns[0].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock secondName = data.Columns[1].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock date = data.Columns[2].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock first = data.Columns[3].GetCellContent(data.Items[i]) as TextBlock;
                        TextBlock second = data.Columns[4].GetCellContent(data.Items[i]) as TextBlock;

                        //date.Text = "23/12/1995";
                        //DateTime dateTime = DateTime.Parse(date.Text);
                        //DateTime dateTime = Convert.ToDateTime(date.Text);
                        DateTime dateTime = dates[i];
                        byte philosophy = Convert.ToByte(first.Text);
                        byte latin = Convert.ToByte(second.Text);

                        _studentOfHumanities.Add(new StudentOfHumanities()
                         {

                         FirstName = firstName.Text,
                        SecondName = secondName.Text,
                        Date = dateTime,
                        Philosophy = philosophy,
                        Latin = latin
                        });
                        if (i == data.Items.Count - 2)
                        {
                        string json = JsonConvert.SerializeObject(_studentOfHumanities.ToArray());
                        writer.WriteLine(json);
                        
                         }
                     }   
                }
            }
            MessageBox.Show("Ulozene");
        }
    }
}
