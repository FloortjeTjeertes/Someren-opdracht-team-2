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

        private ImageList GetDrinkIcons()
        {
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(40, 40);
            string[] filePaths = Directory.GetFiles("C:/Users/sempl/OneDrive/Documenten/Project Databases/Databases-Someren-repo/img");
            

            foreach (string filePath in filePaths)
            {
                imgs.Images.Add(Image.FromFile(filePath));
            }

            return imgs;
        }

        private void showPanel(string panelName)
        {

            if (panelName == "Dashboard")
            {
                // hide all other panels
                pnlStudents.Hide();
                pnlDrinks.Hide();

                // show dashboard
                pnlDashboard.Show();
                imgDashboard.Show();
            }
            else if (panelName == "Students") //if the students panel is clicked this code is executed
            {
                // hide all other panels
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlDrinks.Hide();

                // show students
                pnlStudents.Show();

                try
                {
                    // fill the students listview within the students panel with a list of students
                    StudentService studService = new StudentService();
                    List<Student> studentList = studService.GetStudents();

                    // clear the listview before filling it again
                    listViewStudents.Items.Clear();

                    listViewStudents.SmallImageList = GetDrinkIcons();

                    //add each Student to the ListView in the Student panel
                    foreach (Student s in studentList)
                    {
                        ListViewItem li = new ListViewItem(Convert.ToString(s.Id));
                        li.SubItems.Add(s.FirstName);
                        li.SubItems.Add(s.LastName);
                        listViewStudents.Items.Add(li);
                    }
                    //view the student ListView in Details format
                    listViewStudents.View = View.Details;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the students: " + e.Message);
                }
            }
            else if(panelName == "Drinks")
            {
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();

                pnlDrinks.Show();

                try
                {
                    // fill the students listview within the students panel with a list of students
                    DrinkService drinkService = new DrinkService();
                    List<Drink> drinksList = drinkService.GetDrinks();

                    // clear the listview before filling it again
                    listViewDrinks.Items.Clear();

                    foreach (Drink d in drinksList)
                    {
                        ListViewItem li = new ListViewItem(d.Name);
                        li.SubItems.Add(Convert.ToString(d.SalesPrice));
                        li.SubItems.Add(Convert.ToString(d.Stock));
                        listViewDrinks.Items.Add(li);
                    }
                    listViewDrinks.View = View.Details;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Something went wrong while loading the drinks: " + e.Message);
                }
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
            //if the students toolstrip menu item is clicked use the showPanel() method to show the Students panel
            showPanel("Students");
        }

        private void lecturersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void drinksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Drinks");
        }

        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
