using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Newtonsoft.Json.Linq;

namespace ParentMobile
{
    public partial class Grades : PhoneApplicationPage
    {
        private JObject studentData { get; set; }
        private JArray courses { get; set; }
        private JObject grades { get; set; }
        private int rowCount { get; set; }

        public Grades()
        {
            InitializeComponent();
            getStudentData();
            buildCoursesInterface();
        }

        private void getStudentData()
        {
            studentData = Factory.StudentData;
            courses = (JArray)studentData["courses"];
            grades = (JObject)studentData["grades"];
            rowCount = courses.Count;
        }

        private void buildCoursesInterface()
        {
            
            //Grid cpanel = ContentPanel;

            Grid controlPanel = ContentPanel;
            controlPanel.Height = rowCount * 140;
            controlPanel.ShowGridLines = true;
            ImageBrush imgBrush1 = new ImageBrush();
            imgBrush1.ImageSource = (ImageSource)new ImageSourceConverter().ConvertFromString("Assets/cellbkg1.png");
            imgBrush1.Stretch = Stretch.Fill;
            controlPanel.Background = imgBrush1;

            String gender = "";

            for (int i = 0; i < rowCount; i++)
            {

                //Grid cpanel = new Grid();
                //cpanel.Background = imgBrush;
                //cpanel.Width = 456;
                //cpanel.Height = 120;
                //cpanel.ShowGridLines = true;

                controlPanel.RowDefinitions.Add(new RowDefinition { Height = new GridLength(140) });
/*              Background u bir turlu duzgun ayarlayamadim
                controlPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(124) });
                controlPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(220) });
                controlPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(124) });
                controlPanel.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(124) });
*/                
                // sol taraftaki img
                Image img = new Image();
                img.Height = 106;
                img.Name = "grades";
                img.Source = (ImageSource)new ImageSourceConverter().ConvertFromString("Assets/grades.png");
                img.Stretch = Stretch.Fill;
                img.Width = 124;
                img.HorizontalAlignment = HorizontalAlignment.Left;
                img.VerticalAlignment = VerticalAlignment.Center;
                img.Margin = new Thickness(-5, 5, 0, 0);
                Grid.SetRow(img, i);
                Grid.SetColumn(img, 0);
                controlPanel.Children.Add(img);

                // img nin hemen sagindaki ders ismi
                TextBlock tb1 = new TextBlock();
                tb1.Foreground = new SolidColorBrush(Color.FromArgb(255, 200, 0, 0));
                tb1.FontFamily = new FontFamily("Century Gothic");
                tb1.FontSize = 28;
                if (courses[i]["gender"].ToString().Equals("M")) gender = "Mr."; else gender = "Ms.";

                tb1.Text = courses[i]["coursename"].ToString() + "\n" + gender + " " + courses[i]["lastname"].ToString(); 
                tb1.HorizontalAlignment = HorizontalAlignment.Left;
                tb1.VerticalAlignment = VerticalAlignment.Top;
                tb1.Margin = new Thickness(125, 15, 0, 0);
                Grid.SetRow(tb1, i);
                Grid.SetColumn(tb1, 1);
                controlPanel.Children.Add(tb1);
                

                TextBlock tb2 = new TextBlock();
                tb2.Foreground = new SolidColorBrush(Color.FromArgb(255, 8, 8, 255));
                tb2.FontFamily = new FontFamily("Century Gothic");
                tb2.Margin = new Thickness(125, 5, 0, 0);
                tb2.FontSize = 26;
                string cur_rc = grades[courses[i]["courseid"].ToString()]["rp"].ToString();
                tb2.Text = "Current :"+grades[courses[i]["courseid"].ToString()]["R"+cur_rc].ToString();
                tb2.HorizontalAlignment = HorizontalAlignment.Left;
                tb2.VerticalAlignment = VerticalAlignment.Bottom;
                Grid.SetRow(tb2, i);
                Grid.SetColumn(tb2, 1);
                controlPanel.Children.Add(tb2);

                Button btnDetails = new Button();
                btnDetails.Content = "detail";
                btnDetails.HorizontalAlignment = HorizontalAlignment.Center;
                btnDetails.VerticalAlignment = VerticalAlignment.Center;
                btnDetails.Margin = new Thickness(355, 0, 0, 0);
                Grid.SetRow(btnDetails, i);
                Grid.SetColumn(btnDetails, 2);
                controlPanel.Children.Add(btnDetails);
               

            }

           
            
        }
    }
}