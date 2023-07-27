using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Signal.DTO;
using Signal.Models;

namespace Signal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ChatapplicationContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ChatapplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //hi

        [HttpPost]
        public async Task<IActionResult> verify([FromBody] verifyDTO dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == dto.Username && x.Userpass == dto.Userpass);
            if (user == null)
            {
                return BadRequest();

            }
            var resp = new
            {
                user.Userid,
                user.Username,
                user.Userpass
            };
            return Ok(resp);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> getusers(int id)
        {
            var user = _context.Users.ToList();
            var resp = (from x in user
                        where x.Userid != id
                        select new
                        {
                            x.Userid,
                            x.Username
                        });
            return Ok(resp);

        }

        [HttpPost("createconvo")]
        public async Task<IActionResult> createconversion([FromBody] createconvoDTO createconvo)
        {
            var dataconvo = _context.Conversations.FirstOrDefault(x=>(x.User1==createconvo.User1 && x.User2==createconvo.User2) || (x.User1==createconvo.User2 && x.User2==createconvo.User1));
                Message message = new Message();
            if (dataconvo == null)
            {
                Conversation conversation = new Conversation(); 
                conversation.User1 = createconvo.User1;
                conversation.User2 = createconvo.User2;
                _context.Conversations.Add(conversation);
                _context.SaveChanges();
                message.Author = createconvo.User1;
                message.Convid = conversation.Convid;
                message.Content = createconvo.Content;
                _context.Messages.Add(message);
                _context.SaveChanges();


                return Ok(conversation);

            }
            else
            {
                message.Author = createconvo.User1;
                message.Convid = dataconvo.Convid;
                message.Content= createconvo.Content;
                _context.Messages.Add(message);
                _context.SaveChanges();
                return Ok(dataconvo);
            }


        }

        [HttpPost("getconvo")]
        public async Task<IActionResult> getconvo([FromBody] abcddto abc)
        {

            var message = _context.Messages.ToList();
            var convo = _context.Conversations.ToList();
            var resp = (from c in convo
                        where (c.User1 == abc.User1 && c.User2 == abc.User2) || (c.User1 == abc.User2 && c.User2 == abc.User1)
                        select new
                        {
                            content = (from x in message
                                       where c.Convid == x.Convid
                                       select new
                                       {
                                           x.Content,x.Author
                                       })
                        }).FirstOrDefault();


               


            return Ok(resp);
                    

        }
    }
}