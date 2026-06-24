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
            _storage.MarkAsComplete(id);

            ActivityLogger.Log($"Task marked complete (ID: {id})");
        }

        //Delete task
        public void DeleteTask(int id)
        {
            _storage.DeleteTask(id);

            ActivityLogger.Log($"Task deleted (ID: {id})");
            
        }
    }
}
