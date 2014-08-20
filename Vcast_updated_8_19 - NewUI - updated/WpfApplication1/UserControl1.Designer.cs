
using System.Windows.Forms;
using System;
namespace WpfApplication1
{
    partial class UserControl1
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
        public AxUnityWebPlayerAXLib.AxUnityWebPlayer get_unity()
        {
            return _Unity;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserControl1));
            _Unity = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(_Unity)).BeginInit();
            _Unity.OcxState = (AxHost.State)(resources.GetObject("_Unity.OcxState"));
            _Unity.TabIndex = 0;
            Controls.Add(_Unity);
            ((System.ComponentModel.ISupportInitialize)(_Unity)).EndInit();
            _Unity.src = AppDomain.CurrentDomain.BaseDirectory + @"\Unity.unity3d";
            AxHost.State state = _Unity.OcxState;
            _Unity.Dispose();

            // Create the unity web player object
            _Unity = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            ((System.ComponentModel.ISupportInitialize)(_Unity)).BeginInit();
            this.SuspendLayout();
            _Unity.Dock = DockStyle.Fill;
            _Unity.Name = "Unity";
            _Unity.OcxState = state;
            _Unity.TabIndex = 0;
            Controls.Add(_Unity);
            // 
            // axUnityWebPlayer1
            // 
            _Unity.AccessibleDescription = "Unity";
            _Unity.AccessibleName = "Unity";
            _Unity.Enabled = true;
            _Unity.Location = new System.Drawing.Point(0, 0);
            _Unity.Size = new System.Drawing.Size(1820, 980);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(_Unity);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1820, 980);
            ((System.ComponentModel.ISupportInitialize)(_Unity)).EndInit();
            this.Load += new System.EventHandler(this.Unity_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AxUnityWebPlayerAXLib.AxUnityWebPlayer _Unity;
    }
}
