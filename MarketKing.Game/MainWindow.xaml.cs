﻿using DemoStrategy;
using MarketKing.Game.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MarketKing.Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Engine _engine;
        private readonly Render _render;
        private readonly Statistics _stats;
        public MainWindow()
        {
            InitializeComponent();
            _stats = new Statistics(Dispatcher);
            _render = new Render(canvas, zoom, Dispatcher);
            _engine = new Engine(GetStrategies(10), _render, _stats);
            KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F1:
                    break;
                case Key.F2:
                    break;
                case Key.F3:
                    break;
                case Key.F4:
                    break;
                case Key.F5:
                    StartGame();
                    break;
                case Key.F6:
                    break;
                case Key.F7:
                    break;
                case Key.F8:
                    break;
                case Key.F9:
                    break;
                case Key.F10:
                    break;
                case Key.F11:
                    ToggleFullscreen();
                    break;
            }
            
        }

        private void ToggleFullscreen()
        {
            if(WindowState != WindowState.Maximized)
            {
                WindowState = WindowState.Maximized;
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                WindowState = WindowState.Normal;
                ResizeMode = ResizeMode.CanResize;
            }
        }

        private bool _gameRunning;
        private async void StartGame()
        {
            if (!_gameRunning)
            {
                _gameRunning = true;
                await _engine.RunStartSequence();
            }
        }

        private IStrategy[] GetStrategies(int count)
        {
            var strategies = new RandomStrategy[count];
            for (int i = 0; i < count; i++)
                strategies[i] = new RandomStrategy();

            return strategies;
        }
    }
}
