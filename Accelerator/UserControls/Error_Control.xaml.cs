using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Accelerator.Class;

//https://go.microsoft.com/fwlink/?LinkId=234236 上介绍了“用户控件”项模板

namespace Accelerator.UserControls
{
    public sealed partial class Error_Control : UserControl
    {
        public Error_Control()
        {
            this.InitializeComponent();
            eMessageTB.Text = Ins_Error.eMessage;
            //eInnerExceptionTB.Text = Ins_Error.eInnerexception;
            //eStackTraceTB.Text = Ins_Error.eStackTrace;
        }

        public void Refreshing()
        {
            pr.IsActive = true;
            sy.Visibility = Visibility.Collapsed;
            eMessageTB.Text = "Loading...🧐";
            //eInnerExceptionTB.Text = "";
            //eStackTraceTB.Text = "";
        }
    }
}
