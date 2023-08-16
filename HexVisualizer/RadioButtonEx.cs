using System.Windows.Forms;

namespace HexVisualizer
{
    public static class RadioButtonEx
    {
        public static void AllowDoubleClick(this RadioButton rb, MouseEventHandler MouseDoubleClick)
        {
            //
            // Allow double clicking of radios
            System.Reflection.MethodInfo m = typeof(RadioButton).GetMethod("SetStyle", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if(m != null)
                m.Invoke(rb, new object[] { ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true });

            rb.MouseDoubleClick += MouseDoubleClick;
        }
    }
}
