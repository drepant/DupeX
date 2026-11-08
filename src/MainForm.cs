// DupeX UI shell — open part of the project.
// The trade-desync engine (core) is closed-source and ships only in release builds.
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DupeX.UI
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    // ===== палитра =====
    internal static class C
    {
        public static readonly Color Bg = Color.FromArgb(13, 15, 20);
        public static readonly Color Panel = Color.FromArgb(19, 22, 30);
        public static readonly Color PanelLine = Color.FromArgb(38, 44, 58);
        public static readonly Color Accent = Color.FromArgb(0, 214, 130);
        public static readonly Color AccentDark = Color.FromArgb(0, 150, 90);
        public static readonly Color Text = Color.FromArgb(230, 235, 244);
        public static readonly Color Muted = Color.FromArgb(120, 130, 150);
        public static readonly Color Warn = Color.FromArgb(255, 176, 84);
        public static readonly Color Field = Color.FromArgb(24, 28, 38);
    }

    // ===== круглая кнопка-градиент =====
    internal class AccentButton : Control
    {
        private bool hovered = false;
        private bool pressed = false;

        public AccentButton()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { hovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hovered = false; pressed = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { pressed = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { pressed = false; Invalidate(); base.OnMouseUp(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            int r = Math.Min(10, this.Height / 2);
            using (GraphicsPath p = UI.Round(this.ClientRectangle, r))
            {
                Color c1 = pressed ? C.AccentDark : (hovered ? Color.FromArgb(20, 235, 150) : C.Accent);
                Color c2 = pressed ? C.Accent : C.AccentDark;
                using (LinearGradientBrush b = new LinearGradientBrush(this.ClientRectangle, c1, c2, 90f))
                {
                    e.Graphics.FillPath(b, p);
                }
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    using (Font f = new Font("Segoe UI", 10.5F, FontStyle.Bold))
                    {
                        e.Graphics.DrawString(this.Text, f, Brushes.White, this.ClientRectangle, sf);
                    }
                }
            }
        }
    }

    // ===== тонкая кнопка =====
    internal class GhostButton : Control
    {
        private bool hovered = false;

        public GhostButton()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Cursor = Cursors.Hand;
        }

        protected override void OnMouseEnter(EventArgs e) { hovered = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { hovered = false; Invalidate(); base.OnMouseLeave(e); }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = this.ClientRectangle;
            rect.Width--; rect.Height--;
            using (GraphicsPath p = UI.Round(rect, 8))
            {
                using (SolidBrush fill = new SolidBrush(hovered ? Color.FromArgb(28, 34, 44) : C.Panel))
                {
                    e.Graphics.FillPath(fill, p);
                }
                using (Pen pen = new Pen(hovered ? C.Accent : C.PanelLine, 1.4f))
                {
                    e.Graphics.DrawPath(pen, p);
                }
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    using (Font f = new Font("Segoe UI", 9F, FontStyle.Bold))
                    {
                        using (SolidBrush tb = new SolidBrush(hovered ? C.Text : C.Muted))
                        {
                            e.Graphics.DrawString(this.Text, f, tb, this.ClientRectangle, sf);
                        }
                    }
                }
            }
        }
    }

    // ===== тонкий прогресс =====
    internal class SlimProgress : Control
    {
        private int val = 0;
        public int Value
        {
            get { return val; }
            set { val = Math.Max(0, Math.Min(100, value)); Invalidate(); }
        }

        public SlimProgress()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Height = 8;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath tp = UI.Round(new Rectangle(0, 0, this.Width, this.Height), this.Height / 2))
            using (SolidBrush b = new SolidBrush(C.Field))
            {
                e.Graphics.FillPath(b, tp);
            }
            if (val > 0)
            {
                int w = (int)((this.Width - 2) * (val / 100.0));
                Rectangle fill = new Rectangle(1, 1, Math.Max(6, w), this.Height - 2);
                using (GraphicsPath fp = UI.Round(fill, (this.Height - 2) / 2))
                using (LinearGradientBrush b = new LinearGradientBrush(fill, C.Accent, C.AccentDark, 0f))
                {
                    e.Graphics.FillPath(b, fp);
                }
            }
        }
    }

    // ===== хелперы =====
    internal static class UI
    {
        public static GraphicsPath Round(Rectangle r, int rad)
        {
            GraphicsPath p = new GraphicsPath();
            int d = rad * 2;
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }

        public static Label MkLabel(string text, Color color, float size, bool bold, Point loc)
        {
            Label l = new Label();
            l.Text = text;
            l.ForeColor = color;
            l.Font = new Font("Segoe UI", size, bold ? FontStyle.Bold : FontStyle.Regular);
            l.AutoSize = true;
            l.Location = loc;
            l.BackColor = Color.Transparent;
            return l;
        }
    }

    // ===== главное окно (UI shell) =====
    internal class MainForm : Form
    {
        private readonly TextBox nick;
        private readonly TextBox item;
        private ComboBox game;
        private NumericUpDown amount;
        private readonly AccentButton dupeBtn;
        private readonly RichTextBox log;
        private readonly SlimProgress progress;
        private readonly Label statusDot;
        private readonly Label statusText;
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int step = 0;
        private bool dragging = false;
        private Point dragFrom;

        public MainForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(540, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = C.Bg;
            this.DoubleBuffered = true;

            Panel titleBar = new Panel();
            titleBar.Size = new Size(540, 54);
            titleBar.BackColor = C.Bg;
            titleBar.MouseDown += (s, e) => { dragging = true; dragFrom = e.Location; };
            titleBar.MouseMove += (s, e) => { if (dragging) this.Location = new Point(this.Location.X + e.X - dragFrom.X, this.Location.Y + e.Y - dragFrom.Y); };
            titleBar.MouseUp += (s, e) => dragging = false;
            this.Controls.Add(titleBar);

            Label logo = UI.MkLabel("DupeX", C.Accent, 17F, true, new Point(20, 13));
            titleBar.Controls.Add(logo);
            Label ver = UI.MkLabel("v3.1", C.Muted, 10F, false, new Point(105, 24));
            titleBar.Controls.Add(ver);
            Label cap = UI.MkLabel("item duplicator  ·  private build", C.Muted, 9F, false, new Point(140, 24));
            titleBar.Controls.Add(cap);

            GhostButton min = new GhostButton();
            min.Text = "—";
            min.Size = new Size(34, 26);
            min.Location = new Point(452, 14);
            min.Click += (s, e) => this.WindowState = FormWindowState.Minimized;
            titleBar.Controls.Add(min);

            GhostButton close = new GhostButton();
            close.Text = "×";
            close.Size = new Size(34, 26);
            close.Location = new Point(494, 14);
            close.Click += (s, e) => this.Close();
            titleBar.Controls.Add(close);

            Panel line = new Panel();
            line.Size = new Size(502, 1);
            line.Location = new Point(20, 54);
            line.BackColor = C.PanelLine;
            this.Controls.Add(line);

            Panel card = new Panel();
            card.Size = new Size(500, 206);
            card.Location = new Point(20, 66);
            card.BackColor = C.Panel;
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (GraphicsPath p = UI.Round(new Rectangle(0, 0, 499, 205), 10))
                using (Pen pen = new Pen(C.PanelLine, 1.2f))
                {
                    e.Graphics.DrawPath(pen, p);
                }
            };
            this.Controls.Add(card);

            card.Controls.Add(UI.MkLabel("НИК В ROBLOX", C.Muted, 8.5F, true, new Point(18, 16)));
            nick = new TextBox();
            nick.Location = new Point(18, 36);
            nick.Size = new Size(464, 30);
            nick.BorderStyle = BorderStyle.FixedSingle;
            nick.BackColor = C.Field;
            nick.ForeColor = C.Text;
            nick.Font = new Font("Segoe UI", 11.5F);
            card.Controls.Add(nick);

            // поля «игра/предмет/кол-во» и кнопки — см. скриншоты
            item = new TextBox();
            game = new ComboBox();
            amount = new NumericUpDown();

            dupeBtn = new AccentButton();
            dupeBtn.Text = "DUPLICATE";
            dupeBtn.Size = new Size(150, 40);
            dupeBtn.Location = new Point(330, 146);
            dupeBtn.Click += (s, e) => DupeClick();
            card.Controls.Add(dupeBtn);

            log = new RichTextBox();
            log.Location = new Point(20, 296);
            log.Size = new Size(500, 166);
            log.BorderStyle = BorderStyle.None;
            log.BackColor = Color.FromArgb(10, 12, 16);
            log.ForeColor = C.Text;
            log.ReadOnly = true;
            log.Font = new Font("Consolas", 9.5F);
            this.Controls.Add(log);

            progress = new SlimProgress();
            progress.Location = new Point(20, 486);
            progress.Size = new Size(500, 8);
            this.Controls.Add(progress);

            statusDot = new Label();
            statusDot.Text = "●";
            statusDot.ForeColor = C.Accent;
            statusDot.AutoSize = true;
            statusDot.Location = new Point(20, 506);
            statusDot.BackColor = Color.Transparent;
            this.Controls.Add(statusDot);

            statusText = UI.MkLabel("готов  ·  эксплойт активен до патча", C.Muted, 9F, false, new Point(40, 507));
            this.Controls.Add(statusText);

            timer.Interval = 1300;
            timer.Tick += (s, e) => TimerTick();
        }

        // В shell-сборке дюп-цикл не работает: движок закрыт,
        // release-сборка подключает ядро на этапе сборки.
        private void DupeClick()
        {
            LogLine("[!]", "engine not linked — this is the open UI shell build", C.Warn);
        }

        private void LogLine(string tag, string msg, Color color)
        {
            string ts = DateTime.Now.ToString("HH:mm:ss");
            log.SelectionStart = log.TextLength;
            log.SelectionColor = C.Muted;
            log.AppendText(ts + " ");
            log.SelectionColor = tag == "[+]" ? C.Accent : (tag == "[!]" ? C.Warn : C.Text);
            log.AppendText(tag + " ");
            log.SelectionColor = color;
            log.AppendText(msg + "\n");
            log.SelectionColor = C.Text;
            log.ScrollToCaret();
        }

        private void TimerTick()
        {
        }
    }
}
