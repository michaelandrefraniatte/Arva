using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Rectangle = System.Drawing.Rectangle;
using Bitmap = System.Drawing.Bitmap;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Diagnostics;

namespace ARVA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        private static int Width = Screen.PrimaryScreen.Bounds.Width;
        private static int Height = Screen.PrimaryScreen.Bounds.Height;
        private static int width;
        private static int height;
        private static bool Getstate = false;
        public static bool closed = false;
        public static int group;
        public Control draggedPiece = null;
        public bool resizing = false;
        private Point startDraggingPoint;
        private Size startSize;
        public Rectangle rectProposedSize = Rectangle.Empty;
        public int resizingMargin = 5;
        public static Form1 form1 = (Form1)Application.OpenForms["Form1"];
        public static Image img;
        private FilterInfoCollection CaptureDevice;
        private VideoCaptureDevice FinalFrame;
        private VideoCapabilities[] videoCapabilities;
        private static int heightwebcam = 300, widthwebcam;
        private static bool getstate = false, resizingwebcam = false;
        private static double ratio, border;
        public static int[] wd = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static int[] wu = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static bool[] ws = { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
            ws[n] = val;
        }
        private void Form2_Shown(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            this.Location = new System.Drawing.Point(0, 0);
            this.Size = new System.Drawing.Size(Width, Height);
            this.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Parent = this;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(Width, Height);
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.TopMost = true;
            try
            {
                CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
                videoCapabilities = FinalFrame.VideoCapabilities;
                FinalFrame.VideoResolution = videoCapabilities[videoCapabilities.Length - 1];
                ratio = Convert.ToDouble(FinalFrame.VideoResolution.FrameSize.Width) / Convert.ToDouble(FinalFrame.VideoResolution.FrameSize.Height);
                heightwebcam = 300;
                widthwebcam = (int)(heightwebcam * ratio);
                this.pictureBox3.BackColor = Color.Transparent;
                this.pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                this.pictureBox3.Size = new Size((int)(heightwebcam * ratio), heightwebcam);
                this.pictureBox3.Location = new Point(0, 0);
                this.pictureBox3.Parent = pictureBox1;
                FinalFrame.SetCameraProperty(CameraControlProperty.Zoom, 600, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Focus, 0, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Exposure, 0, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Iris, 0, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Pan, 0, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Tilt, 0, CameraControlFlags.Manual);
                FinalFrame.SetCameraProperty(CameraControlProperty.Roll, 0, CameraControlFlags.Manual);
                FinalFrame.NewFrame += FinalFrame_NewFrame;
                FinalFrame.Start();
            }
            catch { }
        }
        void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                group = Form1.group;
                if (form1.g1cbwebcam.Checked & group == 1)
                {
                    if (!resizingwebcam)
                    {
                        this.pictureBox3.Visible = true;
                        this.pictureBox3.Location = new Point(Convert.ToInt32(form1.g1tbxwebcam.Text), Convert.ToInt32(form1.g1tbywebcam.Text));
                        heightwebcam = Convert.ToInt32(form1.g1tbheightwebcam.Text);
                        this.pictureBox3.Size = new Size((int)(heightwebcam * ratio), heightwebcam);
                    }
                    this.pictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
                }
                else if (form1.g2cbwebcam.Checked & group == 2)
                {
                    if (!resizingwebcam)
                    {
                        this.pictureBox3.Visible = true;
                        this.pictureBox3.Location = new Point(Convert.ToInt32(form1.g2tbxwebcam.Text), Convert.ToInt32(form1.g2tbywebcam.Text));
                        heightwebcam = Convert.ToInt32(form1.g2tbheightwebcam.Text);
                        this.pictureBox3.Size = new Size((int)(heightwebcam * ratio), heightwebcam);
                    }
                    this.pictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
                }
                else if (form1.g3cbwebcam.Checked & group == 3)
                {
                    if (!resizingwebcam)
                    {
                        this.pictureBox3.Visible = true;
                        this.pictureBox3.Location = new Point(Convert.ToInt32(form1.g3tbxwebcam.Text), Convert.ToInt32(form1.g3tbywebcam.Text));
                        heightwebcam = Convert.ToInt32(form1.g3tbheightwebcam.Text);
                        this.pictureBox3.Size = new Size((int)(heightwebcam * ratio), heightwebcam);
                    }
                    this.pictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
                }
                else if (form1.g4cbwebcam.Checked & group == 4)
                {
                    if (!resizingwebcam)
                    {
                        this.pictureBox3.Visible = true;
                        this.pictureBox3.Location = new Point(Convert.ToInt32(form1.g4tbxwebcam.Text), Convert.ToInt32(form1.g4tbywebcam.Text));
                        heightwebcam = Convert.ToInt32(form1.g4tbheightwebcam.Text);
                        this.pictureBox3.Size = new Size((int)(heightwebcam * ratio), heightwebcam);
                    }
                    this.pictureBox3.Image = (Bitmap)eventArgs.Frame.Clone();
                }
                else
                {
                    this.pictureBox3.Visible = false;
                }
            }
            catch { }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            valchanged(0, GetAsyncKeyState(Keys.F5));
            if (wd[0] == 1)
            {
                if (!Getstate)
                {
                    Getstate = true;
                    this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    Getstate = false;
                    this.WindowState = FormWindowState.Maximized;
                }
            }
        }
        public void SetBackgroundPicture(Bitmap EditableImg)
        {
            try
            {
                this.webView1.Destroy();
            }
            catch { }
            try
            {
                pictureBox1.Image = EditableImg;
            }
            catch { }
        }
        public void SetPicture(Bitmap EditableImg)
        {
            try
            {
                pictureBox2.Image = EditableImg;
            }
            catch { }
        }
        public void SetProcess(Bitmap EditableImg)
        {
            try
            {
                pictureBox2.Image = EditableImg;
            }
            catch { }
        }
        public void SetBrowser(string url)
        {
            try
            {
                InitBrowser();
                Navigate(url);
            }
            catch { }
        }
        public void SetVideo(string content)
        {
            try
            {
                InitBrowser();
                webView1.LoadHtml(content);
            }
            catch { }
        }
        public void SetPDF(string content)
        {
            try
            {
                InitBrowser();
                webView1.LoadHtml(content);
            }
            catch { }
        }
        public void InitBox(System.Drawing.Point point, System.Drawing.Size size)
        {
            this.pictureBox2.Parent = pictureBox1;
            this.pictureBox2.Location = point;
            this.pictureBox2.Size = size;
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            width = this.pictureBox2.Width;
            height = this.pictureBox2.Height;
        }
        public void InitBrowser()
        {
            try
            {
                EO.WebEngine.BrowserOptions options = new EO.WebEngine.BrowserOptions();
                options.EnableWebSecurity = false;
                EO.WebBrowser.Runtime.DefaultEngineOptions.SetDefaultBrowserOptions(options);
                EO.WebEngine.Engine.Default.Options.AllowProprietaryMediaFormats();
                EO.WebEngine.Engine.Default.Options.SetDefaultBrowserOptions(new EO.WebEngine.BrowserOptions
                {
                    EnableWebSecurity = false
                });
                this.webView1.Create(pictureBox2.Handle);
                this.webView1.Engine.Options.AllowProprietaryMediaFormats();
                this.webView1.SetOptions(new EO.WebEngine.BrowserOptions
                {
                    EnableWebSecurity = false
                });
                this.webView1.Engine.Options.DisableGPU = false;
                this.webView1.Engine.Options.DisableSpellChecker = true;
                this.webView1.Engine.Options.CustomUserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
                this.webView1.JSInitCode = @"setInterval(function(){ 
                try {
                    if (window.location.href.indexOf('youtube') > -1) {
                        document.cookie='VISITOR_INFO1_LIVE = oKckVSqvaGw; path =/; domain =.youtube.com';
                        var cookies = document.cookie.split('; ');
                        for (var i = 0; i < cookies.length; i++)
                        {
                            var cookie = cookies[i];
                            var eqPos = cookie.indexOf('=');
                            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
                            document.cookie = name + '=;expires=Thu, 01 Jan 1970 00:00:00 GMT';
                        }
                        var el = document.getElementsByClassName('ytp-ad-skip-button');
                        for (var i=0;i<el.length; i++) {
                            el[i].click();
                        }
                        var element = document.getElementsByClassName('ytp-ad-overlay-close-button');
                        for (var i=0;i<element.length; i++) {
                            element[i].click();
                        }
                    }
                }
                catch {}
            }, 5000);
            addScript('https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js');
            function addScript(src) {
                var s = document.createElement('script');
                s.setAttribute('src', src);
                document.body.appendChild(s);
            }
            $('body').on('click', 'img', function() {
                var source = this.src;
                var input = document.createElement('textarea');
                input.value = source;
                document.body.appendChild(input);
                input.select();
                document.execCommand('Copy');
                input.remove();
            });
            ";
            }
            catch { }
        }
        public void Navigate(string address)
        {
            if (String.IsNullOrEmpty(address))
                return;
            if (address.Equals("about:blank"))
                return;
            if (!address.StartsWith("http://") & !address.StartsWith("https://"))
                address = "https://" + address;
            try
            {
                webView1.Url = address;
            }
            catch (System.UriFormatException)
            {
                return;
            }
        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            draggedPiece = sender as Control;
            if ((e.X <= resizingMargin) || (e.X >= draggedPiece.Width - resizingMargin) || (e.Y <= resizingMargin) || (e.Y >= draggedPiece.Height - resizingMargin))
            {
                resizing = true;
                // indicate resizing
                this.Cursor = Cursors.SizeNWSE;
                // starting size
                this.startSize = new Size(e.X, e.Y);
                // get the location of the picture box
                Point pt = this.PointToScreen(draggedPiece.Location);
                rectProposedSize = new Rectangle(pt, startSize);
                // draw rect
                ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
            }
            else
            {
                resizing = false;
                // indicate moving
                this.Cursor = Cursors.SizeAll;
            }
            // start point location
            this.startDraggingPoint = e.Location;
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPiece != null)
            {
                if (resizing)
                {
                    // erase rect
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    // calculate rect new size
                    rectProposedSize.Width = e.X - this.startDraggingPoint.X + this.startSize.Width;
                    rectProposedSize.Height = e.Y - this.startDraggingPoint.Y + this.startSize.Height;
                    // draw rect
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    Point pt;
                    if (draggedPiece == sender)
                        pt = new Point(e.X, e.Y);
                    else
                        pt = draggedPiece.PointToClient((sender as Control).PointToScreen(new Point(e.X, e.Y)));
                    draggedPiece.Left += pt.X - this.startDraggingPoint.X;
                    draggedPiece.Top += pt.Y - this.startDraggingPoint.Y;
                }
            }
        }
        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizing)
            {
                if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                {
                    // erase rect
                    ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                // compare to min width and size ?
                if (rectProposedSize.Width > 10 && rectProposedSize.Height > 10)
                {
                    // set size 
                    this.draggedPiece.Size = rectProposedSize.Size;
                }
                else
                {
                    // you might want to set some minimal size here
                    this.draggedPiece.Size = new Size((int)Math.Max(10, rectProposedSize.Width), Math.Max(10, rectProposedSize.Height));
                }
            }
            Point point = this.draggedPiece.Location;
            Size size = this.draggedPiece.Size;
            string x = point.X.ToString();
            string y = point.Y.ToString();
            string width = size.Width.ToString();
            string height = size.Height.ToString();
            group = Form1.group;
            if (group == 1)
            {
                form1.g1tbx.Text = x;
                form1.g1tby.Text = y;
                form1.g1tbwidth.Text = width;
                form1.g1tbheight.Text = height;
            }
            else if (group == 2)
            {
                form1.g2tbx.Text = x;
                form1.g2tby.Text = y;
                form1.g2tbwidth.Text = width;
                form1.g2tbheight.Text = height;
            }
            else if (group == 3)
            {
                form1.g3tbx.Text = x;
                form1.g3tby.Text = y;
                form1.g3tbwidth.Text = width;
                form1.g3tbheight.Text = height;
            }
            else if (group == 4)
            {
                form1.g4tbx.Text = x;
                form1.g4tby.Text = y;
                form1.g4tbwidth.Text = width;
                form1.g4tbheight.Text = height;
            }
            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            this.Cursor = Cursors.Default;
            this.pictureBox2.BringToFront();
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            resizingwebcam = true;
            draggedPiece = sender as Control;
            if ((e.X <= resizingMargin) || (e.X >= draggedPiece.Width - resizingMargin) || (e.Y <= resizingMargin) || (e.Y >= draggedPiece.Height - resizingMargin))
            {
                resizing = true;
                // indicate resizing
                this.Cursor = Cursors.SizeNWSE;
                // starting size
                this.startSize = new Size(e.X, e.Y);
                // get the location of the picture box
                Point pt = this.PointToScreen(draggedPiece.Location);
                rectProposedSize = new Rectangle(pt, startSize);
                // draw rect
                ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
            }
            else
            {
                resizing = false;
                // indicate moving
                this.Cursor = Cursors.SizeAll;
            }
            // start point location
            this.startDraggingPoint = e.Location;
        }
        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (draggedPiece != null)
            {
                if (resizing)
                {
                    // erase rect
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                    // calculate rect new size
                    rectProposedSize.Width = e.X - this.startDraggingPoint.X + this.startSize.Width;
                    rectProposedSize.Height = e.Y - this.startDraggingPoint.Y + this.startSize.Height;
                    // draw rect
                    if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                        ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                else
                {
                    Point pt;
                    if (draggedPiece == sender)
                        pt = new Point(e.X, e.Y);
                    else
                        pt = draggedPiece.PointToClient((sender as Control).PointToScreen(new Point(e.X, e.Y)));
                    draggedPiece.Left += pt.X - this.startDraggingPoint.X;
                    draggedPiece.Top += pt.Y - this.startDraggingPoint.Y;
                }
            }
        }
        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            if (resizing)
            {
                if (rectProposedSize.Width > 0 && rectProposedSize.Height > 0)
                {
                    // erase rect
                    ControlPaint.DrawReversibleFrame(rectProposedSize, this.ForeColor, FrameStyle.Dashed);
                }
                // compare to min width and size ?
                if (rectProposedSize.Width > 10 && rectProposedSize.Height > 10)
                {
                    // set size 
                    this.draggedPiece.Size = rectProposedSize.Size;
                }
                else
                {
                    // you might want to set some minimal size here
                    this.draggedPiece.Size = new Size((int)Math.Max(10, rectProposedSize.Width), Math.Max(10, rectProposedSize.Height));
                }
            }
            Point point = this.draggedPiece.Location;
            Size size = this.draggedPiece.Size;
            string x = point.X.ToString();
            string y = point.Y.ToString();
            string width = size.Width.ToString();
            string height = size.Height.ToString();
            group = Form1.group;
            if (group == 1)
            {
                form1.g1tbxwebcam.Text = x;
                form1.g1tbywebcam.Text = y;
                form1.g1tbwidthwebcam.Text = width;
                form1.g1tbheightwebcam.Text = height;
            }
            else if (group == 2)
            {
                form1.g2tbxwebcam.Text = x;
                form1.g2tbywebcam.Text = y;
                form1.g2tbwidthwebcam.Text = width;
                form1.g2tbheightwebcam.Text = height;
            }
            else if (group == 3)
            {
                form1.g3tbxwebcam.Text = x;
                form1.g3tbywebcam.Text = y;
                form1.g3tbwidthwebcam.Text = width;
                form1.g3tbheightwebcam.Text = height;
            }
            else if (group == 4)
            {
                form1.g4tbxwebcam.Text = x;
                form1.g4tbywebcam.Text = y;
                form1.g4tbwidthwebcam.Text = width;
                form1.g4tbheightwebcam.Text = height;
            }
            this.draggedPiece = null;
            this.startDraggingPoint = Point.Empty;
            this.Cursor = Cursors.Default; 
            resizingwebcam = false;
            this.pictureBox3.BringToFront();
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
            try
            {
                FinalFrame.NewFrame -= FinalFrame_NewFrame;
                System.Threading.Thread.Sleep(1000);
                if (FinalFrame.IsRunning)
                    FinalFrame.Stop();
            }
            catch { }
        }
    }
}