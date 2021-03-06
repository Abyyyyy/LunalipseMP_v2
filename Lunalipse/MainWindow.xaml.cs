﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Lunalipse.Core.PlayList;
using Lunalipse.Core.Metadata;
using Lunalipse.Core.LpsAudio;
using Lunalipse.Common.Data;
using Lunalipse.Core.BehaviorScript;
using Lunalipse.Presentation.LpsWindow;
using System.Windows.Threading;

namespace Lunalipse
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : LunalipseDialogue
    {
        MusicListPool mlp;
        MediaMetaDataReader mmdr;
        LpsAudio laudio;
        Interpreter intp;
        Dialogue dia;
        public MainWindow() : base()
        {
            InitializeComponent();
            mlp = MusicListPool.INSATNCE;
            laudio = LpsAudio.INSTANCE();
            mmdr = new MediaMetaDataReader();
            mlp.AddToPool("F:/M2", mmdr);
            dipMusic.ItemsSource = mlp.Musics;
            intp = Interpreter.INSTANCE(@"F:\Lunalipse\TestUnit\bin\Debug");
            //if (intp.Load("prg2"))
            //{
            //    PlayFinished();
            //}
            alb.Source = mlp.ToCatalogue().GetCatalogueCover();

            AudioDelegations.PostionChanged += (x) =>
            {
                //Console.WriteLine(x);
            };
            AudioDelegations.PlayingFinished += PlayFinished;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.EnableBlur();
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        if(laudio.Playing)
            //            Console.WriteLine(string.Join(",", AudioDelegations.FftAcquired()));
            //        Thread.Sleep(100);
            //    }
            //});
        }


        private void Window_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void dipMusic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Next(false);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            laudio.Dispose();
        }

        private void PlayFinished()
        {
            MusicEntity MEnt = null;
            if (intp.LBSLoaded)
                MEnt = intp.Stepping();
            else
            {
                Next(true);
            }
            //if (MEnt != null)
            //{
            //    laudio.Load(MEnt);
            //    laudio.Play();
            //}
            //else return;
        }

        private void Next(bool proccedNext)
        {
            if (proccedNext)
            {
                Dispatcher.Invoke(() => dipMusic.SelectedIndex++);
                return;
            }
            if (laudio.Playing) laudio.Stop();
            laudio.Load((MusicEntity)dipMusic.SelectedItem);
            laudio.Play();
            if (dia == null)
            {
                dia = new Dialogue(new _3DVisualize(), "3D");
                dia.Show();
            }
        }
    }
}
