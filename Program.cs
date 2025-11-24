using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ Controllers
builder.Services.AddControllers();

// 2. Cấu hình Swagger (ĐÃ BỔ SUNG PHẦN NÚT KHÓA)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactApp API", Version = "v1" });

    // --- BẮT ĐẦU PHẦN QUAN TRỌNG ĐỂ HIỆN NÚT KHÓA ---
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Xin vui lòng nhập để có thể chỉnh sửa (ví dụ: MySecretKey123)",
        Name = "x-api-key", // Tên header
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "ApiKey",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    // --- KẾT THÚC PHẦN QUAN TRỌNG ---
});

// 3. Cấu hình CORS (Cho phép tất cả - Dễ test)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// 4. Cấu hình HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

// 5. Kích hoạt CORS (Quan trọng: Phải đặt trước MapControllers)
app.UseCors("AllowAll");

app.UseAuthorization();

// 6. Ánh xạ Controller
app.MapControllers();

app.Run();