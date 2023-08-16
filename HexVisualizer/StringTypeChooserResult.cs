using System.Text;

namespace HexVisualizer
{
    public class StringTypeChooserResult
    {
        public enum ResultTypeEnum
        {
            HexString,
            Base64String,
            Encoding
        }

        public ResultTypeEnum ResultType { get; set; }
        public Encoding Encoding { get; set; }
    }
}
