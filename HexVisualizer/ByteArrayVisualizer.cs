using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Microsoft.VisualStudio.DebuggerVisualizers;

[assembly: System.Diagnostics.DebuggerVisualizer(
   typeof(HexVisualizer.ByteArrayVisualizer),
   typeof(VisualizerObjectSource),
   Target = typeof(WeakReference),
   Description = "Hex Visualizer")]
[assembly: System.Diagnostics.DebuggerVisualizer(
   typeof(HexVisualizer.ByteArrayVisualizer),
   typeof(VisualizerObjectSource),
   Target = typeof(WeakReference<byte[]>),
   Description = "Hex Visualizer")]
[assembly: System.Diagnostics.DebuggerVisualizer(
   typeof(HexVisualizer.ByteArrayVisualizer),
   typeof(VisualizerObjectSource),
   Target = typeof(byte[]),
   Description = "Hex Visualizer")]
namespace HexVisualizer
{
    public class ByteArrayVisualizer : DialogDebuggerVisualizer
    {
        public ByteArrayVisualizer() : base(FormatterPolicy.NewtonsoftJson) { }

        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
           var data = Array.Empty<byte>();
            try
            {
                var dataStream = objectProvider.GetData();
                if (objectProvider is IVisualizerObjectProvider3)
                {
                    //New JSON decode
                    var objectProvider3 = (IVisualizerObjectProvider3)objectProvider;
                    if (objectProvider3.SelectedFormatterPolicy != FormatterPolicy.Legacy)
                    {
                        data = objectProvider3.DeserializeFromJson<byte[]>(objectProvider3.GetData());
                    }
                }
                if (data.Length == 0)
                {
                    //Legacy binary decode
                    var binaryFormatter = new BinaryFormatter();
                    var decoded = binaryFormatter.Deserialize(dataStream);
                    if (decoded is byte[])
                    {
                        data = decoded as byte[];
                    }
                    else if (decoded is WeakReference)
                    {
                        if (decoded is WeakReference<byte[]>)
                        {
                           (decoded as WeakReference<byte[]>).TryGetTarget(out data);
                        }
                        else
                        {
                            data = (decoded as WeakReference).Target as byte[];
                        }                        
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                return;
            }
            frmEditor.Visualize(data);
        }

#if DEBUG
        public static void TestShowVisualizer(object objectToVisualize)
        {
            var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ByteArrayVisualizer));
            visualizerHost.ShowVisualizer();
        }
#endif        
    }
}
