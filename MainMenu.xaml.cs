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


namespace ParentMobile
{
    public partial class MainMenu : PhoneApplicationPage
    {


        public MainMenu()
        {
            InitializeComponent();
        }



        private void image2_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Grades.xaml", UriKind.Relative));
        }
    }
}