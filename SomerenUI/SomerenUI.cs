using SomerenLogic;
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
using ErrorHandlers;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        private User user;
        public SomerenUI(User user)
        {
            this.user = user;
            InitializeComponent();
        }
        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }

        //==========GENERAL CODE==========

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

                case "Teachers":
                    hideAll();
                    AddTeachersToList();
                    lecturers_panel.Show();
                    break;

                case "Drinks":
                    hideAll();
                    AddDrinksToList();
                    pnlDrinks.Show();
                    break;

                case "Activities":
                    hideAll();
                    dateTimeStart.CustomFormat = "dd-MM-yyyy HH:mm:ss";
                    dateTimeEnd.CustomFormat = "dd-MM-yyyy HH:mm:ss";
                    AddActivitiesToList();
                    pnlActivities.Show();
                    break;

                default:
                    hideAll();
                    break;
            }
        }
        //exit
        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //log out
        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginUI loginUI = new LoginUI();
            this.Hide();
            loginUI.ShowDialog();
            this.Close();
        }
        //hides all pannels
        private void hideAll()
        {
            foreach (Control c in this.Controls)
            {
                if (c is Panel) c.Visible = false;
            }
        }

        //==========DASHBOARD CODE==========

        private void dashboardToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }
        private void imgDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        //==========STUDENTS CODE==========

        private void studentsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showPanel("Students");
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
                ErrorLogger.WriteLogToFile(e.Message);
                MessageBox.Show("Something went wrong while loading the students: " + e.Message);
            }
        }
        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //==========LECTURERS CODE==========

        //shows  teacher when the lecturer strip button is pressed
        private void lecturersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showPanel("Teachers");
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
                ErrorLogger.WriteLogToFile(e.Message);
                MessageBox.Show("Something went wrong while loading the teachers: " + e.Message);
            }
        }


        //==========ROOMS CODE==========

        private void roomsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showPanel("Rooms");
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
                ErrorLogger.WriteLogToFile(e.Message);
                MessageBox.Show("Something went wrong while loading the rooms: " + e.Message);
            }
        }

        //==========DRINKS CODE==========

        //add drinks to the ListView
        private void AddDrinksToList()
        {
            try
            {
                // fill the drinks listview within the drinks panel with a list of drinks
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
                ErrorLogger.WriteLogToFile(e.Message);
                MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
            }
        }
        //show the drink panel when the drinks toolstrip menu item is clicked
        private void drinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Drinks");
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
                ErrorLogger.WriteLogToFile(ex.Message);
                MessageBox.Show("Something went wrong while selecting a drink: " + ex.Message);
            }
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
                    throw new SomerenException("Please fill all textboxes!");
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
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
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
                    throw new SomerenException("Please select a row before updating one");
                }
                //if a textbox of Drinks isn't filled say that all textboxes must be filled
                if (DrinksBoxesFilled())
                {
                    throw new SomerenException("Please fill all textboxes!");
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
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
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
                    throw new SomerenException("Please select a row before deleting one");
                }
                if (MessageBox.Show("Are you sure you want to delete this drink?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) { return; }
                //update the selected drink in the Drinks database
                drinkService.DeleteDrink((Drink)listViewDrinks.SelectedItems[0].Tag);
                //reload the Drinks in the ListView
                AddDrinksToList();
                //Clear all textboxes in the Drinks panel
                ClearDrinksTxtBoxes();
            }
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
                MessageBox.Show("Something went wrong while deleting a drink: " + exception.Message);
            }
        }
        //button to clear all textboxes in the Drinks panel
        private void btnClearDrinksTxtBoxes_Click(object sender, EventArgs e)
        {
            ClearDrinksTxtBoxes();
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

        //==========ACTIVITIES CODE==========

        //add activities to the ListView
        private void AddActivitiesToList()
        {
            try
            {
                // fill the activity listview within the activity panel with a list of activities
                ActivityService activityService = new ActivityService();
                List<Activity> activitiesList = activityService.GetActivities();

                // clear the listview before filling it again
                listViewActivities.Items.Clear();

                //foreach activity in the list of activities make one row in the activity ListView
                foreach (Activity a in activitiesList)
                {
                    ListViewItem li = new ListViewItem(a.Name);
                    li.SubItems.Add(a.Location);
                    li.SubItems.Add(a.StartDate.ToString("dd-MM-yyyy HH:mm:ss"));
                    li.SubItems.Add(a.EndDate.ToString("dd-MM-yyyy HH:mm:ss"));
                    li.Tag = a;
                    listViewActivities.Items.Add(li);
                }
                listViewActivities.View = View.Details;
            }
            catch (Exception e)
            {
                ErrorLogger.WriteLogToFile(e.Message);
                MessageBox.Show("Something went wrong while loading the activities: " + e.Message);
            }
        }
        //show the activity panel when the activities toolStrip menu item is clicked
        private void activitiesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            showPanel("Activities");
        }
        //if an item in the Activities ListView is selected fill the textboxes with the values that belong to the selected Activity
        private void listViewActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //this is to prevent the code running into exceptions when selecting an item for the second time because when you select an item for the second time
                //c# will first deselect the previous row so there will be a call without a selected item creating an invald argument exception
                if (listViewActivities.SelectedItems.Count == 0)
                    return;
                //get the selected activity
                Activity lsActivity = (Activity)listViewActivities.SelectedItems[0].Tag;
                //put the selected activity values into the textboxes
                txtActivityName.Text = lsActivity.Name;
                txtActivityLocation.Text = lsActivity.Location;
                dateTimeStart.Value = lsActivity.StartDate;
                dateTimeEnd.Value = lsActivity.EndDate;
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteLogToFile(ex.Message);
                MessageBox.Show("Something went wrong while selecting an activity: " + ex.Message);
            }
        }

        //add button for activities
        private void btnActivityAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new ActivityService object
                ActivityService activityService = new ActivityService();
                //if a textbox of Activities isn't filled say that all textboxes must be filled
                if (ActivityBoxEmpty())
                {
                    throw new SomerenException("Please fill all textboxes!");
                }
                //if the entered activity name has already been added say that the activity has already been added
                if (ActivityNameWasAdded(txtActivityName.Text))
                {
                    throw new SomerenException("This activity has already been added!");
                }
                //if the start date is before the end date say that the start date can't be before the end date
                if (dateTimeStart.Value > dateTimeEnd.Value)
                {
                    throw new SomerenException("The start date can't be before the end date!");
                }
                //make a new Activity object with all values from the textboxes
                Activity activity = GetActivityFromTxtBoxes();
                //add an activity to the Activities database
                activityService.AddActivity(activity);
                //reload the activities in the ListView
                AddActivitiesToList();
                //Clear all textboxes in the activities panel
                ClearActivityTxtBoxes();
            }
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
                MessageBox.Show("Something went wrong while adding an activity: " + exception.Message);
            }
        }
        //update button for activities
        private void btnActivityUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new ActivityService object
                ActivityService activityService = new ActivityService();
                //if a row wasn't selected say that a row must be selected
                if (listViewActivities.SelectedItems.Count == 0)
                {
                    throw new SomerenException("Please select a row before updating one");
                }
                //if a textbox of activities isn't filled say that all textboxes must be filled
                if (ActivityBoxEmpty())
                {
                    throw new SomerenException("Please fill all textboxes!");
                }
                //if the entered activity name has already been added say that the activity has already been added
                if (ActivityNameWasAdded(txtActivityName.Text))
                {
                    throw new SomerenException("This activity has already been added!");
                }
                //if the start date is before the end date say that the start date can't be before the end date
                if (dateTimeStart.Value > dateTimeEnd.Value)
                {
                    throw new SomerenException("The start date can't be before the end date!");
                }
                //make a new Activity object with all values from the textboxes
                Activity activity = GetActivityFromTxtBoxes();
                //update the selected drink in the Drinks database
                activityService.UpdateActivity((Activity)listViewActivities.SelectedItems[0].Tag, activity);
                //reload the activities in the ListView
                AddActivitiesToList();
                //Clear all textboxes in the activities panel
                ClearActivityTxtBoxes();
            }
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
                MessageBox.Show("Something went wrong while updating an activity: " + exception.Message);
            }
        }
        //delete button for activities
        private void btnActivityDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //make a new ActivityService object
                ActivityService activityService = new ActivityService();
                //if a row wasn't selected say that a row must be selected
                if (listViewActivities.SelectedItems.Count == 0)
                {
                    throw new SomerenException("Please select a row before deleting one");
                }
                if (MessageBox.Show("Are you sure you want to delete this activity?", "Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) { return; }
                //update the selected activity in the Activities database
                activityService.DeleteActivity((Activity)listViewActivities.SelectedItems[0].Tag);
                //reload the activities in the ListView
                AddActivitiesToList();
                //Clear all textboxes in the activities panel
                ClearActivityTxtBoxes();
            }
            catch (SomerenException sex)
            {
                MessageBox.Show(sex.Message);
            }
            catch (Exception exception)
            {
                ErrorLogger.WriteLogToFile(exception.Message);
                MessageBox.Show("Something went wrong while deleting a drink: " + exception.Message);
            }
        }
        //clear button for activities
        private void BtnActivityClear_Click(object sender, EventArgs e)
        {
            ClearActivityTxtBoxes();
        }
        //method to empty out every textbox and set the start date to now and the end date to one hour after the start date
        private void ClearActivityTxtBoxes()
        {

            txtActivityName.Clear();
            txtActivityLocation.Clear();
            dateTimeStart.Value = DateTime.Now;
            dateTimeEnd.Value = DateTime.Now.AddHours(1);
        }
        //check if an activity box is empty
        private bool ActivityBoxEmpty()
        {
            foreach (Control control in pnlActivities.Controls)
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
        //get an Activity object from the activity txt- and datetime boxes
        private Activity GetActivityFromTxtBoxes()
        {
            Activity activity = new Activity()
            {
                Name = txtActivityName.Text,
                Location = txtActivityLocation.Text,
                StartDate = dateTimeStart.Value,
                EndDate = dateTimeEnd.Value
            };
            return activity;
        }
        //method to check if an activity name was already added
        private bool ActivityNameWasAdded(string name)
        {
            name.Replace(" ", "");
            for (int i = 0; i < listViewActivities.Items.Count; i++)
            {
                if (name == listViewActivities.Items[i].Text.Replace(" ", ""))
                {
                    return true;
                }
            }
            return false;
        }

        //==========UNKNOWN CODE==========

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
