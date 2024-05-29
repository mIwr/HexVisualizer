using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
   typeof(HexVisualizer.StringVisualizer),
   typeof(VisualizerObjectSource),
   Target = typeof(string),
   Description = "Hex Visualizer (String)")]
namespace HexVisualizer
{
    public class StringVisualizer : DialogDebuggerVisualizer
    {
        public StringVisualizer() : base(FormatterPolicy.NewtonsoftJson) { }

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            var stringVisualize = string.Empty;
            try
            {
                var dataStream = objectProvider.GetData();
                if (objectProvider is IVisualizerObjectProvider3)
                {
                    //New JSON decode
                    var objectProvider3 = (IVisualizerObjectProvider3)objectProvider;
                    if (objectProvider3.SelectedFormatterPolicy != FormatterPolicy.Legacy)
                    {                        
                        stringVisualize = objectProvider3.DeserializeFromJson<string>(objectProvider3.GetData());
                    }
                }
                if (string.IsNullOrEmpty(stringVisualize))
                {
                    //Legacy binary decode
                    var binaryFormatter = new BinaryFormatter();
                    var decoded = binaryFormatter.Deserialize(dataStream);
                    if (decoded is string)
                    {
                        stringVisualize = decoded as string;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error converting string: " + ex.Message + "\n" + ex.StackTrace);
            }            
            if (string.IsNullOrEmpty(stringVisualize))
            {
                return;
            }
            var r = StringTypeChooser.GetStringType(stringVisualize);
            if (r == null)
            {
                return;
            }
            var data = Array.Empty<byte>();
            switch (r.ResultType)
            {
                case StringTypeChooserResult.ResultTypeEnum.HexString:
                    data = stringVisualize.ToByteArray();
                    break;

                case StringTypeChooserResult.ResultTypeEnum.Base64String:
                    data = Convert.FromBase64String(stringVisualize);
                    break;

                case StringTypeChooserResult.ResultTypeEnum.Encoding:
                    data = r.Encoding.GetBytes(stringVisualize);
                    break;

            }
            if (data.Length == 0)
            {
                return;
            }
            frmEditor.Visualize(data);
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(StringVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
