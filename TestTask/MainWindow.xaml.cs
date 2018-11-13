using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Web.Script.Serialization;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;

namespace TestTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public bool IsNum(string s) // Check int
        {
            foreach (char c in s)
            {
                if (!Char.IsDigit(c)) return false;
            }
            return true;
        }

        public void ResetValue()
        {
            NameBox.Text = "";
            YearsBox.Text = "";
            CountryBox.Text = "";
            XmlRbtn.IsChecked = false;
            JsonRbtn.IsChecked = false;
        }

        public void YearsBox_PreviewTextInput(object sender, TextCompositionEventArgs e)//For numbers of YearsBox
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
        public void NameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)//For letters of NameBox
        {
            if (!Char.IsLetter(e.Text, 0)) e.Handled = true;
        }

        public void ShowObj(List<NamefromTextBox> a)
        {
            foreach (var item in a)
            {

                if (item.NameData == FindBox.Text)
                {
                    NameBox.Text = item.NameData;
                    YearsBox.Text = item.YearsData;
                    CountryBox.Text = item.CountryData;
                    break;
                }
                if (item.YearsData == FindBox.Text)
                {
                    NameBox.Text = item.NameData;
                    YearsBox.Text = item.YearsData;
                    CountryBox.Text = item.CountryData;
                    break;
                }
                if (item.CountryData == FindBox.Text)
                {
                    NameBox.Text = item.NameData;
                    YearsBox.Text = item.YearsData;
                    CountryBox.Text = item.CountryData;
                    break;
                }
            }
        }
        public void ShowObj1(List<NamefromTextBox> a)
        {
            foreach (var item in a)
            {
                if (item.YearsData == FindBox.Text)
                {
                    NameBox.Text = item.NameData;
                    YearsBox.Text = item.YearsData;
                    CountryBox.Text = item.CountryData;
                    break;
                }
              
            }
        }

        public string[] Names { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Names = new string[] { "Россия", "Германия", "Италия" };
            DataContext = this;
            this.YearsBox.PreviewTextInput += new TextCompositionEventHandler(YearsBox_PreviewTextInput);//For numbers of YearsBox
            this.NameBox.PreviewTextInput += new TextCompositionEventHandler(NameBox_PreviewTextInput);//For letters of NameBox

        }
      
            List<NamefromTextBox> textboxvalue = new List<NamefromTextBox>();
            XmlSerializer formatter = new XmlSerializer(typeof(List<NamefromTextBox>));
            DataContractJsonSerializer jsonFormatter= new DataContractJsonSerializer(typeof(List<NamefromTextBox>));

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == "" || YearsBox.Text == "" || CountryBox.Text == "") { MessageBox.Show("Require to fill in all fields!", "Error"); }
            else if (XmlRbtn.IsChecked == false && JsonRbtn.IsChecked == false) { MessageBox.Show("Choose format to save", "Error"); };
            try
            {
                NamefromTextBox info = new NamefromTextBox();
                if (XmlRbtn.IsChecked == true)
                { 
                   FileStream xmlFile = new FileStream("StaffData.txt", FileMode.Create);
                    info.NameData = NameBox.Text;
                    info.YearsData = YearsBox.Text;
                    info.CountryData = CountryBox.Text;
                    textboxvalue.Add(info);
                    formatter.Serialize(xmlFile, textboxvalue);
                    xmlFile.Close();
                    ResetValue();
                }
                else if (JsonRbtn.IsChecked == true)
                {
                  FileStream jsonFile = new FileStream("StaffData.txt", FileMode.Create);
                    info.NameData = NameBox.Text;
                    info.YearsData = YearsBox.Text;
                    info.CountryData = CountryBox.Text;
                    textboxvalue.Add(info);
                    jsonFormatter.WriteObject(jsonFile, textboxvalue);
                    jsonFile.Close();
                    ResetValue();
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void FindBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (XmlRbtn.IsChecked == true)
                {
                    string file2 = File.ReadAllText("StaffData.txt");
                    StringReader stringReader = new StringReader(file2);
                    List<NamefromTextBox> returnObject = (List<NamefromTextBox>)formatter.Deserialize(stringReader);

                    if (IsNum(FindBox.Text) == true)
                    {
                        ShowObj1(returnObject);
                    }
                    else
                    {
                        ShowObj(returnObject);
                    }

                    //------- Second the way to show XML--------

                    /* var xData = XDocument.Load("StaffData.txt");
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(@"StaffData.txt");

                    foreach (XmlNode node in xmlDoc.GetElementsByTagName("NameData"))
                    {
                        if (IsNum(FindBox.Text) == true) break;
                        if (node.InnerText == FindBox.Text)
                        {
                            NameBox.Text = node.InnerText;
                            YearsBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("NameData").Value == FindBox.Text).Element("YearsData").Value;
                            CountryBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("NameData").Value == FindBox.Text).Element("CountryData").Value;
                            FindBox.Text = "";
                        }
                    }
                    foreach (XmlNode node in xmlDoc.GetElementsByTagName("YearsData"))
                        if (node.InnerText == FindBox.Text)
                        {
                            YearsBox.Text = node.InnerText;
                            NameBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("YearsData").Value == FindBox.Text).Element("NameData").Value;
                            CountryBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("YearsData").Value == FindBox.Text).Element("CountryData").Value;
                            FindBox.Text = "";
                        }
                    foreach (XmlNode node in xmlDoc.GetElementsByTagName("CountryData"))
                        if (node.InnerText == FindBox.Text)
                        {
                            CountryBox.Text = node.InnerText;
                            NameBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("CountryData").Value == FindBox.Text).Element("NameData").Value;
                            YearsBox.Text = xData.Descendants("NamefromTextBox").First(x => x.Element("CountryData").Value == FindBox.Text).Element("YearsData").Value;
                            FindBox.Text = "";
                        }*/
                }
                if (JsonRbtn.IsChecked == true)
                {
                    string file = File.ReadAllText("StaffData.txt");
                    List<NamefromTextBox> returnObject1 = new JavaScriptSerializer().Deserialize<List<NamefromTextBox>>(file);

                    if (IsNum(FindBox.Text) == true)
                    {
                        ShowObj1(returnObject1);
                    }
                    else
                    {
                        ShowObj(returnObject1);
                    }
                }
                if (FindBox.Text == "" && NameBox.Text == "") { MessageBox.Show("String of find is empty"); }
                if (FindBox.Text != "" && NameBox.Text == "") { MessageBox.Show("Nothing find"); }
            }
            
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
    }
}
