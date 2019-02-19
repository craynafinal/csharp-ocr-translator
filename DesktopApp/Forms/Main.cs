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
            if (e.KeyCode == Keys.X && (ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift)))
            {
                OpenScreenGrabber(configuration);
            }

            if (e.KeyCode == Keys.Z && (ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift)))
            {
                translator.Run(configuration, output);
            }

            if (e.KeyCode == Keys.C && (ModifierKeys.HasFlag(Keys.Control) && ModifierKeys.HasFlag(Keys.Shift)))
            {
                translator.Abort();
            }
        }
        
        private void OpenScreenGrabber(Configuration configuration)
        {
            var screenForm = ScreenGrabber.GetInstance(configuration);
            screenForm.Show();
        }

        private void OpenDictionaryEditor()
        {
            if ((Application.OpenForms["DictionaryEditor"] as DictionaryEditor) == null)
            {
                var dictionaryEditor = new DictionaryEditor();
                dictionaryEditor.Show();
            }
        }

        private void OpenConfigEditor(Configuration configuration)
        {
            if ((Application.OpenForms["ConfigEditor"] as ConfigEditor) == null)
            {
                var configEditor = new ConfigEditor(configuration);
                configEditor.Show();
            }
        }

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            OpenConfigEditor(configuration);
        }

        private void DictionaryButton_Click(object sender, EventArgs e)
        {
            OpenDictionaryEditor();
        }

        private void SetTargetButton_Click(object sender, EventArgs e)
        {
            OpenScreenGrabber(configuration);
        }

        private void OutputButton_Click(object sender, EventArgs e)
        {
            if ((Application.OpenForms["Output"] as Output) == null)
            {
                output = new Output(configuration);
                output.Show();
            }
        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            translator.Run(configuration, output);
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            translator.Abort();
        }
    }
}
