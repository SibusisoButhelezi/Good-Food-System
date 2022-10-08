using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoodFoodSystem.BusinessLayer;

namespace GoodFoodSystem.PresentationLayer
{
    public partial class EmployeeMDIParent1 : Form
    {
        #region Variables
        private int childFormNumber = 0;
        private EmployeeForm employeeForm;
        private EmployeeListingForm employeeListForm;
        private EmployeeController employeeController;
        #endregion

        #region Constructor
        public EmployeeMDIParent1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            employeeController = new EmployeeController();
        }

        #endregion

        #region Child forms
        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        #endregion

        #region ToolstripMenus 
        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Employee ToolStrip Menus for Listing 
        private void listAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.NoRole;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }

        private void listHeadWaitersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Headwaiter;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }

        private void listWaitronsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Waiter;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }

        private void listRunnersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeListForm == null)
            {
                CreateNewEmployeeListForm();
            }
            if (employeeListForm.listFormClosed)
            {
                CreateNewEmployeeListForm();
            }
            employeeListForm.RoleValue = Role.RoleType.Runner;
            employeeListForm.setUpEmployeeListView();
            employeeListForm.Show();
        }

        private void addEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (employeeForm == null)
            {
                CreateNewEmployeeForm();
            }
            if (employeeForm.employeeFormClosed)
            {
                CreateNewEmployeeForm();
            }
            employeeForm.Show();

        }
        #endregion

        #region Create a New ChildForm 
        public void CreateNewEmployeeForm()
        {
            employeeForm = new EmployeeForm(employeeController);
            employeeForm.MdiParent = this;//Setting the MDI Parent
            employeeForm.StartPosition = FormStartPosition.CenterParent;
        }

        public void CreateNewEmployeeListForm()
        {
            employeeListForm = new EmployeeListingForm(employeeController);
            employeeListForm.MdiParent = this;        // Setting the MDI Parent
            employeeListForm.StartPosition = FormStartPosition.CenterParent;
        }
        #endregion


    }
}
