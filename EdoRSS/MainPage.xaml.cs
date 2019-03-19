using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x410

namespace EdoRSS
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard1.Begin();

            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStreamAsync(@"https://www.ilsole24ore.com/rss/impresa-e-territori/lavoro.xml");

                var tutte = from not in XElement.Load(response).Element("channel").Elements("item")
                                     select new Notizia { Titolo = not.Element("title").Value, Testo = not.Element("description").Value }; ;

                StringBuilder sb = new StringBuilder();
                foreach( var n in tutte)
                {
                    sb.Append(n.Titolo + " ----- ");
                }

                Txtblock.Text = sb.ToString();



                lsDati.ItemsSource = tutte;
            }
            catch (Exception erore)
            {

            }
        }
    }


    public class Notizia
    {
        public string Titolo { get; set; }
        public string Testo { get; set; }
    }
}
