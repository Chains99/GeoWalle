using GeoWallE.GraphicRuntime;
using System.Windows.Forms;

namespace GeoWallE.WFGraphicComponents
{
    public class RichTextBoxPrinter : GPrinter
    {
        private readonly RichTextBox _textBox;

        public RichTextBoxPrinter(RichTextBox textBox)
        {
            _textBox = textBox;
        }

        public override void Print(string text)
        {
            _textBox.Text += text + "\n";
        }
    }
}
