using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GoodFoodSystem.BusinessLayer;
using GoodFoodSystem.DatabaseLayer;

namespace GoodFoodSystem.PresentationLayer
{
    public partial class EmployeeListingForm : Form
    {
        #region Variables
        public bool listFormClosed;
        private Collection<Employee> employees;
        private Role.RoleType roleValue;
        private EmployeeController employeeController;
        private FormStates state;
        private Employee employee;
        #endregion

        #region Enumeration
        public enum FormStates
        {
            View = 0,
            Add = 1,
            Edit = 2,
            Delete = 3
        }
        #endregion

        #region Constructor
        public EmployeeListingForm(EmployeeController empController)
        {
            InitializeComponent();
            employeeController = empController;
            this.Load += EmployeeListingForm_Load;
            this.Activated += EmployeeListingForm_Activated;
            this.FormClosed += EmployeeListingForm_FormClosed;
            state = FormStates.View;
        }
        #endregion

        #region Property Method       
        public Role.RoleType RoleValue
        {
            set
            {
                roleValue = value;
            }

        }
        #endregion

        #region The List View
        public void setUpEmployeeListView()
        {
            ListViewItem employeeDetails;   //Declare variables
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            //Clear current List View Control
            employeeListViews.Clear();
            //Set Up Columns of List View
            employeeListViews.Columns.Insert(0, "ID", 120, HorizontalAlignment.Left);
            employeeListViews.Columns.Insert(1, "EMPID", 120, HorizontalAlignment.Left);
            employeeListViews.Columns.Insert(2, "Name", 150, HorizontalAlignment.Left);
            employeeListViews.Columns.Insert(3, "Phone", 100, HorizontalAlignment.Left);            
            employees = null;                      //employees collection will be filled by role
            switch (roleValue)                     //  Check which role to add specific headings
            {
                case Role.RoleType.NoRole:
                    // Get all the employees from the EmployeeController object 
                    // (use the property) and assign to a local employees collection reference
                    employees = employeeController.AllEmployees;
                    listLabel.Text = "Listing of all employees";
                    employeeListViews.Columns.Insert(4, "Payment", 100, HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Headwaiter:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Headwaiter);
                    listLabel.Text = "Listing of all Headwaiters";
                    //Set Up Columns of List View
                    employeeListViews.Columns.Insert(4, "Salary", 100, HorizontalAlignment.Center);
                    break;
                //do for the others
                case Role.RoleType.Waiter:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Waiter);
                    listLabel.Text = "Listing of all Waiters";
                    //Set Up Columns of List View
                    employeeListViews.Columns.Insert(4, "Rate", 100, HorizontalAlignment.Center);
                    employeeListViews.Columns.Insert(5, "Number of Shifts", 100, HorizontalAlignment.Center);
                    employeeListViews.Columns.Insert(6, "Tips", 100, HorizontalAlignment.Center);
                    break;
                case Role.RoleType.Runner:
                    //Add a FindByRole method to the EmployeeController 
                    employees = employeeController.FindByRole(employeeController.AllEmployees, Role.RoleType.Runner);
                    listLabel.Text = "Listing of all Runners";
                    //Set Up Columns of List View
                    employeeListViews.Columns.Insert(4, "Rate", 100, HorizontalAlignment.Center);
                    employeeListViews.Columns.Insert(5, "Number of Shifts", 100, HorizontalAlignment.Center);
                    employeeListViews.Columns.Insert(6, "Tips", 100, HorizontalAlignment.Center);
                    break;
            }
            //Add employee details to each ListView item 
            foreach (Employee employee in employees)
            {
                employeeDetails = new ListViewItem();
                employeeDetails.Text = employee.ID.ToString();
                employeeDetails.SubItems.Add(employee.EmployeeID);
                employeeDetails.SubItems.Add(employee.Name);
                employeeDetails.SubItems.Add(employee.Telephone);

                switch (employee.role.getRoleValue)
                {
                    case Role.RoleType.Headwaiter:
                        headW = (HeadWaiter)employee.role;
                        employeeDetails.SubItems.Add(headW.SalaryAmount.ToString());
                        break;
                    case Role.RoleType.Waiter:
                        waiter = (Waiter)employee.role;
                        employeeDetails.SubItems.Add(waiter.getRate.ToString());
                        employeeDetails.SubItems.Add(waiter.getShifts.ToString());
                        employeeDetails.SubItems.Add(waiter.getTips.ToString());
                        break;
                    case Role.RoleType.Runner:
                        runner = (Runner)employee.role;
                        employeeDetails.SubItems.Add(runner.getRate.ToString());
                        employeeDetails.SubItems.Add(runner.getShifts.ToString());
                        employeeDetails.SubItems.Add(runner.getTips.ToString());
                        break;
                }
                employeeListViews.Items.Add(employeeDetails);
            }
            employeeListViews.Refresh();
            employeeListViews.GridLines = true;
        }
        #endregion

        #region Form Events
        private void EmployeeListingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFormClosed = true;
        }

        private void EmployeeListingForm_Load(object sender, EventArgs e)
        {
            employeeListViews.View = View.Details;
        }

        private void EmployeeListingForm_Activated(object sender, EventArgs e)
        {
            employeeListViews.View = View.Details;
            setUpEmployeeListView();
            ShowAll(false, roleValue);

        }

        #endregion

        #region Utility Methods
        private void ShowAll(bool value, Role.RoleType roleType)
        {
            idLabel.Visible = value;
            empIDLabel.Visible = value;
            nameLabel.Visible = value;
            phoneLabel.Visible = value;
            payLabel.Visible = value;
            tipsLabel.Visible = value;
            idTextBox.Visible = value;
            empIDTextBox.Visible = value;
            nameTextBox.Visible = value;
            phoneTextBox.Visible = value;
            paymentTextBox.Visible = value;
            tipsTextBox.Visible = value;
            //If the form state is View, the Submit button and the Edit button should not be visible
            if (state == FormStates.Delete)
            {
                cancelButton.Visible = !value;
                submitButton.Visible = !value;
            }
            else
            {
                cancelButton.Visible = value;
                submitButton.Visible = value;
            }
            deleteButton.Visible = value;
            editButton.Visible = value;

            if ((roleType == Role.RoleType.Waiter) || (roleType == Role.RoleType.Runner) && value)
            {
                shiftsLabel.Visible = value;
                shiftsTextBox.Visible = value;
                tipsLabel.Visible = value;
                tipsTextBox.Visible = value;
            }
            else
            {
                shiftsLabel.Visible = false;
                shiftsTextBox.Visible = false;
                tipsLabel.Visible = false;
                tipsTextBox.Visible = false;
            }

        }
        private void EnableEntries(bool value)
        {
            if ((state == FormStates.Edit) && value)
            {
                idTextBox.Enabled = !value;
                empIDTextBox.Enabled = !value;
            }
            else
            {
                idTextBox.Enabled = value;
                empIDTextBox.Enabled = value;
            }
            nameTextBox.Enabled = value;
            phoneTextBox.Enabled = value;
            paymentTextBox.Enabled = value;
            shiftsTextBox.Enabled = value;
            tipsTextBox.Enabled = value;
            if (state == FormStates.Delete)
            {
                cancelButton.Visible = !value;
                submitButton.Visible = !value;
            }
            else
            {
                cancelButton.Visible = value;
                submitButton.Visible = value;
            }
        }
        private void ClearAll()
        {
            idTextBox.Text = "";
            empIDTextBox.Text = "";
            nameTextBox.Text = "";
            phoneTextBox.Text = "";
            paymentTextBox.Text = "";
            shiftsTextBox.Text = "";
            tipsTextBox.Text = "";
        }
        private void PopulateTextBoxes(Employee emp)
        {
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            idTextBox.Text = emp.ID;          
            empIDTextBox.Text = emp.EmployeeID;
            nameTextBox.Text = emp.Name;
            phoneTextBox.Text = emp.Telephone;

            switch (emp.role.getRoleValue)
            {
                case Role.RoleType.Headwaiter:
                    headW = (HeadWaiter)(emp.role);
                    paymentTextBox.Text = Convert.ToString(headW.SalaryAmount);
                    break;
                case Role.RoleType.Waiter:
                    waiter = (Waiter)(emp.role);
                    paymentTextBox.Text = Convert.ToString(waiter.getRate);
                    shiftsTextBox.Text = Convert.ToString(waiter.getShifts);
                    tipsTextBox.Text = Convert.ToString(waiter.getTips);
                    break;
                case Role.RoleType.Runner:
                    runner = (Runner)(emp.role);
                    paymentTextBox.Text = Convert.ToString(runner.getRate);
                    shiftsTextBox.Text = Convert.ToString(runner.getShifts);
                    tipsTextBox.Text = Convert.ToString(runner.getTips);
                    break;
            }
        }
        private void PopulateObject(Role.RoleType roleType)
        {
            HeadWaiter headW;
            Waiter waiter;
            Runner runner;
            employee = new Employee(roleType);
            employee.ID = idTextBox.Text;
            employee.EmployeeID = empIDTextBox.Text;
            employee.Name = nameTextBox.Text;
            employee.Telephone = phoneTextBox.Text;

            switch (employee.role.getRoleValue)
            {
                case Role.RoleType.Headwaiter:
                    headW = (HeadWaiter)(employee.role);
                    headW.SalaryAmount = decimal.Parse(paymentTextBox.Text);
                    break;
                case Role.RoleType.Waiter:                    
                    waiter = (Waiter)(employee.role);
                    waiter.getRate = decimal.Parse(paymentTextBox.Text);
                    waiter.getShifts = int.Parse(shiftsTextBox.Text);
                    waiter.getTips = decimal.Parse(tipsTextBox.Text);                    
                    break;
                case Role.RoleType.Runner:                    
                    runner = (Runner)(employee.role);
                    runner.getRate = decimal.Parse(paymentTextBox.Text);
                    runner.getShifts = int.Parse(shiftsTextBox.Text);
                    runner.getTips = decimal.Parse(tipsTextBox.Text);
                    break;
            }
        }

        #endregion

        private void employeeListViews_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAll(true, roleValue) ;
            state = FormStates.View;
            EnableEntries(false);
            if (employeeListViews.SelectedItems.Count > 0)   // if you selected an item 
            {
              employee = employeeController.Find(employeeListViews.SelectedItems[0].Text);  //selected employee becoms current employee
             //employee = employeeController.Find(Convert.ToString(employeeListViews.SelectedItems[0]));  //selected employee becomes current employee
                PopulateTextBoxes(employee);
                
                }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            //set the form state to Edit
            state = FormStates.Edit;
            deleteButton.Visible = false;
            EnableEntries(true);//call the EnableEntities method

        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            PopulateObject(employee.role.getRoleValue);
            if (state == FormStates.Edit)
            {
                employeeController.DataMaintenance(employee, DB.DBOperation.Edit);

            }
            else//delete code
            {
                employeeController.DataMaintenance(employee, DB.DBOperation.Delete);
            }
            employeeController.FinalizeChanges(employee);
            ClearAll();
            state = FormStates.View;
            ShowAll(false, roleValue);
            setUpEmployeeListView();   //refresh List View
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            //set the form state to Delete
            state = FormStates.Delete;
            editButton.Visible = false;
            EnableEntries(false);//call the EnableEntities method
        }
    }
}
