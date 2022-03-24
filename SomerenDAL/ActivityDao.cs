using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Collections.ObjectModel;
using SomerenModel;

namespace SomerenDAL
{
    public class ActivityDao : BaseDao
    {
        //query to get all activities
        public List<Activity> GetAllActivities()
        {
            string query = "SELECT activityId, activityName, activityLocation, startDateTime, endDateTime FROM Activities;";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

        //query to add an activity to the Activities database table
        public void Add(Activity activity)
        {
            string query = "INSERT INTO Activities (activityName, activityLocation, startDateTime, endDateTime) VALUES (@name, @location, @start, @end);";
            SqlParameter[] sqlParameters = new SqlParameter[4]
            {
                new SqlParameter("@name", activity.Name),
                new SqlParameter("@location", activity.Location),
                new SqlParameter("@start", activity.StartDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@end", activity.EndDate.ToString("yyyy-MM-dd HH:mm:ss"))
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //query to update a selected activity (oldActivity) to an inserted activity (newActivity)
        public void Update(Activity oldActivity, Activity newActivity)
        {
            string query = "UPDATE Activities SET activityName=@newName, activityLocation=@newLocation, startDateTime=@newStartDate, endDateTime=@newEndDate WHERE activityId=@oldId;";
            SqlParameter[] sqlParameters = new SqlParameter[5]
            {
                new SqlParameter("@newName", newActivity.Name),
                new SqlParameter("@newLocation", newActivity.Location),
                new SqlParameter("@newStartDate", newActivity.StartDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@newEndDate", newActivity.EndDate.ToString("yyyy-MM-dd HH:mm:ss")),
                new SqlParameter("@oldId", oldActivity.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //query to delete a selected activity
        public void Delete(Activity activity)
        {
            string query = "DELETE FROM Activities WHERE activityId=@Id";
            SqlParameter[] sqlParameters = new SqlParameter[1]
            {
                new SqlParameter("@Id", activity.Id)
            };
            ExecuteEditQuery(query, sqlParameters);
        }

        //method to read the data from the query that the sql database returns
        private List<Activity> ReadTables(DataTable dataTable)
        {
            List<Activity> activities = new List<Activity>();

            foreach (DataRow dr in dataTable.Rows)
            {
                Activity activity = new Activity()
                {
                    Id = (int)dr["activityId"],
                    Name = (string)dr["activityName"],
                    Location = (string)dr["activityLocation"],
                    StartDate = (DateTime)dr["startDateTime"],
                    EndDate = (DateTime)dr["endDateTime"]
                };
                activities.Add(activity);
            }
            return activities;
        }
    }
}
