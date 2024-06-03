# Hướng dẫn tích hợp Redis vào Web API kết nối SQL Server với Entity Framework

## 1. Cài đặt gói StackExchange.Redis

Sử dụng NuGet Package Manager hoặc .NET CLI để cài đặt gói `StackExchange.Redis` vào dự án của bạn:

## 2. Cấu hình Redis trong ứng dụng của bạn

### a. Thêm thông tin cấu hình Redis vào file `appsettings.json`:

```json
{
    "ConnectionStrings": {
        "DefaultConnection": "your_sql_server_connection_string"
    },
    "Redis": {
        "ConnectionString": "your_redis_server:port",
        "Password": "your_redis_password"
    }
}
```

```C#
public void ConfigureServices(IServiceCollection services)
{
    // Cấu hình Redis
    services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(Configuration.GetConnectionString("Redis")));

    // Cấu hình Entity Framework
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

    services.AddControllers();
}

public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConnectionMultiplexer _redis;

    public ProductsController(ApplicationDbContext dbContext, IConnectionMultiplexer redis)
    {
        _dbContext = dbContext;
        _redis = redis;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var redisDb = _redis.GetDatabase();
        var cachedProduct = await redisDb.StringGetAsync($"product_{id}");

        if (!cachedProduct.IsNullOrEmpty)
        {
            var product = JsonConvert.DeserializeObject<Product>(cachedProduct);
            return Ok(product);
        }
        else
        {
            var product = await _dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await redisDb.StringSetAsync($"product_{id}", JsonConvert.SerializeObject(product), expiry: TimeSpan.FromMinutes(10));

            return Ok(product);
        }
    }

    // Các phương thức CRUD khác tương tự
}

```

Sau khi bạn copy và paste nội dung này vào file `README.md` trong dự án của mình, bạn có thể sử dụng nó như một hướng dẫn cho dự án của mình.
