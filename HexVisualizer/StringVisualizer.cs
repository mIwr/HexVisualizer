using System;
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
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            try
            {
                byte[] obj = null;

                string objectString = objectProvider.GetObject() as string;

                var r = StringTypeChooser.GetStringType(objectString);

                if(r == null) return;


                switch(r.ResultType)
                {
                    case StringTypeChooserResult.ResultTypeEnum.HexString:
                        obj = objectString.ToByteArray();
                        break;

                    case StringTypeChooserResult.ResultTypeEnum.Base64String:
                        obj = Convert.FromBase64String(objectString);
                        break;

                    case StringTypeChooserResult.ResultTypeEnum.Encoding:
                        obj = r.Encoding.GetBytes(objectString);
                        break;

                }

                if(obj != null)
                {
                    frmEditor.Visualize(obj);
                }
                else
                {
                    MessageBox.Show("String could not be converted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error converting string: {ex.Message}");

            }
        }

        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(StringVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
