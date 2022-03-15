﻿using SomerenLogic;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        public SomerenUI()
        {
            InitializeComponent();
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        //method that shows a specific panel
        private void showPanel(string panelName)
        {

            switch(panelName){
                case "Dashboard":
                    hideAll();

                    pnlDashboard.Show();
                    imgDashboard.Show();
                    break;

                case "Students":
                    hideAll();
                    AddStudentsTolist();
                    pnlStudents.Show();
                    break;

                case "Rooms":
                    hideAll();
                    AddRoomsToList();
                    pnlRooms.Show();
                    break;

                case "Teacher":
                    hideAll();
                    AddTeachersToList();
                    lecturers_panel.Show();
                    break;

                case "Drinks":
                    hideAll();
                    AddDrinksToList();
                    pnlDrinks.Show();
                break;
                case "Checkout":
                    hideAll();
                    CheckoutPannel.Show();


                    break;

                default:
                    hideAll();
                    break;
            }
        }

        //hides all pannels
        private void hideAll()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel) c.Visible = false;
            }
        }

        //method that returns a list of icons from the img filepath
        private ImageList GetDrinkIcons()
        {
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(90, 40);
            string[] filePaths = Directory.GetFiles("../../../img");

            foreach (string filePath in filePaths)
            {
                imgs.Images.Add(Image.FromFile(filePath));
            }

            return imgs;
        }

        //clear all textboxes in the drinkspanel
        private void ClearDrinksTxtBoxes()
        {
            txtName.Clear();
            txtSalesPrice.Clear();
            txtStock.Clear();
            txtAlcoholic.Clear();
            txtNrOfSales.Clear();
        }

        //check if the all textboxes in the drinks panel are filled
        private bool DrinksBoxesFilled()
        {
            foreach (Control control in pnlDrinks.Controls)
            {
                if (control is TextBox)
                {
                    if (String.IsNullOrWhiteSpace(control.Text) || String.IsNullOrEmpty(control.Text))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //get a Drink object from the textboxes
        private Drink GetDrinkFromTxtBoxes()
        {
            Drink drink = new Drink()
            {
                Name = txtName.Text,
                Stock = int.Parse(txtStock.Text),
                SalesPrice = decimal.Parse(txtSalesPrice.Text),
                Alcoholic = Convert.ToBoolean(txtAlcoholic.Text),
                NrOfSales = int.Parse(txtNrOfSales.Text)
            };
            return drink;
        }

        //if an item in the Drinks ListView is selected fill the textboxes with the values that belong to the selected Drink
        private void listViewDrinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this is to prevent the code running into exceptions when selecting an item for the second time because when you select an item for the second time
                //c# will first deselect the previous row so there will be a call without a selected item creating an invald argument exception
                if (listViewDrinks.SelectedItems.Count == 0)
                    return;
                //get the selected Drink
                Drink lsDrink = (Drink)listViewDrinks.SelectedItems[0].Tag;
                //put the selected Drink values into the textboxes
                txtName.Text = lsDrink.Name;
                txtSalesPrice.Text = Convert.ToString(lsDrink.SalesPrice);
                txtStock.Text = Convert.ToString(lsDrink.Stock);
                txtAlcoholic.Text = Convert.ToString(lsDrink.Alcoholic);
                txtNrOfSales.Text = Convert.ToString(lsDrink.NrOfSales);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong while selecting a drink: " + ex.Message);
            }
        }

        private void AddDrinksToList()
        {
            try
            {
                // fill the students listview within the students panel with a list of students
                DrinkService drinkService = new DrinkService();
                List<Drink> drinksList = drinkService.GetDrinks();

                // clear the listview before filling it again
                listViewDrinks.Items.Clear();
                listViewDrinks.SmallImageList = GetDrinkIcons();

                //foreach drink in the list of drinks make one row in the Drinks ListView
                foreach (Drink d in drinksList)
                {
                    ListViewItem li = new ListViewItem();
                    li.SubItems.Add(d.Name);
                    li.SubItems.Add(Convert.ToString(d.SalesPrice));
                    li.SubItems.Add(Convert.ToString(d.Stock));
                    //display the 'Stock nearly depleted' icon when the stock is below 10
                    if (d.Stock < 10)
                    {
                        li.ImageIndex = 0;
                    }
                    //display the 'Stock sufficient' icon when the stock is sufficient (above or equal to 10)
                    else
                    {
                        li.ImageIndex = 1;
                    }
                    li.Tag = d;
                    listViewDrinks.Items.Add(li);
                }
                listViewDrinks.View = View.Details;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
            }
        }

        private void AddTeachersToList()
        {
            try
            {
                // fill the students listview within the students panel with a list of students
                TeacherService teacherService = new TeacherService();
                List<Teacher> TeacherList = teacherService.GetTeachers();

                // clear the listview before filling it again
                teatcherListView.Items.Clear();

                foreach (Teacher t in TeacherList)
                {
                    ListViewItem li = new ListViewItem(Convert.ToString(t.Number));
                    li.SubItems.Add(t.FirstName);
                    li.SubItems.Add(t.LastName);
                    teatcherListView.Items.Add(li);
                }
                teatcherListView.View = View.Details;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
            }
        }
        private void AddStudentsTolist()
        {
            try
            {
                // fill the students listview within the students panel with a list of students
                StudentService studService = new StudentService(); 
                List<Student> studentList = studService.GetStudents(); 

                // clear the listview before filling it again
                listViewStudents.Items.Clear();

                foreach (Student s in studentList)
                {
                    ListViewItem li = new ListViewItem(Convert.ToString(s.Id));
                    li.SubItems.Add(s.FirstName);
                    li.SubItems.Add(s.LastName);
                    listViewStudents.Items.Add(li);
                }
                listViewStudents.View = View.Details;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }
        private void AddRoomsToList()
        {
            try
            {
                RoomService roomService = new RoomService();
                List<Room> roomList = roomService.GetRooms();

                listViewRooms.Items.Clear();

                foreach (Room r in roomList)
                {
                    ListViewItem li = new ListViewItem(r.Type);
                    li.SubItems.Add(Convert.ToString(r.Capacity));
                    li.SubItems.Add(Convert.ToString(r.Number));
                    listViewRooms.Items.Add(li);
                }
                listViewRooms.View = View.Details;
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
            }
        }
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void imgDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }

        //shows  teacher when the lecturer strip button is pressed
        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Teacher");
        }

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //show the drink panel when the drinks toolstrip menu item is clicked
        private void drinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Drinks");
        }

        //button to clear all textboxes in the Drinks panel
        private void btnClearDrinksTxtBoxes_Click(object sender, EventArgs e)
        {
            ClearDrinksTxtBoxes();
        }

        //add button for Drinks
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new DrinkService object
                DrinkService drinkService = new DrinkService();
                //if a textbox of Drinks isn't filled say that all textboxes must be filled
                if (DrinksBoxesFilled())
                {
                    MessageBox.Show("Please fill all textboxes!");
                    return;
                }
                //make a new Drink object with all values from the textboxes
                Drink drink = GetDrinkFromTxtBoxes();
                //add a drink to the Drinks database
                drinkService.AddDrink(drink);
                //reload the Drinks in the ListView
                AddDrinksToList();
                //Clear all textboxes in the Drinks panel
                ClearDrinksTxtBoxes();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong while adding a drink: " + exception.Message);
            }
        }

        //update button for Drinks
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new DrinkService object
                DrinkService drinkService = new DrinkService();
                //if a row wasn't selected say that a row must be selected
                if (listViewDrinks.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a row before updating one");
                    return;
                }
                //if a textbox of Drinks isn't filled say that all textboxes must be filled
                if (DrinksBoxesFilled())
                {
                    MessageBox.Show("Please fill all textboxes!");
                    return;
                }
                //make a new Drink object with all values from the textboxes
                Drink drink = GetDrinkFromTxtBoxes();
                //update the selected drink in the Drinks database
                drinkService.UpdateDrink((Drink)listViewDrinks.SelectedItems[0].Tag, drink);
                //reload the Drinks in the ListView
                AddDrinksToList();
                //Clear all textboxes in the Drinks panel
                ClearDrinksTxtBoxes();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong while updating a drink: " + exception.Message);
            }
        }

        //delete button for Drinks
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new DrinkService object
                DrinkService drinkService = new DrinkService();
                //if a row wasn't selected say that a row must be selected
                if (listViewDrinks.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Please select a row before deleting one");
                    return;
                }
                if (MessageBox.Show("Are you sure you want to delete this row?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) { return; }
                //update the selected drink in the Drinks database
                drinkService.DeleteDrink((Drink)listViewDrinks.SelectedItems[0].Tag);
                //reload the Drinks in the ListView
                AddDrinksToList();
                //Clear all textboxes in the Drinks panel
                ClearDrinksTxtBoxes();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong while deleting a drink: " + exception.Message);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkoutToolStripItem_Click(object sender, EventArgs e)
        {
            showPanel("Checkout");

        }
    }
}
