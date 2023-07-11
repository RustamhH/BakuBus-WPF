using BakuBus;
using BakuBus.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows;
<<<<<<< HEAD
=======
using System.Windows.Media;
using System.Windows.Threading;

>>>>>>> 776ccdedb053b049da0db3dd4b929cdde8ffcc89
namespace Source.Views;

public partial class MainView : Window
{

    public MainView()
    {
        InitializeComponent();
        MainMap.CredentialsProvider = new ApplicationIdCredentialsProvider("ElvBxHgcOznZGeN7Cm4h~kS_wElEU143IExZKpWm0ng~An8GJ2D17ophAjEPgEQ8V6QyMapjKkmLP6OQE0ruwO42vfrMWqqCdG3W6GuOhLWN");
        DataContext = new MainViewModel(MainMap);

<<<<<<< HEAD
    }

   
=======
        DispatcherTimer time = new();
        time.Tick += ReadData;
        time.Interval = new TimeSpan(5000);
        time.Start();
    }

    private async void ReadData(object? sender, EventArgs e)
    {
        //MyBuses = JsonFileHandler.Read<Bakubus>("bakubusApi.json");
        
        var client = new HttpClient();
        var jsonStr = await client.GetStringAsync("https://raw.githubusercontent.com/CavidAtamoghlanov/newBakuBus/main/BakuBusApi");

        MainViewModel.MyBuses = JsonSerializer.Deserialize<Bakubus>(jsonStr);



        foreach (var item in MainViewModel.MyBuses.BUS)
        {
            Brush pushpincolor = new SolidColorBrush();
            Pushpin pushpin = new();
            pushpin.Location = new(Convert.ToDouble(item.attributes.LATITUDE), Convert.ToDouble(item.attributes.LONGITUDE));
            pushpin.Content = item.attributes.DISPLAY_ROUTE_CODE;


            if (!MainViewModel.MyBuses.BusNumbers.Contains(item.attributes.DISPLAY_ROUTE_CODE))
            {
                MainViewModel.MyBuses.BusNumbers.Add(item.attributes.DISPLAY_ROUTE_CODE);
                //pushpincolor = new SolidColorBrush(Color.FromRgb(Convert.ToByte(Random.Shared.Next(0, 255)), Convert.ToByte(Random.Shared.Next(0, 255)), Convert.ToByte(Random.Shared.Next(0, 255))));
            }
            //else
            //{
            //    foreach (var mapch in MainViewModel.Map.Children)
            //    {
            //        if (mapch is Pushpin p)
            //        {
            //            if (p.Content.ToString() == item.attributes.DISPLAY_ROUTE_CODE)
            //            {
            //                pushpincolor = p.Background;
            //            }
            //        }
            //    }
            //}
            //pushpin.Background = pushpincolor;
            MainViewModel.Map.Children.Add(pushpin);
        }
    }
>>>>>>> 776ccdedb053b049da0db3dd4b929cdde8ffcc89
}