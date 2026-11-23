namespace WebApplication36.Models
{
    public class QuestStartRequest
    {
        public int PlayerId { get; set; }  // Номер игрока, который хочет начать задание
        public int QuestId { get; set; }   // Номер выбранного задания

        public List<string> Equipment { get; set; } = new();
        // Список снаряжения игрока (например: "меч", "щит", "зелье")

    }
}
