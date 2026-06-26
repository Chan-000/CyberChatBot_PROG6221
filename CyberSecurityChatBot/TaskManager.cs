using System;
using System.Collections.Generic;

namespace CyberSecurityChatBot
{
    // handles task-related business logic
    public class TaskManager
    {
        private readonly TaskStorageHelper _storage;

        public TaskManager()
        {
            _storage = new TaskStorageHelper();
            
        }

        //Creates a new task
        // Returns a confirmation message
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

        //Mark a task as complete
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

        //Delete task
        public void DeleteTask(int id)
        {
            List<CyberTask> tasks = _storage.LoadTasks();

            CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);
           
            if (task != null)
            {
                ActivityLogger.Log($"Task deleted: '{task.Title}'");
                _storage.DeleteTask(id);

            }
            
        }
    }
}
