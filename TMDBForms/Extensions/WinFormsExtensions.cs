using System;
using System.Windows.Forms;

namespace TMDBForms
{
    public static class WinFormsExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="value"></param>
        public static void AppendLine(this TextBox source, string value)
        {
            if (source.Text.Length == 0)
                source.Text = value;
            else
                source.AppendText(Environment.NewLine + value);
        }


        public static void AppendError(this TextBox textBox, string ex_Message, string ex = "")
        {
            textBox.AppendLine("");
            textBox.AppendLine(ex_Message);
            textBox.AppendLine(ex);
            textBox.AppendLine("");
            textBox.AppendLine("");
            textBox.AppendLine("-----------------------");
        }
    }
}
