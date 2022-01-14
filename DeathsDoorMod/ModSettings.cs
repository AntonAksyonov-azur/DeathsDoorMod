namespace DeathsDoorMod
{
    public class ModSettings
    {
        public bool AutoSize;
        public int FontSize;
        public string Text;

        public ModSettings(bool autoSize, int fontSize, string text)
        {
            AutoSize = autoSize;
            FontSize = fontSize;
            Text = text;
        }
    }
}