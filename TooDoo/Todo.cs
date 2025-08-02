using System.Text.Json.Serialization;

namespace TooDoo
{
    public class Todo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public int Priority { get; set; }
        public int Index { get; set; }
        public string Date { get; set; }
        public string Hours { get; set; }
        public string Minutes { get; set; }

        public Todo() 
        {
            Title = "";
            Description = "";
            IsDone = false;
            Priority = 0;
            Index = 0;
            Date = "";
            Hours = "";
            Minutes = "";
        }
        public Todo(string title, string description, bool isDone, int priority, int index, string date, string hours, string minutes)
        {
            Title = title;
            Description = description;
            IsDone = isDone;
            Priority = priority;
            Index = index;
            Date = date;
            Hours = hours;
            Minutes = minutes;
        }
        public void UpdateDate(string date)
        {
            Date = date;
        }
        public void UpdateTitle(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
            }
            else
            {
                Console.Write("Title cannot be empty!");
                Console.ReadKey();
            }
        }
        public void UpdateDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
            {
                Description = description;
            }
            else
            {
                Console.Write("Description cannot be empty!");
                Console.ReadKey();
            }
        }
        public void UpdatePriority(int priority)
        {
            if (priority > 0 && priority <= 5)
            {
                Priority = priority;
            }
            else
            {
                Console.Write("Priority must be in range 1-5!");
                Console.ReadKey();
            }
        }
        public void UpdateIsDone(bool isDone)
        {
            IsDone = isDone;
        }

        public int UpdateIndex(int index)
        {
            Index = index;
            return Index;
        }
        public string UpdateHours(string hours)
        {
            Hours = hours;
            return Hours;
        }
        public string UpdateMinutes(string minutes)
        {
            Minutes = minutes;
            return Minutes;
        }
        public string TodoToString()
        {
            return Index + ") Title: " + Title + "\n   Description: " + Description +
                   "\n   Priority: " + Priority + "\n   Date: " + Date + "\n   Time: " + Hours + ":" + Minutes;
        }
    }
}