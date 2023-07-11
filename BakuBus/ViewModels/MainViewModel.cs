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


        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ResetCommand { get; set; }


        private string selectedbus;

        public string SelectedBus
        {
            get { return selectedbus; }
            set { selectedbus = value; INotifyPropertyChanged(); }
        }

        public static Bakubus MyBuses { get; set; }

        public static Map Map { get; set; }











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



        


        public MainViewModel(Map map = null)
        {
            Map = map;
            SearchCommand = new(Search);
            ResetCommand = new(Reset);
            
        }











    }
}
