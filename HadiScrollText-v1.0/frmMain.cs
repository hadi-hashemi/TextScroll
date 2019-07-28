using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;

namespace HadiScrollText_v1._0
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        //  Side + Speed + Intensity + Text
        int
            _brightness = 3,
            _speed = 3,
            _side = 1;

        string[] portNumbers;

        public frmMain()
        {
            InitializeComponent();

            gaugeControl1.ColorScheme.Color = Color.RoyalBlue;
            gaugeControl2.ColorScheme.Color = Color.Crimson;

            label1.ForeColor = Color.RoyalBlue;
            label2.ForeColor = Color.Crimson;

            portNumbers = SerialPort.GetPortNames();

            if (portNumbers.Length != 0)
            {
                cmbDeviceName.Items.Clear();

                for (int i = 0; i < portNumbers.Length; i++)
                {
                    cmbDeviceName.Items.Add(portNumbers[i]);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://hadihashemi.com.tr");
        }

        private void cmbDeviceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            sp.PortName = cmbDeviceName.SelectedItem.ToString();
            pnl.Visible = false;

            Send();
        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            arcScaleComponent1.Value = (float)Convert.ToDouble(trackBarControl1.EditValue);
            labelComponent1.Text = trackBarControl1.Value.ToString();

            _speed = trackBarControl1.Value;
        }

        private void trackBarControl1_MouseUp(object sender, MouseEventArgs e)
        {
            Send();
        }

        private void trackBarControl2_EditValueChanged(object sender, EventArgs e)
        {
            arcScaleComponent2.Value = (float)Convert.ToDouble(trackBarControl2.EditValue);
            labelComponent2.Text = trackBarControl2.Value.ToString();

            _brightness = trackBarControl2.Value;
        }

        private void trackBarControl2_MouseUp(object sender, MouseEventArgs e)
        {
            Send();
        }

        private void picLeft_Click(object sender, EventArgs e)
        {
            _side = 1;

            picLeft.Image = Properties.Resources.LeftG;
            picRight.Image = Properties.Resources.RightW;

            Send();
        }

        private void picRight_Click(object sender, EventArgs e)
        {
            _side = 2;

            picLeft.Image = Properties.Resources.LeftW;
            picRight.Image = Properties.Resources.RightG;

            Send();
        }

        private void txtText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Send();
            }
        }

        private void Send()
        {
            try
            {
                sp.Open();
                sp.Write(_side.ToString() + _speed.ToString() + _brightness.ToString() + txtText.Text);
                sp.Close();
            }
            catch
            {
                //...
            }
        }
    }
}
