namespace WebApplication36.Models
{
    public class Quest
    {
        public int Id { get; set; }              // Уникальный номер задания
        public string Title { get; set; } = "";  // Название задания
        public int Difficulty { get; set; }      // Сложность задания (от 1 до 5)
        public int Reward { get; set; }          // Награда за выполнение (золотые монеты)
        public string Location { get; set; } = ""; // Место выполнения задания
        public bool IsDangerous { get; set; }    // Опасное ли задание (true — да, false — нет)
    }
}
