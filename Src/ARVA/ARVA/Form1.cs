using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NAudio.Wave;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using NAudio.Extras;
using System.Drawing.Imaging;
using NAudio.Utils;
using System.Threading;
using System.Diagnostics;
namespace ARVA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        private bool closed = false, audiogetstate = false;
        private static MediaFoundationReader audioFileReader;
        private static IWavePlayer waveOutDevice;
        public static Form2 form2 = new Form2();
        public static Bitmap img, EditableImg;
        private static IntPtr handle;
        private static string savedfilename = "";
        public static int group;
        private void Form1_Shown(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            form2.Show();
        }
        private void Start(IntPtr process)
        {
            while (!closed)
            {
                form2.SetProcess(PrintWindow(handle));
                System.Threading.Thread.Sleep(40);
            }
        }
        private void g1btfont_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbfont.Text = op.FileName;
            }
        }
        private void g1btpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbpicture.Text = op.FileName;
            }
        }
        private void g1btvideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbvideo.Text = op.FileName;
            }
        }
        private void g1btpage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbpage.Text = op.FileName;
            }
        }
        private void g1btprocess_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbprocess.Text = op.FileName;
            }
        }
        private void g1btpdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbpdf.Text = op.FileName;
            }
        }
        private void g1btsong1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbsong1.Text = op.FileName;
            }
        }
        private void g1btsong2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbsong2.Text = op.FileName;
            }
        }
        private void g1btsong3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbsong3.Text = op.FileName;
            }
        }
        private void g1btsong4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g1tbsong4.Text = op.FileName;
            }
        }
        private void g2btfont_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbfont.Text = op.FileName;
            }
        }
        private void g2btpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbpicture.Text = op.FileName;
            }
        }
        private void g2btvideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbvideo.Text = op.FileName;
            }
        }
        private void g2btpage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbpage.Text = op.FileName;
            }
        }
        private void g2btprocess_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbprocess.Text = op.FileName;
            }
        }
        private void g2btpdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbpdf.Text = op.FileName;
            }
        }
        private void g2btsong1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbsong1.Text = op.FileName;
            }
        }
        private void g2btsong2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbsong2.Text = op.FileName;
            }
        }
        private void g2btsong3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbsong3.Text = op.FileName;
            }
        }
        private void g2btsong4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g2tbsong4.Text = op.FileName;
            }
        }
        private void g3btfont_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbfont.Text = op.FileName;
            }
        }
        private void g3btpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbpicture.Text = op.FileName;
            }
        }
        private void g3btvideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbvideo.Text = op.FileName;
            }
        }
        private void g3btpage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbpage.Text = op.FileName;
            }
        }
        private void g3btprocess_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbprocess.Text = op.FileName;
            }
        }
        private void g3btpdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbpdf.Text = op.FileName;
            }
        }
        private void g3btsong1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbsong1.Text = op.FileName;
            }
        }
        private void g3btsong2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbsong2.Text = op.FileName;
            }
        }
        private void g3btsong3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbsong3.Text = op.FileName;
            }
        }
        private void g3btsong4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g3tbsong4.Text = op.FileName;
            }
        }
        private void g4btfont_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbfont.Text = op.FileName;
            }
        }
        private void g4btpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbpicture.Text = op.FileName;
            }
        }
        private void g4btvideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbvideo.Text = op.FileName;
            }
        }
        private void g4btpage_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbpage.Text = op.FileName;
            }
        }
        private void g4btprocess_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbprocess.Text = op.FileName;
            }
        }
        private void g4btpdf_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbpdf.Text = op.FileName;
            }
        }
        private void g4btsong1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbsong1.Text = op.FileName;
            }
        }
        private void g4btsong2_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbsong2.Text = op.FileName;
            }
        }
        private void g4btsong3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbsong3.Text = op.FileName;
            }
        }
        private void g4btsong4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                g4tbsong4.Text = op.FileName;
            }
        }
        private void g1cbpicture_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbpicture.Checked)
            {
                g1cbvideo.Checked = false;
                g1cbpage.Checked = false;
                g1cbprocess.Checked = false;
                g1cbpdf.Checked = false;
            }
        }
        private void g1cbvideo_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbvideo.Checked)
            {
                g1cbpicture.Checked = false;
                g1cbpage.Checked = false;
                g1cbprocess.Checked = false;
                g1cbpdf.Checked = false;
            }
        }
        private void g1cbpage_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbpage.Checked)
            {
                g1cbpicture.Checked = false;
                g1cbvideo.Checked = false;
                g1cbprocess.Checked = false;
                g1cbpdf.Checked = false;
            }
        }
        private void g1cbprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbprocess.Checked)
            {
                g1cbpicture.Checked = false;
                g1cbvideo.Checked = false;
                g1cbpage.Checked = false;
                g1cbpdf.Checked = false;
            }
        }
        private void g1cbpdf_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbpdf.Checked)
            {
                g1cbpicture.Checked = false;
                g1cbvideo.Checked = false;
                g1cbpage.Checked = false;
                g1cbprocess.Checked = false;
            }
        }
        private void g1cbsong1_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbsong1.Checked)
            {
                g1cbsong2.Checked = false;
                g1cbsong3.Checked = false;
                g1cbsong4.Checked = false;
            }
        }
        private void g1cbsong2_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbsong2.Checked)
            {
                g1cbsong1.Checked = false;
                g1cbsong3.Checked = false;
                g1cbsong4.Checked = false;
            }
        }
        private void g1cbsong3_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbsong3.Checked)
            {
                g1cbsong1.Checked = false;
                g1cbsong2.Checked = false;
                g1cbsong4.Checked = false;
            }
        }
        private void g1cbsong4_CheckedChanged(object sender, EventArgs e)
        {
            if (g1cbsong4.Checked)
            {
                g1cbsong1.Checked = false;
                g1cbsong2.Checked = false;
                g1cbsong3.Checked = false;
            }
        }
        private void g2cbpicture_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbpicture.Checked)
            {
                g2cbvideo.Checked = false;
                g2cbpage.Checked = false;
                g2cbprocess.Checked = false;
                g2cbpdf.Checked = false;
            }
        }
        private void g2cbvideo_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbvideo.Checked)
            {
                g2cbpicture.Checked = false;
                g2cbpage.Checked = false;
                g2cbprocess.Checked = false;
                g2cbpdf.Checked = false;
            }
        }
        private void g2cbpage_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbpage.Checked)
            {
                g2cbpicture.Checked = false;
                g2cbvideo.Checked = false;
                g2cbprocess.Checked = false;
                g2cbpdf.Checked = false;
            }
        }
        private void g2cbprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbprocess.Checked)
            {
                g2cbpicture.Checked = false;
                g2cbvideo.Checked = false;
                g2cbpage.Checked = false;
                g2cbpdf.Checked = false;
            }
        }
        private void g2cbpdf_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbpdf.Checked)
            {
                g2cbpicture.Checked = false;
                g2cbvideo.Checked = false;
                g2cbpage.Checked = false;
                g2cbprocess.Checked = false;
            }
        }
        private void g2cbsong1_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbsong1.Checked)
            {
                g2cbsong2.Checked = false;
                g2cbsong3.Checked = false;
                g2cbsong4.Checked = false;
            }
        }
        private void g2cbsong2_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbsong2.Checked)
            {
                g2cbsong1.Checked = false;
                g2cbsong3.Checked = false;
                g2cbsong4.Checked = false;
            }
        }
        private void g2cbsong3_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbsong3.Checked)
            {
                g2cbsong1.Checked = false;
                g2cbsong2.Checked = false;
                g2cbsong4.Checked = false;
            }
        }
        private void g2cbsong4_CheckedChanged(object sender, EventArgs e)
        {
            if (g2cbsong4.Checked)
            {
                g2cbsong1.Checked = false;
                g2cbsong2.Checked = false;
                g2cbsong3.Checked = false;
            }
        }
        private void g3cbpicture_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbpicture.Checked)
            {
                g3cbvideo.Checked = false;
                g3cbpage.Checked = false;
                g3cbprocess.Checked = false;
                g3cbpdf.Checked = false;
            }
        }
        private void g3cbvideo_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbvideo.Checked)
            {
                g3cbpicture.Checked = false;
                g3cbpage.Checked = false;
                g3cbprocess.Checked = false;
                g3cbpdf.Checked = false;
            }
        }
        private void g3cbpage_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbpage.Checked)
            {
                g3cbpicture.Checked = false;
                g3cbvideo.Checked = false;
                g3cbprocess.Checked = false;
                g3cbpdf.Checked = false;
            }
        }
        private void g3cbprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbprocess.Checked)
            {
                g3cbpicture.Checked = false;
                g3cbvideo.Checked = false;
                g3cbpage.Checked = false;
                g3cbpdf.Checked = false;
            }
        }
        private void g3cbpdf_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbpdf.Checked)
            {
                g3cbpicture.Checked = false;
                g3cbvideo.Checked = false;
                g3cbpage.Checked = false;
                g3cbprocess.Checked = false;
            }
        }
        private void g3cbsong1_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbsong1.Checked)
            {
                g3cbsong2.Checked = false;
                g3cbsong3.Checked = false;
                g3cbsong4.Checked = false;
            }
        }
        private void g3cbsong2_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbsong2.Checked)
            {
                g3cbsong1.Checked = false;
                g3cbsong3.Checked = false;
                g3cbsong4.Checked = false;
            }
        }
        private void g3cbsong3_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbsong3.Checked)
            {
                g3cbsong1.Checked = false;
                g3cbsong2.Checked = false;
                g3cbsong4.Checked = false;
            }
        }
        private void g3cbsong4_CheckedChanged(object sender, EventArgs e)
        {
            if (g3cbsong4.Checked)
            {
                g3cbsong1.Checked = false;
                g3cbsong2.Checked = false;
                g3cbsong3.Checked = false;
            }
        }
        private void g4cbpicture_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbpicture.Checked)
            {
                g4cbvideo.Checked = false;
                g4cbpage.Checked = false;
                g4cbprocess.Checked = false;
                g4cbpdf.Checked = false;
            }
        }
        private void g4cbvideo_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbvideo.Checked)
            {
                g4cbpicture.Checked = false;
                g4cbpage.Checked = false;
                g4cbprocess.Checked = false;
                g4cbpdf.Checked = false;
            }
        }
        private void g4cbpage_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbpage.Checked)
            {
                g4cbpicture.Checked = false;
                g4cbvideo.Checked = false;
                g4cbprocess.Checked = false;
                g4cbpdf.Checked = false;
            }
        }
        private void g4cbprocess_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbprocess.Checked)
            {
                g4cbpicture.Checked = false;
                g4cbvideo.Checked = false;
                g4cbpage.Checked = false;
                g4cbpdf.Checked = false;
            }
        }
        private void g4cbpdf_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbpdf.Checked)
            {
                g4cbpicture.Checked = false;
                g4cbvideo.Checked = false;
                g4cbpage.Checked = false;
                g4cbprocess.Checked = false;
            }
        }
        private void g4cbsong1_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbsong1.Checked)
            {
                g4cbsong2.Checked = false;
                g4cbsong3.Checked = false;
                g4cbsong4.Checked = false;
            }
        }
        private void g4cbsong2_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbsong2.Checked)
            {
                g4cbsong1.Checked = false;
                g4cbsong3.Checked = false;
                g4cbsong4.Checked = false;
            }
        }
        private void g4cbsong3_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbsong3.Checked)
            {
                g4cbsong1.Checked = false;
                g4cbsong2.Checked = false;
                g4cbsong4.Checked = false;
            }
        }
        private void g4cbsong4_CheckedChanged(object sender, EventArgs e)
        {
            if (g4cbsong4.Checked)
            {
                g4cbsong1.Checked = false;
                g4cbsong2.Checked = false;
                g4cbsong3.Checked = false;
            }
        }
        private void g1btplay_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (g1cbsong1.Checked)
            {
                filename = g1tbsong1.Text;
            }
            else if (g1cbsong2.Checked)
            {
                filename = g1tbsong2.Text;
            }
            else if (g1cbsong3.Checked)
            {
                filename = g1tbsong3.Text;
            }
            else if (g1cbsong4.Checked)
            {
                filename = g1tbsong4.Text;
            }
            Play(filename);
        }
        private void g2btplay_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (g2cbsong1.Checked)
            {
                filename = g2tbsong1.Text;
            }
            else if (g2cbsong2.Checked)
            {
                filename = g2tbsong2.Text;
            }
            else if (g2cbsong3.Checked)
            {
                filename = g2tbsong3.Text;
            }
            else if (g2cbsong4.Checked)
            {
                filename = g2tbsong4.Text;
            }
            Play(filename);
        }
        private void g3btplay_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (g3cbsong1.Checked)
            {
                filename = g3tbsong1.Text;
            }
            else if (g3cbsong2.Checked)
            {
                filename = g3tbsong2.Text;
            }
            else if (g3cbsong3.Checked)
            {
                filename = g3tbsong3.Text;
            }
            else if (g3cbsong4.Checked)
            {
                filename = g3tbsong4.Text;
            }
            Play(filename);
        }
        private void g4btplay_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (g4cbsong1.Checked)
            {
                filename = g4tbsong1.Text;
            }
            else if (g4cbsong2.Checked)
            {
                filename = g4tbsong2.Text;
            }
            else if (g4cbsong3.Checked)
            {
                filename = g4tbsong3.Text;
            }
            else if (g4cbsong4.Checked)
            {
                filename = g4tbsong4.Text;
            }
            Play(filename);
        }
        private void Play(string filename)
        {
            if (!audiogetstate)
            {
                try
                {
                    waveOutDevice = new NAudio.Wave.WaveOut();
                    audioFileReader = new MediaFoundationReader(filename);
                    waveOutDevice.Init(audioFileReader);
                    waveOutDevice.Play();
                    waveOutDevice.PlaybackStopped += WaveOutDevice_PlaybackStopped;
                }
                catch { }
                audiogetstate = true;
            }
            else if (audiogetstate)
            {
                try
                {
                    waveOutDevice.Stop();
                    audioFileReader.Dispose();
                    waveOutDevice.Dispose();
                }
                catch { }
                audiogetstate = false;
            }
        }
        private void WaveOutDevice_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            audiogetstate = false;
        }
        private void g1btdisplay_Click(object sender, EventArgs e)
        {
            group = 1;
            closed = true;
            System.Threading.Thread.Sleep(100);
            closed = false;
            try
            {
                form2.SetBackgroundPicture(Bitmap.FromFile(g1tbfont.Text) as Bitmap);
            }
            catch { }
            if (g1cbpicture.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g1tbx.Text), Convert.ToInt32(g1tby.Text)), new System.Drawing.Size(Convert.ToInt32(g1tbwidth.Text), Convert.ToInt32(g1tbheight.Text)));
                    form2.SetPicture(Bitmap.FromFile(g1tbpicture.Text) as Bitmap);
                }
                catch { }
            }
            else if (g1cbprocess.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g1tbx.Text), Convert.ToInt32(g1tby.Text)), new System.Drawing.Size(Convert.ToInt32(g1tbwidth.Text), Convert.ToInt32(g1tbheight.Text)));
                    var processes = Process.GetProcessesByName(g1tbprocess.Text);
                    if (processes.Length > 0)
                    {
                        handle = processes[0].MainWindowHandle;
                        Task.Run(() => Start(handle));
                    }
                }
                catch { }
            }
            else if (g1cbpage.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g1tbx.Text), Convert.ToInt32(g1tby.Text)), new System.Drawing.Size(Convert.ToInt32(g1tbwidth.Text), Convert.ToInt32(g1tbheight.Text)));
                    form2.SetBrowser(g1tbpage.Text);
                }
                catch { }
            }
            else if (g1cbvideo.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g1tbx.Text), Convert.ToInt32(g1tby.Text)), new System.Drawing.Size(Convert.ToInt32(g1tbwidth.Text), Convert.ToInt32(g1tbheight.Text)));
                    string videofilename = @"file:///" + g1tbvideo.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; }</style></head><body><video controls width='400'><source src='output.mp4' type='video/mp4'></video></body></html>".Replace("400", g1tbwidth.Text).Replace("output.mp4", videofilename);
                    form2.SetVideo(content);
                }
                catch { }
            }
            else if (g1cbpdf.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g1tbx.Text), Convert.ToInt32(g1tby.Text)), new System.Drawing.Size(Convert.ToInt32(g1tbwidth.Text), Convert.ToInt32(g1tbheight.Text)));
                    string pdffilename = @"file:///" + g1tbpdf.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; border: none; }</style></head><body><object data='SCD_T_2011_0054_FRANIATTE.pdf' type='application/pdf' style='min-height:100vh;width:100%;'></object></body></html>".Replace("SCD_T_2011_0054_FRANIATTE.pdf", pdffilename);
                    form2.SetPDF(content);
                }
                catch { }
            }
        }
        private void g2btdisplay_Click(object sender, EventArgs e)
        {
            group = 2;
            closed = true;
            System.Threading.Thread.Sleep(100);
            closed = false;
            try
            {
                form2.SetBackgroundPicture(Bitmap.FromFile(g2tbfont.Text) as Bitmap);
            }
            catch { }
            if (g2cbpicture.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g2tbx.Text), Convert.ToInt32(g2tby.Text)), new System.Drawing.Size(Convert.ToInt32(g2tbwidth.Text), Convert.ToInt32(g2tbheight.Text)));
                    form2.SetPicture(Bitmap.FromFile(g2tbpicture.Text) as Bitmap);
                }
                catch { }
            }
            else if (g2cbprocess.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g2tbx.Text), Convert.ToInt32(g2tby.Text)), new System.Drawing.Size(Convert.ToInt32(g2tbwidth.Text), Convert.ToInt32(g2tbheight.Text)));
                    var processes = Process.GetProcessesByName(g2tbprocess.Text);
                    if (processes.Length > 0)
                    {
                        handle = processes[0].MainWindowHandle;
                        Task.Run(() => Start(handle));
                    }
                }
                catch { }
            }
            else if (g2cbpage.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g2tbx.Text), Convert.ToInt32(g2tby.Text)), new System.Drawing.Size(Convert.ToInt32(g2tbwidth.Text), Convert.ToInt32(g2tbheight.Text)));
                    form2.SetBrowser(g2tbpage.Text);
                }
                catch { }
            }
            else if (g2cbvideo.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g2tbx.Text), Convert.ToInt32(g2tby.Text)), new System.Drawing.Size(Convert.ToInt32(g2tbwidth.Text), Convert.ToInt32(g2tbheight.Text)));
                    string videofilename = @"file:///" + g2tbvideo.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; }</style></head><body><video controls width='400'><source src='output.mp4' type='video/mp4'></video></body></html>".Replace("400", g2tbwidth.Text).Replace("output.mp4", videofilename);
                    form2.SetVideo(content);
                }
                catch { }
            }
            else if (g2cbpdf.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g2tbx.Text), Convert.ToInt32(g2tby.Text)), new System.Drawing.Size(Convert.ToInt32(g2tbwidth.Text), Convert.ToInt32(g2tbheight.Text)));
                    string pdffilename = @"file:///" + g2tbpdf.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; border: none; }</style></head><body><object data='SCD_T_2011_0054_FRANIATTE.pdf' type='application/pdf' style='min-height:100vh;width:100%;'></object></body></html>".Replace("SCD_T_2011_0054_FRANIATTE.pdf", pdffilename);
                    form2.SetPDF(content);
                }
                catch { }
            }
        }
        private void g3btdisplay_Click(object sender, EventArgs e)
        {
            group = 3;
            closed = true;
            System.Threading.Thread.Sleep(100);
            closed = false;
            try
            {
                form2.SetBackgroundPicture(Bitmap.FromFile(g3tbfont.Text) as Bitmap);
            }
            catch { }
            if (g3cbpicture.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g3tbx.Text), Convert.ToInt32(g3tby.Text)), new System.Drawing.Size(Convert.ToInt32(g3tbwidth.Text), Convert.ToInt32(g3tbheight.Text)));
                    form2.SetPicture(Bitmap.FromFile(g3tbpicture.Text) as Bitmap);
                }
                catch { }
            }
            else if (g3cbprocess.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g3tbx.Text), Convert.ToInt32(g3tby.Text)), new System.Drawing.Size(Convert.ToInt32(g3tbwidth.Text), Convert.ToInt32(g3tbheight.Text)));
                    var processes = Process.GetProcessesByName(g3tbprocess.Text);
                    if (processes.Length > 0)
                    {
                        handle = processes[0].MainWindowHandle;
                        Task.Run(() => Start(handle));
                    }
                }
                catch { }
            }
            else if (g3cbpage.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g3tbx.Text), Convert.ToInt32(g3tby.Text)), new System.Drawing.Size(Convert.ToInt32(g3tbwidth.Text), Convert.ToInt32(g3tbheight.Text)));
                    form2.SetBrowser(g3tbpage.Text);
                }
                catch { }
            }
            else if (g3cbvideo.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g3tbx.Text), Convert.ToInt32(g3tby.Text)), new System.Drawing.Size(Convert.ToInt32(g3tbwidth.Text), Convert.ToInt32(g3tbheight.Text)));
                    string videofilename = @"file:///" + g3tbvideo.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; }</style></head><body><video controls width='400'><source src='output.mp4' type='video/mp4'></video></body></html>".Replace("400", g3tbwidth.Text).Replace("output.mp4", videofilename);
                    form2.SetVideo(content);
                }
                catch { }
            }
            else if (g3cbpdf.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g3tbx.Text), Convert.ToInt32(g3tby.Text)), new System.Drawing.Size(Convert.ToInt32(g3tbwidth.Text), Convert.ToInt32(g3tbheight.Text)));
                    string pdffilename = @"file:///" + g3tbpdf.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; border: none; }</style></head><body><object data='SCD_T_2011_0054_FRANIATTE.pdf' type='application/pdf' style='min-height:100vh;width:100%;'></object></body></html>".Replace("SCD_T_2011_0054_FRANIATTE.pdf", pdffilename);
                    form2.SetPDF(content);
                }
                catch { }
            }
        }
        private void g4btdisplay_Click(object sender, EventArgs e)
        {
            group = 4;
            closed = true;
            System.Threading.Thread.Sleep(100);
            closed = false;
            try
            {
                form2.SetBackgroundPicture(Bitmap.FromFile(g4tbfont.Text) as Bitmap);
            }
            catch { }
            if (g4cbpicture.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g4tbx.Text), Convert.ToInt32(g4tby.Text)), new System.Drawing.Size(Convert.ToInt32(g4tbwidth.Text), Convert.ToInt32(g4tbheight.Text)));
                    form2.SetPicture(Bitmap.FromFile(g4tbpicture.Text) as Bitmap);
                }
                catch { }
            }
            else if (g4cbprocess.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g4tbx.Text), Convert.ToInt32(g4tby.Text)), new System.Drawing.Size(Convert.ToInt32(g4tbwidth.Text), Convert.ToInt32(g4tbheight.Text)));
                    var processes = Process.GetProcessesByName(g4tbprocess.Text);
                    if (processes.Length > 0)
                    {
                        handle = processes[0].MainWindowHandle;
                        Task.Run(() => Start(handle));
                    }
                }
                catch { }
            }
            else if (g4cbpage.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g4tbx.Text), Convert.ToInt32(g4tby.Text)), new System.Drawing.Size(Convert.ToInt32(g4tbwidth.Text), Convert.ToInt32(g4tbheight.Text)));
                    form2.SetBrowser(g4tbpage.Text);
                }
                catch { }
            }
            else if (g4cbvideo.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g4tbx.Text), Convert.ToInt32(g4tby.Text)), new System.Drawing.Size(Convert.ToInt32(g4tbwidth.Text), Convert.ToInt32(g4tbheight.Text)));
                    string videofilename = @"file:///" + g4tbvideo.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; }</style></head><body><video controls width='400'><source src='output.mp4' type='video/mp4'></video></body></html>".Replace("400", g4tbwidth.Text).Replace("output.mp4", videofilename);
                    form2.SetVideo(content);
                }
                catch { }
            }
            else if (g4cbpdf.Checked)
            {
                try
                {
                    form2.InitBox(new System.Drawing.Point(Convert.ToInt32(g4tbx.Text), Convert.ToInt32(g4tby.Text)), new System.Drawing.Size(Convert.ToInt32(g4tbwidth.Text), Convert.ToInt32(g4tbheight.Text)));
                    string pdffilename = @"file:///" + g4tbpdf.Text.Replace(@"\", @"/");
                    string content = @"<!DOCTYPE html><html><head><style>body { overflow: hidden; margin: 0; padding: 0; border: none; }</style></head><body><object data='SCD_T_2011_0054_FRANIATTE.pdf' type='application/pdf' style='min-height:100vh;width:100%;'></object></body></html>".Replace("SCD_T_2011_0054_FRANIATTE.pdf", pdffilename);
                    form2.SetPDF(content);
                }
                catch { }
            }
        }
        public Bitmap PrintWindow(IntPtr hwnd)
        {
            RECT rc;
            GetWindowRect(hwnd, out rc);
            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();
            PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }
        private void btnew_Click(object sender, EventArgs e)
        {
            savedfilename = "";
            g1tbfont.Text = "";
            g1tbpicture.Text = "";
            g1tbvideo.Text = "";
            g1tbpage.Text = "";
            g1tbprocess.Text = "";
            g1tbsong1.Text = "";
            g1tbsong2.Text = "";
            g1tbsong3.Text = "";
            g1tbsong4.Text = "";
            g1cbpicture.Checked = false;
            g1cbvideo.Checked = false;
            g1cbpage.Checked = false;
            g1cbprocess.Checked = false;
            g1cbsong1.Checked = false;
            g1cbsong2.Checked = false;
            g1cbsong3.Checked = false;
            g1cbsong4.Checked = false;
            g2tbfont.Text = "";
            g2tbpicture.Text = "";
            g2tbvideo.Text = "";
            g2tbpage.Text = "";
            g2tbprocess.Text = "";
            g2tbsong1.Text = "";
            g2tbsong2.Text = "";
            g2tbsong3.Text = "";
            g2tbsong4.Text = "";
            g2cbpicture.Checked = false;
            g2cbvideo.Checked = false;
            g2cbpage.Checked = false;
            g2cbprocess.Checked = false;
            g2cbsong1.Checked = false;
            g2cbsong2.Checked = false;
            g2cbsong3.Checked = false;
            g2cbsong4.Checked = false;
            g3tbfont.Text = "";
            g3tbpicture.Text = "";
            g3tbvideo.Text = "";
            g3tbpage.Text = "";
            g3tbprocess.Text = "";
            g3tbsong1.Text = "";
            g3tbsong2.Text = "";
            g3tbsong3.Text = "";
            g3tbsong4.Text = "";
            g3cbpicture.Checked = false;
            g3cbvideo.Checked = false;
            g3cbpage.Checked = false;
            g3cbprocess.Checked = false;
            g3cbsong1.Checked = false;
            g3cbsong2.Checked = false;
            g3cbsong3.Checked = false;
            g3cbsong4.Checked = false;
            g4tbfont.Text = "";
            g4tbpicture.Text = "";
            g4tbvideo.Text = "";
            g4tbpage.Text = "";
            g4tbprocess.Text = "";
            g4tbsong1.Text = "";
            g4tbsong2.Text = "";
            g4tbsong3.Text = "";
            g4tbsong4.Text = "";
            g4cbpicture.Checked = false;
            g4cbvideo.Checked = false;
            g4cbpage.Checked = false;
            g4cbprocess.Checked = false;
            g4cbsong1.Checked = false;
            g4cbsong2.Checked = false;
            g4cbsong3.Checked = false;
            g4cbsong4.Checked = false;
            g1tbx.Text = "";
            g1tby.Text = "";
            g1tbwidth.Text = "";
            g1tbheight.Text = "";
            g2tbx.Text = "";
            g2tby.Text = "";
            g2tbwidth.Text = "";
            g2tbheight.Text = "";
            g3tbx.Text = "";
            g3tby.Text = "";
            g3tbwidth.Text = "";
            g3tbheight.Text = "";
            g4tbx.Text = "";
            g4tby.Text = "";
            g4tbwidth.Text = "";
            g4tbheight.Text = "";
            g1tbpdf.Text = "";
            g1cbpdf.Checked = false;
            g2tbpdf.Text = "";
            g2cbpdf.Checked = false;
            g3tbpdf.Text = "";
            g3cbpdf.Checked = false;
            g4tbpdf.Text = "";
            g4cbpdf.Checked = false;
            g1cbwebcam.Checked = false;
            g1tbxwebcam.Text = "";
            g1tbywebcam.Text = "";
            g1tbwidthwebcam.Text = "";
            g1tbheightwebcam.Text = "";
            g2cbwebcam.Checked = false;
            g2tbxwebcam.Text = "";
            g2tbywebcam.Text = "";
            g2tbwidthwebcam.Text = "";
            g2tbheightwebcam.Text = "";
            g3cbwebcam.Checked = false;
            g3tbxwebcam.Text = "";
            g3tbywebcam.Text = "";
            g3tbwidthwebcam.Text = "";
            g3tbheightwebcam.Text = "";
            g4cbwebcam.Checked = false;
            g4tbxwebcam.Text = "";
            g4tbywebcam.Text = "";
            g4tbwidthwebcam.Text = "";
            g4tbheightwebcam.Text = "";
        }
        private void btopen_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "All Files(*.*)|*.*";
            if (op.ShowDialog() == DialogResult.OK)
            {
                savedfilename = op.FileName;
                this.Text = op.FileName;
                using (System.IO.StreamReader file = new System.IO.StreamReader(savedfilename))
                {
                    g1tbfont.Text = file.ReadLine();
                    g1tbpicture.Text = file.ReadLine();
                    g1tbvideo.Text = file.ReadLine();
                    g1tbpage.Text = file.ReadLine();
                    g1tbprocess.Text = file.ReadLine();
                    g1tbsong1.Text = file.ReadLine();
                    g1tbsong2.Text = file.ReadLine();
                    g1tbsong3.Text = file.ReadLine();
                    g1tbsong4.Text = file.ReadLine();
                    g1cbpicture.Checked = bool.Parse(file.ReadLine());
                    g1cbvideo.Checked = bool.Parse(file.ReadLine());
                    g1cbpage.Checked = bool.Parse(file.ReadLine());
                    g1cbprocess.Checked = bool.Parse(file.ReadLine());
                    g1cbsong1.Checked = bool.Parse(file.ReadLine());
                    g1cbsong2.Checked = bool.Parse(file.ReadLine());
                    g1cbsong3.Checked = bool.Parse(file.ReadLine());
                    g1cbsong4.Checked = bool.Parse(file.ReadLine());
                    g2tbfont.Text = file.ReadLine();
                    g2tbpicture.Text = file.ReadLine();
                    g2tbvideo.Text = file.ReadLine();
                    g2tbpage.Text = file.ReadLine();
                    g2tbprocess.Text = file.ReadLine();
                    g2tbsong1.Text = file.ReadLine();
                    g2tbsong2.Text = file.ReadLine();
                    g2tbsong3.Text = file.ReadLine();
                    g2tbsong4.Text = file.ReadLine();
                    g2cbpicture.Checked = bool.Parse(file.ReadLine());
                    g2cbvideo.Checked = bool.Parse(file.ReadLine());
                    g2cbpage.Checked = bool.Parse(file.ReadLine());
                    g2cbprocess.Checked = bool.Parse(file.ReadLine());
                    g2cbsong1.Checked = bool.Parse(file.ReadLine());
                    g2cbsong2.Checked = bool.Parse(file.ReadLine());
                    g2cbsong3.Checked = bool.Parse(file.ReadLine());
                    g2cbsong4.Checked = bool.Parse(file.ReadLine());
                    g3tbfont.Text = file.ReadLine();
                    g3tbpicture.Text = file.ReadLine();
                    g3tbvideo.Text = file.ReadLine();
                    g3tbpage.Text = file.ReadLine();
                    g3tbprocess.Text = file.ReadLine();
                    g3tbsong1.Text = file.ReadLine();
                    g3tbsong2.Text = file.ReadLine();
                    g3tbsong3.Text = file.ReadLine();
                    g3tbsong4.Text = file.ReadLine();
                    g3cbpicture.Checked = bool.Parse(file.ReadLine());
                    g3cbvideo.Checked = bool.Parse(file.ReadLine());
                    g3cbpage.Checked = bool.Parse(file.ReadLine());
                    g3cbprocess.Checked = bool.Parse(file.ReadLine());
                    g3cbsong1.Checked = bool.Parse(file.ReadLine());
                    g3cbsong2.Checked = bool.Parse(file.ReadLine());
                    g3cbsong3.Checked = bool.Parse(file.ReadLine());
                    g3cbsong4.Checked = bool.Parse(file.ReadLine());
                    g4tbfont.Text = file.ReadLine();
                    g4tbpicture.Text = file.ReadLine();
                    g4tbvideo.Text = file.ReadLine();
                    g4tbpage.Text = file.ReadLine();
                    g4tbprocess.Text = file.ReadLine();
                    g4tbsong1.Text = file.ReadLine();
                    g4tbsong2.Text = file.ReadLine();
                    g4tbsong3.Text = file.ReadLine();
                    g4tbsong4.Text = file.ReadLine();
                    g4cbpicture.Checked = bool.Parse(file.ReadLine());
                    g4cbvideo.Checked = bool.Parse(file.ReadLine());
                    g4cbpage.Checked = bool.Parse(file.ReadLine());
                    g4cbprocess.Checked = bool.Parse(file.ReadLine());
                    g4cbsong1.Checked = bool.Parse(file.ReadLine());
                    g4cbsong2.Checked = bool.Parse(file.ReadLine());
                    g4cbsong3.Checked = bool.Parse(file.ReadLine());
                    g4cbsong4.Checked = bool.Parse(file.ReadLine());
                    g1tbx.Text = file.ReadLine();
                    g1tby.Text = file.ReadLine();
                    g1tbwidth.Text = file.ReadLine();
                    g1tbheight.Text = file.ReadLine();
                    g2tbx.Text = file.ReadLine();
                    g2tby.Text = file.ReadLine();
                    g2tbwidth.Text = file.ReadLine();
                    g2tbheight.Text = file.ReadLine();
                    g3tbx.Text = file.ReadLine();
                    g3tby.Text = file.ReadLine();
                    g3tbwidth.Text = file.ReadLine();
                    g3tbheight.Text = file.ReadLine();
                    g4tbx.Text = file.ReadLine();
                    g4tby.Text = file.ReadLine();
                    g4tbwidth.Text = file.ReadLine();
                    g4tbheight.Text = file.ReadLine();
                    g1tbpdf.Text = file.ReadLine();
                    g1cbpdf.Checked = bool.Parse(file.ReadLine());
                    g2tbpdf.Text = file.ReadLine();
                    g2cbpdf.Checked = bool.Parse(file.ReadLine());
                    g3tbpdf.Text = file.ReadLine();
                    g3cbpdf.Checked = bool.Parse(file.ReadLine());
                    g4tbpdf.Text = file.ReadLine();
                    g4cbpdf.Checked = bool.Parse(file.ReadLine());
                    g1cbwebcam.Checked = bool.Parse(file.ReadLine());
                    g1tbxwebcam.Text = file.ReadLine();
                    g1tbywebcam.Text = file.ReadLine();
                    g1tbwidthwebcam.Text = file.ReadLine();
                    g1tbheightwebcam.Text = file.ReadLine();
                    g2cbwebcam.Checked = bool.Parse(file.ReadLine());
                    g2tbxwebcam.Text = file.ReadLine();
                    g2tbywebcam.Text = file.ReadLine();
                    g2tbwidthwebcam.Text = file.ReadLine();
                    g2tbheightwebcam.Text = file.ReadLine();
                    g3cbwebcam.Checked = bool.Parse(file.ReadLine());
                    g3tbxwebcam.Text = file.ReadLine();
                    g3tbywebcam.Text = file.ReadLine();
                    g3tbwidthwebcam.Text = file.ReadLine();
                    g3tbheightwebcam.Text = file.ReadLine();
                    g4cbwebcam.Checked = bool.Parse(file.ReadLine());
                    g4tbxwebcam.Text = file.ReadLine();
                    g4tbywebcam.Text = file.ReadLine();
                    g4tbwidthwebcam.Text = file.ReadLine();
                    g4tbheightwebcam.Text = file.ReadLine();
                }
            }
        }
        private void btsave_Click(object sender, EventArgs e)
        {
            if (savedfilename != "")
            {
                using (System.IO.StreamWriter createdfile = new System.IO.StreamWriter(savedfilename))
                {
                    createdfile.WriteLine(g1tbfont.Text);
                    createdfile.WriteLine(g1tbpicture.Text);
                    createdfile.WriteLine(g1tbvideo.Text);
                    createdfile.WriteLine(g1tbpage.Text);
                    createdfile.WriteLine(g1tbprocess.Text);
                    createdfile.WriteLine(g1tbsong1.Text);
                    createdfile.WriteLine(g1tbsong2.Text);
                    createdfile.WriteLine(g1tbsong3.Text);
                    createdfile.WriteLine(g1tbsong4.Text);
                    createdfile.WriteLine(g1cbpicture.Checked);
                    createdfile.WriteLine(g1cbvideo.Checked);
                    createdfile.WriteLine(g1cbpage.Checked);
                    createdfile.WriteLine(g1cbprocess.Checked);
                    createdfile.WriteLine(g1cbsong1.Checked);
                    createdfile.WriteLine(g1cbsong2.Checked);
                    createdfile.WriteLine(g1cbsong3.Checked);
                    createdfile.WriteLine(g1cbsong4.Checked);
                    createdfile.WriteLine(g2tbfont.Text);
                    createdfile.WriteLine(g2tbpicture.Text);
                    createdfile.WriteLine(g2tbvideo.Text);
                    createdfile.WriteLine(g2tbpage.Text);
                    createdfile.WriteLine(g2tbprocess.Text);
                    createdfile.WriteLine(g2tbsong1.Text);
                    createdfile.WriteLine(g2tbsong2.Text);
                    createdfile.WriteLine(g2tbsong3.Text);
                    createdfile.WriteLine(g2tbsong4.Text);
                    createdfile.WriteLine(g2cbpicture.Checked);
                    createdfile.WriteLine(g2cbvideo.Checked);
                    createdfile.WriteLine(g2cbpage.Checked);
                    createdfile.WriteLine(g2cbprocess.Checked);
                    createdfile.WriteLine(g2cbsong1.Checked);
                    createdfile.WriteLine(g2cbsong2.Checked);
                    createdfile.WriteLine(g2cbsong3.Checked);
                    createdfile.WriteLine(g2cbsong4.Checked);
                    createdfile.WriteLine(g3tbfont.Text);
                    createdfile.WriteLine(g3tbpicture.Text);
                    createdfile.WriteLine(g3tbvideo.Text);
                    createdfile.WriteLine(g3tbpage.Text);
                    createdfile.WriteLine(g3tbprocess.Text);
                    createdfile.WriteLine(g3tbsong1.Text);
                    createdfile.WriteLine(g3tbsong2.Text);
                    createdfile.WriteLine(g3tbsong3.Text);
                    createdfile.WriteLine(g3tbsong4.Text);
                    createdfile.WriteLine(g3cbpicture.Checked);
                    createdfile.WriteLine(g3cbvideo.Checked);
                    createdfile.WriteLine(g3cbpage.Checked);
                    createdfile.WriteLine(g3cbprocess.Checked);
                    createdfile.WriteLine(g3cbsong1.Checked);
                    createdfile.WriteLine(g3cbsong2.Checked);
                    createdfile.WriteLine(g3cbsong3.Checked);
                    createdfile.WriteLine(g3cbsong4.Checked);
                    createdfile.WriteLine(g4tbfont.Text);
                    createdfile.WriteLine(g4tbpicture.Text);
                    createdfile.WriteLine(g4tbvideo.Text);
                    createdfile.WriteLine(g4tbpage.Text);
                    createdfile.WriteLine(g4tbprocess.Text);
                    createdfile.WriteLine(g4tbsong1.Text);
                    createdfile.WriteLine(g4tbsong2.Text);
                    createdfile.WriteLine(g4tbsong3.Text);
                    createdfile.WriteLine(g4tbsong4.Text);
                    createdfile.WriteLine(g4cbpicture.Checked);
                    createdfile.WriteLine(g4cbvideo.Checked);
                    createdfile.WriteLine(g4cbpage.Checked);
                    createdfile.WriteLine(g4cbprocess.Checked);
                    createdfile.WriteLine(g4cbsong1.Checked);
                    createdfile.WriteLine(g4cbsong2.Checked);
                    createdfile.WriteLine(g4cbsong3.Checked);
                    createdfile.WriteLine(g4cbsong4.Checked);
                    createdfile.WriteLine(g1tbx.Text);
                    createdfile.WriteLine(g1tby.Text);
                    createdfile.WriteLine(g1tbwidth.Text);
                    createdfile.WriteLine(g1tbheight.Text);
                    createdfile.WriteLine(g2tbx.Text);
                    createdfile.WriteLine(g2tby.Text);
                    createdfile.WriteLine(g2tbwidth.Text);
                    createdfile.WriteLine(g2tbheight.Text);
                    createdfile.WriteLine(g3tbx.Text);
                    createdfile.WriteLine(g3tby.Text);
                    createdfile.WriteLine(g3tbwidth.Text);
                    createdfile.WriteLine(g3tbheight.Text);
                    createdfile.WriteLine(g4tbx.Text);
                    createdfile.WriteLine(g4tby.Text);
                    createdfile.WriteLine(g4tbwidth.Text);
                    createdfile.WriteLine(g4tbheight.Text);
                    createdfile.WriteLine(g1tbpdf.Text);
                    createdfile.WriteLine(g1cbpdf.Checked);
                    createdfile.WriteLine(g2tbpdf.Text);
                    createdfile.WriteLine(g2cbpdf.Checked);
                    createdfile.WriteLine(g3tbpdf.Text);
                    createdfile.WriteLine(g3cbpdf.Checked);
                    createdfile.WriteLine(g4tbpdf.Text);
                    createdfile.WriteLine(g4cbpdf.Checked);
                    createdfile.WriteLine(g1cbwebcam.Checked);
                    createdfile.WriteLine(g1tbxwebcam.Text);
                    createdfile.WriteLine(g1tbywebcam.Text);
                    createdfile.WriteLine(g1tbwidthwebcam.Text);
                    createdfile.WriteLine(g1tbheightwebcam.Text);
                    createdfile.WriteLine(g2cbwebcam.Checked);
                    createdfile.WriteLine(g2tbxwebcam.Text);
                    createdfile.WriteLine(g2tbywebcam.Text);
                    createdfile.WriteLine(g2tbwidthwebcam.Text);
                    createdfile.WriteLine(g2tbheightwebcam.Text);
                    createdfile.WriteLine(g3cbwebcam.Checked);
                    createdfile.WriteLine(g3tbxwebcam.Text);
                    createdfile.WriteLine(g3tbywebcam.Text);
                    createdfile.WriteLine(g3tbwidthwebcam.Text);
                    createdfile.WriteLine(g3tbheightwebcam.Text);
                    createdfile.WriteLine(g4cbwebcam.Checked);
                    createdfile.WriteLine(g4tbxwebcam.Text);
                    createdfile.WriteLine(g4tbywebcam.Text);
                    createdfile.WriteLine(g4tbwidthwebcam.Text);
                    createdfile.WriteLine(g4tbheightwebcam.Text);
                }
            }
            else
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "All Files(*.*)|*.*";
                if (sf.ShowDialog() == DialogResult.OK)
                {
                    savedfilename = sf.FileName;
                    this.Text = sf.FileName;
                    using (System.IO.StreamWriter createdfile = new System.IO.StreamWriter(savedfilename))
                    {
                        createdfile.WriteLine(g1tbfont.Text);
                        createdfile.WriteLine(g1tbpicture.Text);
                        createdfile.WriteLine(g1tbvideo.Text);
                        createdfile.WriteLine(g1tbpage.Text);
                        createdfile.WriteLine(g1tbprocess.Text);
                        createdfile.WriteLine(g1tbsong1.Text);
                        createdfile.WriteLine(g1tbsong2.Text);
                        createdfile.WriteLine(g1tbsong3.Text);
                        createdfile.WriteLine(g1tbsong4.Text);
                        createdfile.WriteLine(g1cbpicture.Checked);
                        createdfile.WriteLine(g1cbvideo.Checked);
                        createdfile.WriteLine(g1cbpage.Checked);
                        createdfile.WriteLine(g1cbprocess.Checked);
                        createdfile.WriteLine(g1cbsong1.Checked);
                        createdfile.WriteLine(g1cbsong2.Checked);
                        createdfile.WriteLine(g1cbsong3.Checked);
                        createdfile.WriteLine(g1cbsong4.Checked);
                        createdfile.WriteLine(g2tbfont.Text);
                        createdfile.WriteLine(g2tbpicture.Text);
                        createdfile.WriteLine(g2tbvideo.Text);
                        createdfile.WriteLine(g2tbpage.Text);
                        createdfile.WriteLine(g2tbprocess.Text);
                        createdfile.WriteLine(g2tbsong1.Text);
                        createdfile.WriteLine(g2tbsong2.Text);
                        createdfile.WriteLine(g2tbsong3.Text);
                        createdfile.WriteLine(g2tbsong4.Text);
                        createdfile.WriteLine(g2cbpicture.Checked);
                        createdfile.WriteLine(g2cbvideo.Checked);
                        createdfile.WriteLine(g2cbpage.Checked);
                        createdfile.WriteLine(g2cbprocess.Checked);
                        createdfile.WriteLine(g2cbsong1.Checked);
                        createdfile.WriteLine(g2cbsong2.Checked);
                        createdfile.WriteLine(g2cbsong3.Checked);
                        createdfile.WriteLine(g2cbsong4.Checked);
                        createdfile.WriteLine(g3tbfont.Text);
                        createdfile.WriteLine(g3tbpicture.Text);
                        createdfile.WriteLine(g3tbvideo.Text);
                        createdfile.WriteLine(g3tbpage.Text);
                        createdfile.WriteLine(g3tbprocess.Text);
                        createdfile.WriteLine(g3tbsong1.Text);
                        createdfile.WriteLine(g3tbsong2.Text);
                        createdfile.WriteLine(g3tbsong3.Text);
                        createdfile.WriteLine(g3tbsong4.Text);
                        createdfile.WriteLine(g3cbpicture.Checked);
                        createdfile.WriteLine(g3cbvideo.Checked);
                        createdfile.WriteLine(g3cbpage.Checked);
                        createdfile.WriteLine(g3cbprocess.Checked);
                        createdfile.WriteLine(g3cbsong1.Checked);
                        createdfile.WriteLine(g3cbsong2.Checked);
                        createdfile.WriteLine(g3cbsong3.Checked);
                        createdfile.WriteLine(g3cbsong4.Checked);
                        createdfile.WriteLine(g4tbfont.Text);
                        createdfile.WriteLine(g4tbpicture.Text);
                        createdfile.WriteLine(g4tbvideo.Text);
                        createdfile.WriteLine(g4tbpage.Text);
                        createdfile.WriteLine(g4tbprocess.Text);
                        createdfile.WriteLine(g4tbsong1.Text);
                        createdfile.WriteLine(g4tbsong2.Text);
                        createdfile.WriteLine(g4tbsong3.Text);
                        createdfile.WriteLine(g4tbsong4.Text);
                        createdfile.WriteLine(g4cbpicture.Checked);
                        createdfile.WriteLine(g4cbvideo.Checked);
                        createdfile.WriteLine(g4cbpage.Checked);
                        createdfile.WriteLine(g4cbprocess.Checked);
                        createdfile.WriteLine(g4cbsong1.Checked);
                        createdfile.WriteLine(g4cbsong2.Checked);
                        createdfile.WriteLine(g4cbsong3.Checked);
                        createdfile.WriteLine(g4cbsong4.Checked);
                        createdfile.WriteLine(g1tbx.Text);
                        createdfile.WriteLine(g1tby.Text);
                        createdfile.WriteLine(g1tbwidth.Text);
                        createdfile.WriteLine(g1tbheight.Text);
                        createdfile.WriteLine(g2tbx.Text);
                        createdfile.WriteLine(g2tby.Text);
                        createdfile.WriteLine(g2tbwidth.Text);
                        createdfile.WriteLine(g2tbheight.Text);
                        createdfile.WriteLine(g3tbx.Text);
                        createdfile.WriteLine(g3tby.Text);
                        createdfile.WriteLine(g3tbwidth.Text);
                        createdfile.WriteLine(g3tbheight.Text);
                        createdfile.WriteLine(g4tbx.Text);
                        createdfile.WriteLine(g4tby.Text);
                        createdfile.WriteLine(g4tbwidth.Text);
                        createdfile.WriteLine(g4tbheight.Text);
                        createdfile.WriteLine(g1tbpdf.Text);
                        createdfile.WriteLine(g1cbpdf.Checked);
                        createdfile.WriteLine(g2tbpdf.Text);
                        createdfile.WriteLine(g2cbpdf.Checked);
                        createdfile.WriteLine(g3tbpdf.Text);
                        createdfile.WriteLine(g3cbpdf.Checked);
                        createdfile.WriteLine(g4tbpdf.Text);
                        createdfile.WriteLine(g4cbpdf.Checked);
                        createdfile.WriteLine(g1cbwebcam.Checked);
                        createdfile.WriteLine(g1tbxwebcam.Text);
                        createdfile.WriteLine(g1tbywebcam.Text);
                        createdfile.WriteLine(g1tbwidthwebcam.Text);
                        createdfile.WriteLine(g1tbheightwebcam.Text);
                        createdfile.WriteLine(g2cbwebcam.Checked);
                        createdfile.WriteLine(g2tbxwebcam.Text);
                        createdfile.WriteLine(g2tbywebcam.Text);
                        createdfile.WriteLine(g2tbwidthwebcam.Text);
                        createdfile.WriteLine(g2tbheightwebcam.Text);
                        createdfile.WriteLine(g3cbwebcam.Checked);
                        createdfile.WriteLine(g3tbxwebcam.Text);
                        createdfile.WriteLine(g3tbywebcam.Text);
                        createdfile.WriteLine(g3tbwidthwebcam.Text);
                        createdfile.WriteLine(g3tbheightwebcam.Text);
                        createdfile.WriteLine(g4cbwebcam.Checked);
                        createdfile.WriteLine(g4tbxwebcam.Text);
                        createdfile.WriteLine(g4tbywebcam.Text);
                        createdfile.WriteLine(g4tbwidthwebcam.Text);
                        createdfile.WriteLine(g4tbheightwebcam.Text);
                    }
                }
            }
        }
        private void btsaveas_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "All Files(*.*)|*.*";
            if (sf.ShowDialog() == DialogResult.OK)
            {
                savedfilename = sf.FileName;
                this.Text = sf.FileName;
                using (System.IO.StreamWriter createdfile = new System.IO.StreamWriter(savedfilename))
                {
                    createdfile.WriteLine(g1tbfont.Text);
                    createdfile.WriteLine(g1tbpicture.Text);
                    createdfile.WriteLine(g1tbvideo.Text);
                    createdfile.WriteLine(g1tbpage.Text);
                    createdfile.WriteLine(g1tbprocess.Text);
                    createdfile.WriteLine(g1tbsong1.Text);
                    createdfile.WriteLine(g1tbsong2.Text);
                    createdfile.WriteLine(g1tbsong3.Text);
                    createdfile.WriteLine(g1tbsong4.Text);
                    createdfile.WriteLine(g1cbpicture.Checked);
                    createdfile.WriteLine(g1cbvideo.Checked);
                    createdfile.WriteLine(g1cbpage.Checked);
                    createdfile.WriteLine(g1cbprocess.Checked);
                    createdfile.WriteLine(g1cbsong1.Checked);
                    createdfile.WriteLine(g1cbsong2.Checked);
                    createdfile.WriteLine(g1cbsong3.Checked);
                    createdfile.WriteLine(g1cbsong4.Checked);
                    createdfile.WriteLine(g2tbfont.Text);
                    createdfile.WriteLine(g2tbpicture.Text);
                    createdfile.WriteLine(g2tbvideo.Text);
                    createdfile.WriteLine(g2tbpage.Text);
                    createdfile.WriteLine(g2tbprocess.Text);
                    createdfile.WriteLine(g2tbsong1.Text);
                    createdfile.WriteLine(g2tbsong2.Text);
                    createdfile.WriteLine(g2tbsong3.Text);
                    createdfile.WriteLine(g2tbsong4.Text);
                    createdfile.WriteLine(g2cbpicture.Checked);
                    createdfile.WriteLine(g2cbvideo.Checked);
                    createdfile.WriteLine(g2cbpage.Checked);
                    createdfile.WriteLine(g2cbprocess.Checked);
                    createdfile.WriteLine(g2cbsong1.Checked);
                    createdfile.WriteLine(g2cbsong2.Checked);
                    createdfile.WriteLine(g2cbsong3.Checked);
                    createdfile.WriteLine(g2cbsong4.Checked);
                    createdfile.WriteLine(g3tbfont.Text);
                    createdfile.WriteLine(g3tbpicture.Text);
                    createdfile.WriteLine(g3tbvideo.Text);
                    createdfile.WriteLine(g3tbpage.Text);
                    createdfile.WriteLine(g3tbprocess.Text);
                    createdfile.WriteLine(g3tbsong1.Text);
                    createdfile.WriteLine(g3tbsong2.Text);
                    createdfile.WriteLine(g3tbsong3.Text);
                    createdfile.WriteLine(g3tbsong4.Text);
                    createdfile.WriteLine(g3cbpicture.Checked);
                    createdfile.WriteLine(g3cbvideo.Checked);
                    createdfile.WriteLine(g3cbpage.Checked);
                    createdfile.WriteLine(g3cbprocess.Checked);
                    createdfile.WriteLine(g3cbsong1.Checked);
                    createdfile.WriteLine(g3cbsong2.Checked);
                    createdfile.WriteLine(g3cbsong3.Checked);
                    createdfile.WriteLine(g3cbsong4.Checked);
                    createdfile.WriteLine(g4tbfont.Text);
                    createdfile.WriteLine(g4tbpicture.Text);
                    createdfile.WriteLine(g4tbvideo.Text);
                    createdfile.WriteLine(g4tbpage.Text);
                    createdfile.WriteLine(g4tbprocess.Text);
                    createdfile.WriteLine(g4tbsong1.Text);
                    createdfile.WriteLine(g4tbsong2.Text);
                    createdfile.WriteLine(g4tbsong3.Text);
                    createdfile.WriteLine(g4tbsong4.Text);
                    createdfile.WriteLine(g4cbpicture.Checked);
                    createdfile.WriteLine(g4cbvideo.Checked);
                    createdfile.WriteLine(g4cbpage.Checked);
                    createdfile.WriteLine(g4cbprocess.Checked);
                    createdfile.WriteLine(g4cbsong1.Checked);
                    createdfile.WriteLine(g4cbsong2.Checked);
                    createdfile.WriteLine(g4cbsong3.Checked);
                    createdfile.WriteLine(g4cbsong4.Checked);
                    createdfile.WriteLine(g1tbx.Text);
                    createdfile.WriteLine(g1tby.Text);
                    createdfile.WriteLine(g1tbwidth.Text);
                    createdfile.WriteLine(g1tbheight.Text);
                    createdfile.WriteLine(g2tbx.Text);
                    createdfile.WriteLine(g2tby.Text);
                    createdfile.WriteLine(g2tbwidth.Text);
                    createdfile.WriteLine(g2tbheight.Text);
                    createdfile.WriteLine(g3tbx.Text);
                    createdfile.WriteLine(g3tby.Text);
                    createdfile.WriteLine(g3tbwidth.Text);
                    createdfile.WriteLine(g3tbheight.Text);
                    createdfile.WriteLine(g4tbx.Text);
                    createdfile.WriteLine(g4tby.Text);
                    createdfile.WriteLine(g4tbwidth.Text);
                    createdfile.WriteLine(g4tbheight.Text);
                    createdfile.WriteLine(g1tbpdf.Text);
                    createdfile.WriteLine(g1cbpdf.Checked);
                    createdfile.WriteLine(g2tbpdf.Text);
                    createdfile.WriteLine(g2cbpdf.Checked);
                    createdfile.WriteLine(g3tbpdf.Text);
                    createdfile.WriteLine(g3cbpdf.Checked);
                    createdfile.WriteLine(g4tbpdf.Text);
                    createdfile.WriteLine(g4cbpdf.Checked);
                    createdfile.WriteLine(g1cbwebcam.Checked);
                    createdfile.WriteLine(g1tbxwebcam.Text);
                    createdfile.WriteLine(g1tbywebcam.Text);
                    createdfile.WriteLine(g1tbwidthwebcam.Text);
                    createdfile.WriteLine(g1tbheightwebcam.Text);
                    createdfile.WriteLine(g2cbwebcam.Checked);
                    createdfile.WriteLine(g2tbxwebcam.Text);
                    createdfile.WriteLine(g2tbywebcam.Text);
                    createdfile.WriteLine(g2tbwidthwebcam.Text);
                    createdfile.WriteLine(g2tbheightwebcam.Text);
                    createdfile.WriteLine(g3cbwebcam.Checked);
                    createdfile.WriteLine(g3tbxwebcam.Text);
                    createdfile.WriteLine(g3tbywebcam.Text);
                    createdfile.WriteLine(g3tbwidthwebcam.Text);
                    createdfile.WriteLine(g3tbheightwebcam.Text);
                    createdfile.WriteLine(g4cbwebcam.Checked);
                    createdfile.WriteLine(g4tbxwebcam.Text);
                    createdfile.WriteLine(g4tbywebcam.Text);
                    createdfile.WriteLine(g4tbwidthwebcam.Text);
                    createdfile.WriteLine(g4tbheightwebcam.Text);
                }
            }
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            private int _Left;
            private int _Top;
            private int _Right;
            private int _Bottom;
            public RECT(RECT Rectangle) : this(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom)
            {
            }
            public RECT(int Left, int Top, int Right, int Bottom)
            {
                _Left = Left;
                _Top = Top;
                _Right = Right;
                _Bottom = Bottom;
            }
            public int X
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Y
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Left
            {
                get { return _Left; }
                set { _Left = value; }
            }
            public int Top
            {
                get { return _Top; }
                set { _Top = value; }
            }
            public int Right
            {
                get { return _Right; }
                set { _Right = value; }
            }
            public int Bottom
            {
                get { return _Bottom; }
                set { _Bottom = value; }
            }
            public int Height
            {
                get { return _Bottom - _Top; }
                set { _Bottom = value + _Top; }
            }
            public int Width
            {
                get { return _Right - _Left; }
                set { _Right = value + _Left; }
            }
            public Point Location
            {
                get { return new Point(Left, Top); }
                set
                {
                    _Left = value.X;
                    _Top = value.Y;
                }
            }
            public Size Size
            {
                get { return new Size(Width, Height); }
                set
                {
                    _Right = value.Width + _Left;
                    _Bottom = value.Height + _Top;
                }
            }
            public static implicit operator Rectangle(RECT Rectangle)
            {
                return new Rectangle(Rectangle.Left, Rectangle.Top, Rectangle.Width, Rectangle.Height);
            }
            public static implicit operator RECT(Rectangle Rectangle)
            {
                return new RECT(Rectangle.Left, Rectangle.Top, Rectangle.Right, Rectangle.Bottom);
            }
            public static bool operator ==(RECT Rectangle1, RECT Rectangle2)
            {
                return Rectangle1.Equals(Rectangle2);
            }
            public static bool operator !=(RECT Rectangle1, RECT Rectangle2)
            {
                return !Rectangle1.Equals(Rectangle2);
            }
            public override string ToString()
            {
                return "{Left: " + _Left + "; " + "Top: " + _Top + "; Right: " + _Right + "; Bottom: " + _Bottom + "}";
            }
            public override int GetHashCode()
            {
                return ToString().GetHashCode();
            }
            public bool Equals(RECT Rectangle)
            {
                return Rectangle.Left == _Left && Rectangle.Top == _Top && Rectangle.Right == _Right && Rectangle.Bottom == _Bottom;
            }
            public override bool Equals(object Object)
            {
                if (Object is RECT)
                {
                    return Equals((RECT)Object);
                }
                else if (Object is Rectangle)
                {
                    return Equals(new RECT((Rectangle)Object));
                }
                return false;
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
            form2.Close();
            Process.GetCurrentProcess().Kill();
        }
    }
}