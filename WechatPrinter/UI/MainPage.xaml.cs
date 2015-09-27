﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WechatPrinter.Support;

namespace WechatPrinter
{
    /// <summary>
    /// MainPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainPage : Page
    {
        MainWindow window;

        public MainPage()
        {
            InitializeComponent();
        }
        private void page_main_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                window.Close();
            //else if (e.Key == Key.J)
            //    server.HideAdImg();
            //else if (e.Key == Key.Enter)
            //{
            //    Console.WriteLine("Enter");
            //    server.GetPrintImg(WechatPrinterConf.PrintImgUrl);
            //}
        }

        private void page_main_Loaded(object sender, RoutedEventArgs e)
        {
            window = (MainWindow)Window.GetWindow(this);
            window.KeyDown += page_main_KeyDown;
            
        }
       

        private void image_QR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (MessageBox.Show("删除缓存并重新加载？", "提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            //{
            //    FileUtils.CleanCache();
            //    System.Windows.Forms.Application.Restart();
            //    Application.Current.Shutdown();
            //}
        }

        private void mediaElement_ad_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaElement_ad.Position = TimeSpan.FromSeconds(0);
            mediaElement_ad.Play();
        }
    }
}
