﻿/*
    *Group Members: Martin Barber and Auriana Hayles
    * Student ID's:	100368442			100832275
    * Class: OOP 3200 - 07
    * Date: Started Sept 20th, Finished Friday Sept 30th.
    * File Name: Lab_2(Main.cpp)
    * GitHub: https://github.com/Ritz-Bitz13/OOP__Week04/tree/main/Lab_2
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab1_CODE_IT_INC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private List<String> Status = new List<String>();
        string FileName;
        string displayProject;
        double displaybudget;
        double displaySpent;
        double displayHours;
        string displayStatus;
        bool validation1 = true;
        bool validation2 = true;
        bool validation3 = true;
        bool validation4 = true;
        bool validation5 = true;


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Program proj = new Program();
            

            //Check project name string length
            if (txtProjectName.Text.Length >= 3)
            {
                // If name of project is 3 or more characters, save project name.
                validation1 = true;
                proj.ProjectName = this.txtProjectName.Text.Trim();
            }
            else
            {
                // If the Project name is too short, fail and alert the user.
                MessageBox.Show("Error - Project name too short, Please enter at least 3 characters.",
                    "Error - Project name");
                txtProjectName.Clear();
                txtProjectName.Focus();
                validation1 = false;
            }

            // Check Budget Textbox
            if (CheckDouble(txtBudget.Text))
            {
                // If budget is entered properly and passed validation, save info to class list
                validation2 = true;
                proj.TheBudget = Convert.ToDouble(txtBudget.Text.Trim());
            }
            else
            {
                // If budget has string characters in the textbox, fail and alert user
                MessageBox.Show("Error - Budget is not a proper number. Please try again",
                    "Error - Project Budget");
                txtBudget.Clear();
                txtBudget.Focus();
                validation2 = false;
            }

            // Check the spent textbox
            if (CheckDouble(txtSpent.Text))
            {
                // If entered correctly
                validation3 = true;
                proj.AmountSpent = Convert.ToDouble(txtSpent.Text.Trim());
            }
            else
            {
                // If Spent textbox has string characters, fail validation and notify user.
                MessageBox.Show("Error - Your input for 'spent' is not a proper number. Please try again." ,
                    "Error - Project Spent");
                txtSpent.Clear();
                txtSpent.Focus();
                validation3 = false;
            }

            // Check Hours textbox
            if (CheckDouble(txtHoursRemaining.Text))
            {
                // If hours is entered correctly, save information.
                validation4 = true;
                proj.HoursRemaining = Convert.ToDouble(txtHoursRemaining.Text.Trim());
            }
            else
            {
                // If hours txtbox has string characters show fail.
                MessageBox.Show("Error - Your hours are not a number. Please try again.",
                    "Error - Project Hours");
                txtHoursRemaining.Clear();
                txtHoursRemaining.Focus();
                validation4 = false;
                
            }

            // If combo box has nothing selected, fail and show it is blank
            if (cboStatus.Text.Length == 0)
            {
                validation5 = false;
                MessageBox.Show("Error - Status is blank, Please select one of the Status.", "Error Status");
            }
            else
            {
                // If combo box is selected save info
                validation5 = true;
                proj.ProjectStatus = cboStatus.Text.Trim();
            }


            if (validation1 && validation2 && validation3 && validation4 && validation5)
            {
                Program.Projects.Add(proj);
                // Clears old filename to make new filename
                FileName = "";
                FileName = proj.ProjectName;


                Status.Add(proj.ToString());
                lbxProjects.Items.Add(FileName);
                SetDefaults();
            }
            
        }

        public void SelectedAnItem(object sender, EventArgs e)
        {
            if (lbxProjects.SelectedItem == null)
            {
                MessageBox.Show("Error, nothing has been selected, Please select a Project.",
                    "Error, Project Selection.");
            }
            else
            {
                string temp;
                temp = lbxProjects.SelectedItem.ToString();
                Populate(temp);
            }
            // If item is double clicked on the listbox, retreive information and display it.
            
        }
        
        #region Custom Functions

        private void SetDefaults()
        {
            // Clear all textboxes to be ready for the next project.
            txtProjectName.Clear();
            txtBudget.Clear();
            txtSpent.Clear();
            txtHoursRemaining.Clear();
            cboStatus.Text = "";
            txtProjectName.Focus();
        }

        private void Populate(string name)
        {
            // Looks to see if the project name exists.
            Program SelectProject = Program.Find(name);

            //THIS IS FOR THE MESSAGEBOX IF YOU DOUBLE CLICK THE FILE IN LISTBOX
            displayProject = SelectProject.ProjectName;
            displaybudget = SelectProject.TheBudget;
            displaySpent = SelectProject.AmountSpent;
            displayHours = SelectProject.HoursRemaining;
            displayStatus = SelectProject.ProjectStatus;

            // This is the messagebox that is receiving the information to display in the message box.
            MessageBox.Show(("Project Name: " + displayProject + "\nBudget: $" + displaybudget + "\nSpent: $" +
                             displaySpent + "\nHours Remaining: " + displayHours + "\nStatus: " + displayStatus), "Project Information");

        }

        public bool CheckDouble(string value)
        {
            // Variables for the validation
            bool isDouble = true;
            double tempNum;
            // Test if you can turn the input into a double
            if (double.TryParse(value, out tempNum))
            {
                // Once confirmed number works, is it above 0?
                if (tempNum > 0.0)
                {
                    // If its above zero, show its true and return the double
                    isDouble = true;
                    return isDouble;
                }
                else
                {
                    // If number is negative show false
                    isDouble = false;
                    return isDouble;
                }
            }
            else
            {
                // If there is string characters, show false.
                isDouble = false;
                return isDouble;
            }
        }


        #endregion

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbxProjects.SelectedItem == null)
            {
                // If nothing is selected, Show an error
                MessageBox.Show("Error, Please select a project to edit.", "Error");
            }
            else
            {
                {
                    string temp;
                    temp = lbxProjects.SelectedItem.ToString();

                    Program SelectProject = Program.Find(temp);

                    EditPage window = new EditPage();
                    window.project = SelectProject.ProjectName;
                    window.budget = SelectProject.TheBudget;
                    window.spent = SelectProject.AmountSpent;
                    window.hours = SelectProject.HoursRemaining;
                    window.status = SelectProject.ProjectStatus;
                    window.ShowDialog();
                }
            }
        }
    }
}