using BakuBus.Models;
using Microsoft.Maps.MapControl.WPF;
using Source.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace BakuBus.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        void INotifyPropertyChanged([CallerMemberName] string? proPertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proPertyName));




        public Attributes BusTooltip { get; set; }


        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }


        private string selectedbus;

        public string SelectedBus
        {
            get { return selectedbus; }
            set { selectedbus = value; INotifyPropertyChanged(); }
        }



        private Map map;

        public Map Map
        {
            get { return map; }
            set { map = value; INotifyPropertyChanged(); }
        }




        

        private Bakubus? myBuses;
        public Bakubus? MyBuses
        {
            get => myBuses;
            set
            {
                myBuses = value;
                INotifyPropertyChanged();
            }
        }





        public void Reset(object? param)
        {
            foreach (var item in Map.Children)
            {
                if (item is Pushpin p)
                {
                    p.Visibility=Visibility.Visible;
                }
            }
        }



        public void Search(object?param)
        {
            foreach (var item in Map.Children)
            {
                if(item is Pushpin p)
                {
                    if (p.Content.ToString()== SelectedBus)
                    {
                        p.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        p.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }



        private void ReadData()
        {
            MyBuses = JsonFileHandler.Read<Bakubus>("bakubusApi.json");
            foreach (var item in MyBuses.BUS) {
                Brush pushpincolor = new SolidColorBrush();
                Pushpin pushpin = new();
                pushpin.Location = new(Convert.ToDouble(item.attributes.LATITUDE), Convert.ToDouble(item.attributes.LONGITUDE));
                pushpin.Content = item.attributes.DISPLAY_ROUTE_CODE;
                pushpin.Tag = item.attributes;
                pushpin.ToolTip = new();
                pushpin.MouseEnter += Focus;

                if (!MyBuses.BusNumbers.Contains(item.attributes.DISPLAY_ROUTE_CODE)) 
                { 
                    MyBuses.BusNumbers.Add(item.attributes.DISPLAY_ROUTE_CODE);
                    pushpincolor= new SolidColorBrush(Color.FromRgb(Convert.ToByte(Random.Shared.Next(0, 255)), Convert.ToByte(Random.Shared.Next(0, 255)), Convert.ToByte(Random.Shared.Next(0, 255))));
                }
                else
                {
                    foreach (var mapch in Map.Children)
                    {
                        if(mapch is Pushpin p)
                        {
                            if(p.Content.ToString()== item.attributes.DISPLAY_ROUTE_CODE)
                            {
                                pushpincolor = p.BorderBrush;
                            }
                        }
                    }
                }
                pushpin.BorderBrush = pushpincolor;
                Map.Children.Add(pushpin);

            }
        }

        
        private void Focus(object sender,EventArgs a)
        {
            Pushpin pushpin = sender as Pushpin;
            BusTooltip = pushpin.Tag as Attributes;
        }

        
        
        
        




        public MainViewModel(Map map = null)
        {
            Map = map;
            ReadData();
            SearchCommand = new(Search);
            ResetCommand = new(Reset);
        }











    }
}
