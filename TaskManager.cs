using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace ToDo_Reminder
{
    internal class TaskManager
    {
        // A private list that holds all tasks
        List<Task> taskList;
        public TaskManager()    
        { 
            taskList = new List<Task>();
        }

        public Task GetTask(int index)
        {
            if (CheckedIndex(index))
                return taskList[index];
            else
                return null;

        }

        public bool CheckedIndex(int index) 
        { 
            bool ok = false;
            if ((index >= 0) && (index < taskList.Count))
            {
                ok = true;
            }
            return ok;
        }

        // Method to add a new task to the task list
        public bool AddNewTask(Task newTask)
        {
            if (newTask == null)
            {
                return false;
            }

            taskList.Add(newTask);
            return true;
        }




        public bool CheckIndex(int index) 
        {
            bool ok = false;

            if ((index >= 0) && (index < taskList.Count))
            {
                ok = true;
            }
            return ok;
        }

        // Method to get an array of strings representing each task in the list
        public string[] GetInfoStringList()
        {
            string[] infoStrings = new string[taskList.Count];

            for (int i = 0; i < infoStrings.Length; i++) 
            {
                infoStrings[i] = taskList[i].ToString();
            }
            return infoStrings;
        }

        public bool WriteDataToFile(string fileName)
        {
            FileManager fileManager = new FileManager();
            return fileManager.SaveTaskListToFile(taskList, fileName);
        }

        // Create an instance of FileManager and use it to read tasks into the task list
        public bool ReadDataFromFile(string fileName)
        {
            FileManager fileManager = new FileManager();
            return fileManager.ReadTaskListFrFile(taskList, fileName);
        }
        
    }
}
