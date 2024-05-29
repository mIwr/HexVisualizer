using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace HexVisualizer
{
    public partial class frmEditor : Form
    {
        bool newFind = true;
        public frmEditor()
        {
            InitializeComponent();
        }

        internal static void Visualize(byte[] p)
        {
            var frm = new frmEditor();

            var provider = new DynamicByteProvider(p);

            frm.textBox1.ByteProvider = provider;

            frm.ShowDialog();
        }

        private void textBox1_SelectionLengthChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
            ShowInfo();

        }

        private void textBox1_SelectionStartChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
            ShowInfo();
        }

        private void ShowInfo()
        {
            listView1.BeginUpdate();
            listView1.Items.Clear();

            try
            {
                switch(textBox1.SelectionLength)
                {
                    case 1:
                        {
                            // byte
                            var item = listView1.Items.Add("Type", "Type");
                            item.SubItems.Add("Byte");

                            break;
                        }
                    case 2:
                        {
                            // short
                            var item = listView1.Items.Add("Type", "Type");
                            item.SubItems.Add("Short/UShort (2 Bytes)");

                            var bytes = getSelectedBytes(2);

                            item = listView1.Items.Add("Short (Big Endian)");
                            item.SubItems.Add(BitConverter.ToInt16(bytes, 0).ToString());

                            item = listView1.Items.Add("UShort (Big Endian)");
                            item.SubItems.Add(BitConverter.ToUInt16(bytes, 0).ToString());

                            Array.Reverse(bytes);

                            item = listView1.Items.Add("Short (Little Endian)");
                            item.SubItems.Add(BitConverter.ToInt16(bytes, 0).ToString());

                            item = listView1.Items.Add("UShort (Little Endian)");
                            item.SubItems.Add(BitConverter.ToUInt16(bytes, 0).ToString());


                            break;
                        }
                    case 4:
                        {
                            // int/uint
                            var item = listView1.Items.Add("Type", "Type");
                            item.SubItems.Add("Int/Uint (4 Bytes)");

                            var bytes = getSelectedBytes(4);

                            item = listView1.Items.Add("Int (Big Endian)");
                            item.SubItems.Add(BitConverter.ToInt32(bytes, 0).ToString());

                            item = listView1.Items.Add("UInt (Big Endian)");
                            item.SubItems.Add(BitConverter.ToUInt32(bytes, 0).ToString());

                            Array.Reverse(bytes);

                            item = listView1.Items.Add("Int (Little Endian)");
                            item.SubItems.Add(BitConverter.ToInt32(bytes, 0).ToString());

                            item = listView1.Items.Add("UInt (Litle Endian)");
                            item.SubItems.Add(BitConverter.ToUInt32(bytes, 0).ToString());

                            break;
                        }
                    case 8:
                        {
                            // long/ulong
                            var item = listView1.Items.Add("Type", "Type");
                            item.SubItems.Add("Long/ULong (8 Bytes)");

                            var bytes = getSelectedBytes(8);

                            item = listView1.Items.Add("Long (Big Endian)");
                            item.SubItems.Add(BitConverter.ToInt64(bytes, 0).ToString());

                            item = listView1.Items.Add("ULong (Big Endian)");
                            item.SubItems.Add(BitConverter.ToUInt64(bytes, 0).ToString());

                            Array.Reverse(bytes);

                            item = listView1.Items.Add("Long (Little Endian)");
                            item.SubItems.Add(BitConverter.ToInt64(bytes, 0).ToString());

                            item = listView1.Items.Add("ULong (Little Endian)");
                            item.SubItems.Add(BitConverter.ToUInt64(bytes, 0).ToString());

                            break;
                        }

                    default:
                        {
                            var item = listView1.Items.Add("Type", "Type");
                            item.SubItems.Add(string.Format("Unknown ({0} Bytes)", textBox1.SelectionLength));

                            break;
                        }
                }

                var b = getSelectedBytes((int)textBox1.SelectionLength);

                var i = listView1.Items.Add("ASCII", "ASCII");
                i.SubItems.Add(ASCIIEncoding.Default.GetString(b));

                i = listView1.Items.Add("UTF8", "UTF8");
                i.SubItems.Add(UTF8Encoding.UTF8.GetString(b));

                i = listView1.Items.Add("UTF32", "UTF32");
                i.SubItems.Add(UTF32Encoding.UTF32.GetString(b));

                i = listView1.Items.Add("Bytes", "Bytes");
                i.SubItems.Add("0x" + BitConverter.ToString(b).Replace("-", ""));
            }
            catch(Exception ex)
            {
                listView1.Items.Add("Error", "Error: " + ex.Message);
            }

            listView1.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
            listView1.EndUpdate();

        }

        private byte[] getSelectedBytes(int len)
        {
            var p = textBox1.ByteProvider;

            var b = new byte[len];

            for(int i = 0; i < len; i++)
            {
                b[i] = p.ReadByte(textBox1.SelectionStart + i);
            }

            return b;
        }

        private void UpdateStatusBar()
        {
            var sb = new StringBuilder();

            if(textBox1.SelectionLength > 0)
            {
                sb.AppendFormat("Selected index {0} to {1}, ", textBox1.SelectionStart, textBox1.SelectionStart + textBox1.SelectionLength - 1);
            }
            sb.AppendFormat("Current index {0} (Line {1} Column {2})", (textBox1.CurrentLine - 1) * 16 + textBox1.CurrentPositionInLine - 1, textBox1.CurrentLine, textBox1.CurrentPositionInLine);

            uiPosition.Text = sb.ToString();
        }


        private void textBox1_CurrentLineChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }


        private void textBox1_CurrentPositionInLineChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                Clipboard.SetText(listView1.SelectedItems[0].SubItems[1].Text);
            }
        }

        private void uiFindButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(uiFindText.Text)) return;

            if(newFind)
            {
                textBox1.AbortFind();
                textBox1.Select(0, 0); //remove any selection
            }

            Regex rBytes = new Regex("0x[0-9a-fA-F]+", RegexOptions.Compiled);

            if(rBytes.IsMatch(uiFindText.Text) && uiFindText.Text.Length % 2 == 0)
            {
                var bytes = uiFindText.Text.ToByteArray();
                var r = textBox1.Find(new FindOptions()
                {
                    Hex = bytes,
                    Type = FindType.Hex,

                });
            }
            else
            {
                var r = textBox1.Find(new FindOptions()
                {
                    Text = uiFindText.Text,
                    Type = FindType.Text,
                    MatchCase = false,
                });
            }

            newFind = false;
        }

        private void frmEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F && e.Control)
            {
                uiFindText.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }

            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void uiFindText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                uiFindButton.PerformClick();
                e.Handled = true;
            }
        }

        private void uiFindText_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.F)
            {
                e.SuppressKeyPress = true; // stop the annoying beep
            }
        }

        private void uiFindText_TextChanged(object sender, EventArgs e)
        {
            newFind = true;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            // Allow the escape key to close the form
            if (ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
