using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using speechRecoLib;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Globalization;
using System.Threading;

namespace RobertArtificialCopilot
{

    public partial class rac : Form
    {

        ProfileParser profile;
        CommandManager cmd;

        public rac()
        {
            InitializeComponent();
            
            //Engine DropDownList
            Dictionary<string, CultureInfo> list = new Dictionary<string, CultureInfo>();
            foreach (RecognizerInfo engine in SpeechManager.GetInstalledEngine())
            {
                this.EngineComboBox.Items.Add(new ComboboxItem(engine.Description, engine.Culture));
            }
            this.EngineComboBox.SelectedIndex = 0;

            //Voice DropDownList
            foreach (VoiceInfo voice in SpeechManager.GetInstalledVoice())
            {
                this.VoiceComboBox.Items.Add(new ComboboxItem(voice.Description, voice));
            }
            this.VoiceComboBox.SelectedIndex = 0;
        }

        private void EngineComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string culture = (this.EngineComboBox.SelectedItem as ComboboxItem).Value.ToString();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }

        private void VoiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VoiceInfo voice = (this.VoiceComboBox.SelectedItem as ComboboxItem).Value as VoiceInfo;
            SpeechManager.SelectInstalledVoice(voice);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            this.ProfileNameLabel.Text = openFileDialog1.FileName;
            profile = new ProfileParser(openFileDialog1.FileName);

            if (profile.Parse())
            {
                this.StartButton.Enabled = true;
            }
            else
            {
                this.StartButton.Enabled = false;
            }
        }

        private void LoadProfileButton_Click(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            cmd = profile.GetCmd();
            SpeechManager.Start(this.HandleSpeechRecognized, profile.GetGrammar());
            this.StartButton.Enabled = false;
            this.StopButton.Enabled = true;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SpeechManager.Stop();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            SpeechManager.Stop();
            this.StartButton.Enabled = true;
            this.StopButton.Enabled = false;
        }

        public void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            richTextBox1.AppendText("You said: " + e.Result.Text + "\n");
            cmd.Exec(e.Result.Text);
        }

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
        public ComboboxItem(string text, object value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
