using System;
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
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider)
        {
            byte[] obj = null;

            if (objectProvider.GetObject() is WeakReference<byte[]>)
            {
                (objectProvider.GetObject() as WeakReference<byte[]>).TryGetTarget(out obj);
            }

            if (objectProvider.GetObject() is WeakReference)
                obj = (objectProvider.GetObject() as WeakReference).Target as byte[];

            else if (objectProvider.GetObject() is byte[])
                obj = objectProvider.GetObject() as byte[];

            if(obj != null)
            {
                frmEditor.Visualize(obj);
            }
            else
            {
                MessageBox.Show("Only WeakReference of byte[] allowed");
            }
        }
        
        public static void TestShowVisualizer(object objectToVisualize)
        {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(ByteArrayVisualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
