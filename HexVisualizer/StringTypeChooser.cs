using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HexVisualizer
{
    public partial class StringTypeChooser : Form
    {
        public StringTypeChooser()
        {
            InitializeComponent();

            var encodings = (from enc in Encoding.GetEncodings()
                             select new
                             {
                                 DisplayName = $"{enc.CodePage} - {enc.DisplayName}",
                                 EncodingInfo = enc,
                                 enc.CodePage
                             })
                      .ToList();

            uiEncoding.DisplayMember = "DisplayName";
            uiEncoding.ValueMember = "CodePage";
            uiEncoding.DataSource = encodings;
            uiEncoding.SelectedValue = Encoding.Default.CodePage;
            uiEncoding.Enabled = false;

            uiRadio_Hex.AllowDoubleClick(DoubleClickRadioButton);
            uiRadio_Base64.AllowDoubleClick(DoubleClickRadioButton);
            uiRadio_Encoding.AllowDoubleClick(DoubleClickRadioButton);
        }

        private void StringTypeChooser_Load(object sender, EventArgs e)
        {

        }

        public static StringTypeChooserResult GetStringType(string objString)
        {
            Regex rHexString = new Regex("^(0x){0,1}[0-9a-fA-F]+$", RegexOptions.Compiled);
            Regex rBase64 = new Regex("^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$", RegexOptions.Compiled);

            var f = new StringTypeChooser();

            //try be clever about a default chosen option
            if(rHexString.IsMatch(objString))
            {
                f.uiRadio_Hex.Checked = true;
            }
            else if(rBase64.IsMatch(objString))
            {
                f.uiRadio_Base64.Checked = true;
            }
            else
            {
                f.uiRadio_Encoding.Checked = true;
            }


            var r = f.ShowDialog();

            if(r == DialogResult.OK)
            {
                return new StringTypeChooserResult()
                {
                    ResultType = f.uiRadio_Hex.Checked
                                    ? StringTypeChooserResult.ResultTypeEnum.HexString
                                    : f.uiRadio_Base64.Checked ? StringTypeChooserResult.ResultTypeEnum.Base64String
                                    : StringTypeChooserResult.ResultTypeEnum.Encoding,
                    Encoding = f.uiRadio_Encoding.Checked ? Encoding.GetEncoding((int)f.uiEncoding.SelectedValue) : null
                };
            }
            else
            {
                return null;
            }
        }

        private void uiRadio_CheckedChanged(object sender, EventArgs e)
        {
            uiEncoding.Enabled = uiRadio_Encoding.Checked;
            uiOkButton.Enabled = uiRadio_Base64.Checked || uiRadio_Hex.Checked || uiRadio_Encoding.Checked;
        }

        private void DoubleClickRadioButton(object sender, MouseEventArgs e)
        {
            (sender as RadioButton).Select();
            uiOkButton.PerformClick();
        }
    }
}
