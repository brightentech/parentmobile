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
using System.IO.IsolatedStorage;
using Newtonsoft.Json.Linq;


namespace ParentMobile
{
    public partial class MainPage : PhoneApplicationPage
    {
        private String schoolURL { get; set; }
        private String responseString { get; set; }
        private String studentData { get; set; }
        private Dictionary<string, string> loginInfo { get; set; }
        private bool isLoggedIn { get; set; }
        HttpWebRequest myRequest;
        private JObject jsonobj;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            addListItem("Harmony Innovation");
            
        }

        private void addListItem(string pItem)
        {
            listBox1.Items.Add(pItem);
        }

        private void listBox1_Tap(object sender, GestureEventArgs e)
        {

        }

        private void login(string pUsername, string pPassword, string pURL)
        {

            string requestString = pURL;

            loginInfo = new Dictionary<string, string>();
            loginInfo.Add("username", pUsername);
            loginInfo.Add("password", pPassword);
            //MessageBox.Show(loginInfo.ElementAt(0).Value);

            myRequest = (HttpWebRequest)WebRequest.Create(pURL);
            myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.0.3705)";
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.BeginGetRequestStream(new AsyncCallback(GetRequestStreamCallback), myRequest);
 
        }

        

        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            //MessageBox.Show("getrequeststreamcallback START");
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult);

            string parametersString = "";
            foreach (KeyValuePair<string, string> parameter in loginInfo)
            {
                parametersString = parametersString + (parametersString != "" ? "&" : "") + string.Format("{0}={1}", parameter.Key, parameter.Value);
            }

            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(parametersString);
            postStream.Write(byteArray, 0, parametersString.Length);
            postStream.Close();
           // MessageBox.Show("getrequeststreamcallback END");
            request.BeginGetResponse(new AsyncCallback(GetResponseCallback), request);
        }

        private void GetResponseCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);
            System.IO.Stream streamResponse = response.GetResponseStream();
            System.IO.StreamReader streamRead = new System.IO.StreamReader(streamResponse);
            responseString = streamRead.ReadToEnd();

            jsonobj = JObject.Parse(responseString); 
            streamResponse.Close();
            streamRead.Close();
            response.Close(); 

            if (!responseString.Equals("0"))
            {
                Factory.StudentData = jsonobj;
                Deployment.Current.Dispatcher.BeginInvoke(()=>
                {
                    NavigationService.Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
                });
                
            }

            
        }

       


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            login(txtUsername.Text, txtPassword.Password, "https://hsihouston.org/database/parentapp/loginwp.php");
            
        }
    }
}