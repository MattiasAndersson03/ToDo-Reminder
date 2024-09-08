using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Reminder
{
    [Serializable]
    public class Task
    {
        // Private fields to sore the task details
        private DateTime date;
        private string description;
        private PriorityType priority;
        
        // Constructor to initialize the the task with normal priority
        public Task()
        {
            priority = PriorityType.Normal;
        }

        public Task(DateTime taskDate) : this(taskDate, string.Empty, PriorityType.Normal) 
        { 
        }

        public Task(DateTime taskDate, string description, PriorityType priority)
        {
            this.date = taskDate;
            this.description = description;
            this.priority = priority;
        }
        #region GetSet
        // Property to get set dame and time of the task
        public DateTime DateTime 
        { 
            get { return date; } 
            set { date = value; }
        }

        public PriorityType Priority
        { 
            get { return priority; } 
            set { priority = value; }
        }

        // Property to get or set the description of the task.
        public string Description
        { 
            get { return description; } 
            set 
            {
                // Only set the description if the value is not null or empty.
                if (!string.IsNullOrEmpty(value))
                    description = value;
            } 
        }

        public DateTime TaskDate
        { 
            get { return date; } 
            set {  date = value; } 
        }
        #endregion
        private string GetTimeString()
        {
            string time = string.Format("{0}:{1}", date.Hour.ToString("00"),
                date.Minute.ToString("00"));
            
            return time;
        }

        // Method to return the priority level as a string withour underscores
        public string GetPriorityString()
        {
            string txtOut = Enum.GetName(typeof(PriorityType), priority)?.Replace("_", string.Empty);
            return txtOut ?? string.Empty;
        }


        // Override the ToString method to provide a formatted string representation of the task
        public override string ToString()
        {
            string textOut = string.Format("{0,-20} {1,25} {2,18} {3,45}",
                                           date.ToLongDateString(),
                                           GetTimeString(),
                                           GetPriorityString(),
                                           description);
            return textOut;
        }


    }

}
