﻿using BakuBus;
using BakuBus.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;


namespace Source.Views;

public partial class MainView : Window
{
    public MainView()
    {
        InitializeComponent();
        MainMap.CredentialsProvider = new ApplicationIdCredentialsProvider("ElvBxHgcOznZGeN7Cm4h~kS_wElEU143IExZKpWm0ng~An8GJ2D17ophAjEPgEQ8V6QyMapjKkmLP6OQE0ruwO42vfrMWqqCdG3W6GuOhLWN");
        DataContext = new MainViewModel(MainMap);
        
    }

    
}