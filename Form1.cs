using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDo_Reminder
{
    public partial class Form1 : Form
    {
        private TaskManager taskManager;
        string fileName = Application.StartupPath + "\\Tasks.txt";
    
        public Form1()
        {
            InitializeComponent();
            InitializeGUI();
        }

        private void InitializeGUI()
        {
            this.Text = "ToDo Reminder by Matias";

            taskManager = new TaskManager();

            // Gets the clock working
            clockTimer = new Timer();
            clockTimer.Interval = 1000; 
            clockTimer.Tick += new EventHandler(clockTimer_Tick); 
            clockTimer.Start();

            // Clears everything when starting the program
            cmbPriority.Items.Clear();
            cmbPriority.Items.AddRange(Enum.GetNames(typeof(PriorityType)));
            cmbPriority.SelectedIndex = (int)PriorityType.Normal;

            lstTasks.Items.Clear();
            lblClock.Text = String.Empty;
            
            txtDescription.Text = String.Empty;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd   HH:mm";



            // Set tooltips for various controls when hovering over them
            toolTip1.SetToolTip(dateTimePicker1, "Click to open calender");
            toolTip1.SetToolTip(cmbPriority, "Select the priority");
            toolTip1.SetToolTip(txtDescription, "Write something here!");
            toolTip1.SetToolTip(lstTasks, "Your tasks");

            toolTip1.ShowAlways = true;

            menuFileOpen.Enabled = true;
            menuFileSave.Enabled = true;
              
        }


        // Method to read user input and create a new Task object
        private Task ReadInput()
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                MessageBox.Show("Please write something!");
                return null;
            }

            var task = new Task
            {
                Description = txtDescription.Text,
                TaskDate = dateTimePicker1.Value,
                Priority = (PriorityType)cmbPriority.SelectedIndex
            };

            return task;
        }


        // Method to update the GUI particularly the task list.
        private void UpdateGUI()
        {
            lstTasks.Items.Clear();

            var infoStrings = taskManager.GetInfoStringList();
            if (infoStrings?.Length > 0)
            {
                lstTasks.Items.AddRange(infoStrings);
            }
        }


        // Event handler for the clock timer
        private void clockTimer_Tick(object sender, EventArgs e)
        {
            lblClock.Text = DateTime.Now.ToString("HH:mm:ss"); // Format time as HH:mm:ss
        }
        #region Menu events
        private void menuFileSave_Click(object sender, EventArgs e)
        {
            // Error message in case something goes wrong during the save operation
            string errMessage = "Something went wrong!";

            // Attempt to save the task data to a file
            bool ok = taskManager.WriteDataToFile(fileName);
            if (!ok)
                MessageBox.Show(errMessage);
            else
                MessageBox.Show("Data saved to file" + Environment.NewLine + fileName);

        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {
            string errMessage = "Something went wrong with opening the file!";

            // Attempt to read task data from the file
            bool ok = taskManager.ReadDataFromFile(fileName);
            if (!ok)
                MessageBox.Show(errMessage);
            else
                // If the read operation is successful update the GUI with the task form text file
                UpdateGUI();
        }

        // Event for handling when you press to exit the program
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("Are you sure you want to exit the program?",
                                                     "Think twice",
                                                     MessageBoxButtons.OKCancel);

            if (dlgResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void menuFileNew_Click(object sender, EventArgs e)
        {
            InitializeGUI();
        }
        #endregion
        // Event handler for the 'OK' button click event then updates the gui
        private void btnOK_Click(object sender, EventArgs e)
        {
            Task task = ReadInput();
            if(taskManager.AddNewTask(task))
            {
                UpdateGUI();
            }

        }

        #region Unused
        private void dlgResult_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        #endregion

    }
}
