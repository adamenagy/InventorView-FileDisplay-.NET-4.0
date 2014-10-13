using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using AxInventorViewControlLib;

using Inventor;

namespace CSharpFileDisplaySample
{
  /// <summary>
  /// Summary description for Form1.
  /// </summary>
  public class Form1 : System.Windows.Forms.Form
  {
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.Button buttonBrowse;
    private System.Windows.Forms.TextBox textBoxFileName;
    private System.Windows.Forms.Label label1;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.Container components = null;

    // added manually
    private ApprenticeServerComponent m_oserver;
    private ApprenticeServerDocument m_odocument;
    private ApprenticeServerDrawingDocument m_odrawingDocument;
    private ClientView m_oview;
    private Camera m_ocamera;
    private Point2d m_previousPoint;
    private AxInventorViewControlLib.AxInventorViewControl axInventorView1;
    private bool m_bmouseDown;

    public void AddInventorPath()
    {
      // Note: System.Environment.Is64BitProcess and
      // System.Environment.Is64BitProcess were
      // introduced in .NET Framework 4.0
      string path = System.Environment.GetEnvironmentVariable("PATH");

      // In case process and OS bitness match it's
      // C:\Program Files\Autodesk\Inventor 2015
      // otherwise it's
      // C:\Program Files\Autodesk\Inventor 2015\bin
      string inventorPath = m_oserver.InstallPath;

      if (System.Environment.Is64BitOperatingSystem &&
          !System.Environment.Is64BitProcess)
      {
        // If you are running the app as a 32 bit process 
        // on a 64 bit OS then you'll need this
        path += ";" + inventorPath + "Bin32";
      }
      else
      {
        // Otherwise you need this
        path += ";" + inventorPath + "Bin";
      }

      System.Environment.SetEnvironmentVariable("PATH", path);
    }

    public Form1()
    {
      // Try to create an instance of apprentice server
      try
      {
        m_oserver = new ApprenticeServerComponent();
        AddInventorPath();
      }
      catch (SystemException exception)
      {
        MessageBox.Show(this,
          "Failed to create an instance of Apprentice server.",
          "CSharpFileDisplaySample",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }

      m_odocument = null;
      m_oview = null;
      m_ocamera = null;
      m_bmouseDown = false;

      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();
    }

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.buttonBrowse = new System.Windows.Forms.Button();
      this.textBoxFileName = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.axInventorView1 = new AxInventorViewControlLib.AxInventorViewControl();
      ((System.ComponentModel.ISupportInitialize)(this.axInventorView1)).BeginInit();
      this.SuspendLayout();
      // 
      // pictureBox1
      // 
      this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.pictureBox1.Location = new System.Drawing.Point(8, 8);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new System.Drawing.Size(368, 320);
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_OnMouseUp);
      this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_OnMouseMove);
      this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_OnMouseDown);
      // 
      // buttonBrowse
      // 
      this.buttonBrowse.Location = new System.Drawing.Point(680, 336);
      this.buttonBrowse.Name = "buttonBrowse";
      this.buttonBrowse.Size = new System.Drawing.Size(88, 24);
      this.buttonBrowse.TabIndex = 1;
      this.buttonBrowse.Text = "Browse";
      this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
      // 
      // textBoxFileName
      // 
      this.textBoxFileName.Location = new System.Drawing.Point(80, 336);
      this.textBoxFileName.Name = "textBoxFileName";
      this.textBoxFileName.Size = new System.Drawing.Size(592, 20);
      this.textBoxFileName.TabIndex = 2;
      this.textBoxFileName.Text = "";
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 336);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(72, 16);
      this.label1.TabIndex = 3;
      this.label1.Text = "Inventor file:";
      // 
      // axInventorView1
      // 
      this.axInventorView1.Enabled = true;
      this.axInventorView1.Location = new System.Drawing.Point(392, 8);
      this.axInventorView1.Name = "axInventorView1";
      this.axInventorView1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axInventorView1.OcxState")));
      this.axInventorView1.Size = new System.Drawing.Size(376, 320);
      this.axInventorView1.TabIndex = 4;
      // 
      // Form1
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(784, 374);
      this.Controls.Add(this.axInventorView1);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBoxFileName);
      this.Controls.Add(this.buttonBrowse);
      this.Controls.Add(this.pictureBox1);
      this.Name = "Form1";
      this.Text = "Inventor File Display C# Demo";
      this.Load += new System.EventHandler(this.Form1_Load);
      ((System.ComponentModel.ISupportInitialize)(this.axInventorView1)).EndInit();
      this.ResumeLayout(false);

    }
    #endregion

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    /// 
    [STAThread]
    static void Main()
    {
      System.Windows.Forms.Application.Run(new Form1());
    }

    private void buttonBrowse_Click(object sender, System.EventArgs e)
    {
      OpenFileDialog fdlg = new OpenFileDialog();
      fdlg.Title = "C# Inventor Open File Dialog";
      fdlg.InitialDirectory = @"c:\program files\autodesk\inventor 2013\samples\models\";
      fdlg.Filter = "Inventor files (*.ipt; *.iam; *.idw)|*.ipt;*.iam;*.idw";
      fdlg.FilterIndex = 2;
      fdlg.RestoreDirectory = true;
      if (fdlg.ShowDialog() == DialogResult.OK)
      {
        // get the file name
        textBoxFileName.Text = fdlg.FileName;
        m_odocument = m_oserver.Open(textBoxFileName.Text);

        // if drawing document, get the first sheet and from it the client views collection, then create a client view
        if (m_odocument.DocumentType == DocumentTypeEnum.kDrawingDocumentObject)
        {
          m_odrawingDocument = (Inventor.ApprenticeServerDrawingDocument)m_odocument;

          m_oview = m_odrawingDocument.Sheets[1].ClientViews.Add(pictureBox1.Handle.ToInt32());

          m_ocamera = m_oview.Camera;
          m_ocamera.Perspective = false;
        }
        // if part or assembly get the client views collection from the document, then create a client view
        else
        {
          m_oview = m_odocument.ClientViews.Add(pictureBox1.Handle.ToInt32());

          m_ocamera = m_oview.Camera;
          m_ocamera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation;
        }

        m_ocamera.Fit();
        m_ocamera.Apply();

        m_oview.Update(false);

        // using the control to display the file 
        axInventorView1.FileName = fdlg.FileName;
      }
    }
    protected void pictureBox1_OnMouseDown(object o, MouseEventArgs e)
    {
      if (e.Button == MouseButtons.Left && m_oserver != null)
      {
        m_bmouseDown = true;
        m_previousPoint = m_oserver.TransientGeometry.CreatePoint2d(e.X, e.Y);
      }
      base.OnMouseDown(e);
    }

    protected void pictureBox1_OnMouseMove(object o, MouseEventArgs e)
    {
      if (m_bmouseDown == true && m_oserver != null && m_oview != null)
      {
        m_ocamera = m_oview.Camera;
        Point2d opoint = m_oserver.TransientGeometry.CreatePoint2d(e.X, e.Y);
        m_ocamera.ComputeWithMouseInput(m_previousPoint, opoint, 0, ViewOperationTypeEnum.kRotateViewOperation);

        m_previousPoint = opoint;
        m_ocamera.Apply();
        m_oview.Update(true);
      }
      base.OnMouseMove(e);
    }

    protected void pictureBox1_OnMouseUp(object o, MouseEventArgs e)
    {
      if (m_oview != null)
      {
        m_oview.Update(false);
        m_bmouseDown = false;
      }
      base.OnMouseUp(e);
    }

    private void Form1_Load(object sender, System.EventArgs e)
    {

    }
  }
}
