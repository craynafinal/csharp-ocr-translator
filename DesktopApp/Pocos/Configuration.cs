namespace DesktopApp.Poco
{
    /// <summary>
    /// Collection of x, y, width and height of screen focus.
    /// </summary>
    public class Configuration
    {
        private static Configuration instance;

        private Configuration()
        {
            IsScreenshotAreaSet = false;
            IsOutputAreaSet = false;
            Font = "Gulim";
            FontSize = 16;
        }

        /// <summary>
        /// Get instance of configuration.
        /// </summary>
        /// <returns>Configuration instance</returns>
        public static Configuration GetInstance()
        {
            if (instance == null)
            {
                instance = new Configuration();
            }

            return instance;
        }

        /* screenshot area */
        public int ScreenshotX { get; set; }
        public int ScreenshotY { get; set; }
        public int ScreenshotWidth { get; set; }
        public int ScreenshotHeight { get; set; }
        public bool IsScreenshotAreaSet { get; set; }
        
        /* font */
        public string Font { get; set; }
        public int FontSize { get; set; }

        /* output area */
        public int OutputX { get; set; }
        public int OutputY { get; set; }
        public int OutputWidth { get; set; }
        public int OutputHeight { get; set; }
        public bool IsOutputAreaSet { get; set; }

        /* language setting */
    }
}
