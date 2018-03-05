# XLoop
XTask是一个基于C#编写的客户端异步工作引擎.

## 运行环境: 
.Net Framework 2.0或以上.

## 用法
```C#
public partial class Form1 : Form
{
    //创建一个循环工作引擎
    private readonly ILoopEngine engine = new LoopEngine();
    private SynchronizationContext syncContext = null;

    public Form1()
    {
        InitializeComponent();

        this.btnStart.Click += (s, e) => Start();
        this.btnStop.Click += (s, e) => Stop();

        engine.OnWorking += engine_OnWorking;//注册需要循环执行的方法
        engine.Intervel = 500;//时间间隔为500ms

        syncContext = SynchronizationContext.Current;
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
    /// 引擎需要执行的方法
    /// </summary>
    void engine_OnWorking()
    {
        syncContext.Send(obj =>
        {
            try
            {
                this.lsb.Items.Add(DateTime.Now.Ticks);
            }
            catch (Exception ex)
            {
                this.lsb.Items.Add(ex.Message);//处理异常
            }
        }, null);
    }
}
```

**更多详见Demo代码**