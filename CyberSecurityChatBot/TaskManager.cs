using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    /*
     * The TaskManager class handles all task-related operations
     * it acts as the middle layer between a chatbot and the JSON storage system
     */

    public class TaskManager
    {
        // helper class responsible for reading and writing tasks.json
        private readonly TaskStorageHelper _storage;

        // constructor to create storage helper object
        public TaskManager()
        {
            _storage = new TaskStorageHelper();
            
        }

        //Adds new tasks to storage
        //records the action in the activity log
        //return a confirmation message for the uer
        public string AddTask(string title, string description, string reminder)
        {
            _storage.AddTasks(title, description, reminder);
            ActivityLogger.Log($"Task added: '{title}'");

            return $"Task '{title}' added successfully.";
        }

        //retrieves all stored tasks
        public List<CyberTask> GetAllTasks()
        {
            return _storage.LoadTasks();
        }

        //Mark a task as complete and update the task in JSON storage
        public void MarkAsComplete(int id)
        {
            List<CyberTask> tasks = _storage.LoadTasks();
            CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);
            
            if (task != null)
            {
                _storage.MarkAsComplete(id);
                ActivityLogger.Log($"Task marked complete: '{task.Title}'");
            }

           ;
        }

        //Delete task from storage 
        public void DeleteTask(int id)
        {
            List<CyberTask> tasks = _storage.LoadTasks();

            CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);
           
            if (task != null)
            { 
                //record deletion action
                ActivityLogger.Log($"Task deleted: '{task.Title}'");
                _storage.DeleteTask(id);

            }
            
        }
    }
}
