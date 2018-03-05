﻿using System;
using System.Threading;
using System.Windows;

namespace XLoop.WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isError = false;

        private readonly ILoopEngine engine = new LoopEngine();
        private SynchronizationContext syncContext = null;

        public MainWindow()
        {
            InitializeComponent();

            this.btnStart.Click += (s, e) => Start();
            this.btnStop.Click += (s, e) => Stop();
            this.btnError.Click += (s, e) => Error();

            syncContext = SynchronizationContext.Current;

            engine.OnWorking += engine_OnWorking;
            engine.Intervel = 500;
        }

        /// <summary>
        /// 启动
        /// </summary>
        private void Start()
        {
            engine.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        private void Stop()
        {
            engine.Stop();
        }

        /// <summary>
        /// 发生错误
        /// </summary>
        private void Error()
        {
            isError = true;
        }

        /// <summary>
        /// 引擎需要执行的方法
        /// </summary>
        void engine_OnWorking()
        {
            syncContext.Send(obj =>
            {
                try
                {
                    if (isError) throw new Exception("sorry, something gets wrong!");

                    this.lsb.Items.Add(DateTime.Now.Ticks);
                }
                catch (Exception ex)
                {
                    this.lsb.Items.Add(ex.Message);//处理异常
                }
                finally
                {
                    isError = false;
                }
            }, null);
        }
    }
}
