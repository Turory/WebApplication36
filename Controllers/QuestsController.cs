using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication36.Models;

namespace WebApplication36.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestsController : ControllerBase
    {
        static List<Quest> quests = new()
        {
            new() { Id=1, Title="Пропавший амулет", Difficulty=1, Reward=100, Location="Деревня", IsDangerous=false },
            new() { Id=2, Title="Лесные разбойники", Difficulty=2, Reward=250, Location="Лес", IsDangerous=true },
            new() { Id=3, Title="Тайна старой башни", Difficulty=3, Reward=500, Location="Башня", IsDangerous=true },
            new() { Id=4, Title="Сундук капитана", Difficulty=2, Reward=450, Location="Порт", IsDangerous=false },
            new() { Id=5, Title="Огненный голем", Difficulty=4, Reward=900, Location="Горы", IsDangerous=true },
            new() { Id=6, Title="Зелье для мага", Difficulty=1, Reward=150, Location="Город", IsDangerous=false },
            new() { Id=7, Title="Ледяная пещера", Difficulty=5, Reward=1500, Location="Север", IsDangerous=true },
            new() { Id=8, Title="Забытый храм", Difficulty=3, Reward=650, Location="Джунгли", IsDangerous=true }
        };

        static List<Player> players = new()
        {
            new() { Id=1, Nickname="Лис", Level=1 },
            new() { Id=2, Nickname="Рыцарь_77", Level=3 },
            new() { Id=3, Nickname="Тень", Level=5 }
        };

        // Коллекция заявок игроков на приключения
        // Изначально пустая, заполняется при успешном начале задания
        static List<QuestStartRequest> requests = new();

        [HttpGet]
        public IActionResult getQuests()
        {
            return Ok(quests);
        }

        [HttpGet("{id:int}")]
        public IActionResult findQuest(int id)
        {
            var q = quests.FirstOrDefault(x => x.Id == id);
            if (q != null)
                return Ok(q);
            return NotFound();
        }

        [HttpGet("search")]
        public IActionResult searchByDiff([FromQuery] string difficulty)
        {
            var q = quests.Where(q => $"{q.Difficulty}" == difficulty);
            if (q.Any())
                return Ok(q);
            return NoContent();
        }

        [HttpGet("filter")]
        public IActionResult searchByTwo(
            [FromQuery] int minReward,
            [FromQuery] bool dangerous)
        {
            var result = quests.Where(q => q.Reward == 500 && q.IsDangerous == true);
            return Ok(result);

        }

        [HttpGet("random")]
        public IActionResult randomResult()
        {
            int num = Random.Shared.Next(quests.Count);
            var result = quests[num];

            return Ok(result);
        }

        [HttpPost("start")]
        public IActionResult startQuest([FromBody] QuestStartRequest questStartReq)
        {
            var player = players.FirstOrDefault(p => p.Id == questStartReq.PlayerId);
            var quest = quests.FirstOrDefault(q => q.Id == questStartReq.QuestId);

            if (player == null || quest == null)
                return BadRequest();

            if ( player.Level < quest.Difficulty )
                return BadRequest($"Уровень игрока {player.Nickname} слишком низкий!");

            if (quest.IsDangerous == true && !questStartReq.Equipment.Any())
                return BadRequest("Отсутствует экипировка");

            requests.Add(questStartReq);
            return Ok("Успешно");
        }

        [HttpPost("nickname")]
        public IActionResult welcome([FromForm] string Nickname)
        {
            var player = players.FirstOrDefault(p => p.Nickname == Nickname);
            return Ok($"Привет, {player.Nickname}!");
        }

        [HttpGet("secret")]
        public IActionResult secretCode([FromQuery] string code)
        {
            if (code == "DRAGON2026")
            {
                List<Quest> secretQuest = [new() { Id = 9, Title = "Пещера древнего дракона", Difficulty = 5, Reward = 3000, Location = "Горы", IsDangerous = true }];
                return Ok(secretQuest);
            }
            
            return NotFound();
        }
    }
}
