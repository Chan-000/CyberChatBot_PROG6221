using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CyberSecurityChatBot
{
    /*
     * The TaskStorageHelper class is responsible for reading and writing 
     * task data to the tasks.json file
     * It provide CRUD operations 
     */
    public class TaskStorageHelper
    {
        // path of the JSON file used to store class
        private const string FilePath = "tasks.json";

        //load all task from json file
        public List<CyberTask> LoadTasks()
        {
            try
            {
                if  (!File.Exists(FilePath))
                {
                    return new List<CyberTask>();
                }

                string json = File.ReadAllText(FilePath);

                return JsonConvert.DeserializeObject<List<CyberTask>>(json) ?? new List<CyberTask>();
            }
            catch
            {
                // return empty list if file  doesn't exist or error occurs
                return new List<CyberTask>();
            }
        }

        // Saves all tasks to JSON file
        public void SaveTasks (List<CyberTask> tasks)
        {
            try
            {
                string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch
            {
                //error handling
            }
        }

        // Creates a new task and saves it to the JSON file
        // a unique ID is generated automatically
        public void AddTasks(string title, string description, string reminder)
        {
            List<CyberTask> tasks = LoadTasks();

            int newId = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1;

            CyberTask newTask = new CyberTask
            {
                Id = newId,
                Title = title,
                Description = description,
                Reminder = reminder,
                IsComplete = false,
                CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
            };

            tasks.Add(newTask);
            SaveTasks(tasks);
        }

        //Mark a selected task as completed 
        // and saves the updated list
        public void MarkAsComplete(int id)
        {
            List<CyberTask> tasks = LoadTasks();

            CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);

            if  (task != null)
            {
                task.IsComplete = true;
                SaveTasks(tasks);
            }
        }

        // delete a task from json file
        // and saves the updated list
        public void DeleteTask(int id)
        {
            List<CyberTask> tasks = LoadTasks();

            CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                tasks.Remove(task);
                SaveTasks(tasks);
            }

        }
    }
}
