using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using webApiTest.Model;
using webApiTest.MyDataBaseContext;

namespace webApiTest.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MyDataBaseContext_main _coreDbContext;

        private readonly ILogger<ValuesController> _logger;
        public ValuesController(ILogger<ValuesController> logger, MyDataBaseContext_main coreDbContext)
        {
            _logger = logger;
            _coreDbContext = coreDbContext;
        }
        [HttpGet]
        public string R1()
        {
            _logger.LogInformation("这是一个Get请求");
            _logger.LogError("写入error测试,这里是Error输出");
            return ("这里是control1");
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<t_Master> GetUserInfo()
        {
            try
            {

                return _coreDbContext.Set<t_Master>().ToList();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "failed to retrieve user info");
                return new List<t_Master>();
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<t_Master>> GetInfoByID(int id)
        {
            var tmaster = await _coreDbContext.t_Master.FindAsync(id);
            if (tmaster == null)
            {
                return NotFound();
            }
            return TMaster(tmaster);
        }

        /// <summary>
        /// 根据输入参数查询
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet("{Name}")]
        public async Task<ActionResult<student>> GetInfoByuserName(string Name,int age)
        {
            var tmaster = await _coreDbContext.student.Where(s=>s.Name==Name && s.Age==age).FirstOrDefaultAsync();
            
            if (tmaster == null)
            {
                return NotFound();
            }
            return ToStudent(tmaster);
        }

        /// <summary>
        /// 根据输入ID更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tmasterdo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<t_Master>> PuttMaster(int id,t_MasterDo tmasterdo)
        {
            if (id != tmasterdo.id)
            {
                return BadRequest();
            }
            var t_master = await _coreDbContext.t_Master.FindAsync(id);
            if (t_master==null)
            {
                return NotFound();
            }
            t_master.Name = tmasterdo.Name;
            try
            {
                await _coreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)when(!ToExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        private bool ToExists(int id)
        {
            return _coreDbContext.t_Master.Any(e => e.id == id);
        }
        private static t_Master TMaster(t_Master tmaster) => new t_Master
        {
            id = tmaster.id,
            Name = tmaster.Name,
        };

        private static student ToStudent(student stu) => new student
        {
            id = stu.id,
            Name = stu.Name,
            Age = stu.Age,
            Create_time = stu.Create_time,
            last_login_time = stu.last_login_time,
        };
    }
}
