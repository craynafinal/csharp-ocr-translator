using DesktopApp.Forms;
using DesktopApp.Poco;
using DesktopApp.Processors;
using System;
using System.Windows.Forms;

namespace DesktopApp
{
    /// <summary>
    /// Main form.
    /// </summary>
    public partial class Main : Form
    {
        
        private GlobalKeyHook globalKeyHook;
        private Configuration configuration;
        private Translator translator;
        private Output output;

        public Main()
        {
            InitializeComponent();
            SetupGlobalKeyHook();
            configuration = Configuration.GetInstance();
            translator = Translator.GetInstance();

            output = new Output(configuration);
            output.Show();
        }

        private void SetupGlobalKeyHook()
        {
            globalKeyHook = new GlobalKeyHook();
            globalKeyHook.KeyDown += new KeyEventHandler(GlobalKeyHook_KeyDown);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                globalKeyHook.HookedKeys.Add(key);
            }

            globalKeyHook.Hook();
        }

        /// <summary>
        /// Set of rules for key down shortcuts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void GlobalKeyHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.X && (ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Shift)))
            {
                OpenScreenGrabber(configuration);
            }

            if (e.KeyCode == Keys.Z && (ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Shift)))
            {
                translator.Run(configuration, output);
            }

            if (e.KeyCode == Keys.C && (ModifierKeys.HasFlag(Keys.Control) || ModifierKeys.HasFlag(Keys.Shift)))
            {
                translator.Abort();
            }
        }
        
        private void OpenScreenGrabber(Configuration configuration)
        {
            var screenForm = new ScreenGrabber(configuration);
            screenForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var dictionaryEditor = new DictionaryEditor();
            dictionaryEditor.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var configEditor = new ConfigEditor(configuration);
            configEditor.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }
    }
}
