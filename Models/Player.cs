namespace WebApplication36.Models
{
    public class Player
    {
        public int Id { get; set; }                  // Уникальный номер игрока
        public string Nickname { get; set; } = "";  // Игровое имя (никнейм)
        public int Level { get; set; }               // Уровень игрока

    }
}
