using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace QuickBox.MG.Controls
{
    public class ImageButton : Button
    {
        public enum State
        {
            /// <summary>
            /// 默认
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 获取焦点
            /// </summary>
            Focus = 1,
            /// <summary>
            /// 鼠标按下
            /// </summary>
            Down = 2,
            /// <summary>
            /// 禁用
            /// </summary>
            Disabled = 3
        }

        private ImageButton.State state = ImageButton.State.Normal;
        private bool _buttonEnabled = true;
        private IContainer components = null;

        public ImageButton.State ButtonState
        {
            get
            {
                return this.state;
            }
            set
            {
                if (this.state != value)
                {
                    this.state = value;
                    this.Refresh();
                }
            }
        }

        public bool BtnEnabled
        {
            get
            {
                return this._buttonEnabled;
            }
            set
            {
                if (!value)
                {
                    this._buttonEnabled = false;
                    this.state = ImageButton.State.Disabled;
                    this.disBindEvent();
                }
                else
                {
                    this._buttonEnabled = true;
                    this.state = ImageButton.State.Normal;
                    this.bindEvent();
                }
            }
        }

        public ImageButton()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            base.Paint += new PaintEventHandler(this.imgButtonPaint);
        }

        private void bindEvent()
        {
            base.MouseLeave += new EventHandler(this.imgButton_MouseLeave);
            base.LostFocus += new EventHandler(this.imgButton_LostFocus);
            base.MouseHover += new EventHandler(this.imgButton_MouseHover);
            base.MouseUp += new MouseEventHandler(this.imgButton_MouseUP);
            base.MouseDown += new MouseEventHandler(this.imgButton_MouseDown);
            base.EnabledChanged += new EventHandler(this.imgButton_EnAbleChanged);
        }

        private void disBindEvent()
        {
            base.MouseLeave -= new EventHandler(this.imgButton_MouseLeave);
            base.LostFocus -= new EventHandler(this.imgButton_LostFocus);
            base.MouseHover -= new EventHandler(this.imgButton_MouseHover);
            base.MouseUp -= new MouseEventHandler(this.imgButton_MouseUP);
            base.MouseDown -= new MouseEventHandler(this.imgButton_MouseDown);
            base.EnabledChanged -= new EventHandler(this.imgButton_EnAbleChanged);
        }

        private void imgButton_MouseLeave(object sender, EventArgs e)
        {
            this.ButtonState = ImageButton.State.Normal;
            base.Invalidate();
        }

        private void imgButton_LostFocus(object sender, EventArgs e)
        {
            this.ButtonState = ImageButton.State.Normal;
            base.Invalidate();
        }

        private void imgButton_MouseHover(object sender, EventArgs e)
        {
            this.ButtonState = ImageButton.State.Focus;
            base.Invalidate();
        }

        private void imgButton_MouseUP(object sender, MouseEventArgs e)
        {
            this.ButtonState = ImageButton.State.Focus;
            base.Invalidate();
        }

        private void imgButton_MouseDown(object sender, MouseEventArgs e)
        {
            this.ButtonState = ImageButton.State.Down;
            base.Invalidate();
        }

        private void imgButton_EnAbleChanged(object sender, EventArgs e)
        {
            if (base.Enabled)
            {
                this.ButtonState = ImageButton.State.Normal;
                base.Invalidate();
            }
            else
            {
                this.ButtonState = ImageButton.State.Disabled;
                base.Invalidate();
            }
        }
        private void imgButtonPaint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            int num = base.Size.Width * (int)this.state;
            if (base.BackgroundImage != null)
            {
                Rectangle rectangle = new Rectangle(new Point(num, 0), base.Size);

                Bitmap bit = new Bitmap(base.BackgroundImage);
                bit.SetResolution(96.0F, 96.0F);
                graphics.DrawImage(bit, 0, 0, rectangle, GraphicsUnit.Pixel);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }
    }
}
