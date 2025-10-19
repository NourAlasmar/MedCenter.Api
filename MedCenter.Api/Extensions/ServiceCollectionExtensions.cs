// هذا الملف يوفّر توسيعات (Extension Methods) على IServiceCollection و IApplicationBuilder
// الهدف: إبقاء Program.cs نظيفًا وغير مزدحم عبر نقل تسجيل الخدمات وخط أنابيب الوسطاء (Middleware)
// إلى مكان واحد واضح ومنظم. نستدعي هاتين الدالتين من Program.cs:
//
// builder.Services.AddAppServices(builder.Configuration);
// app.UseAppPipeline(app.Environment);
//
// الدالة الأولى AddAppServices: تسجّل الـ DbContext والمستودعات والخدمات وSwagger.
// الدالة الثانية UseAppPipeline: تضبط الـ Middleware (Swagger في التطوير، HTTPS، Routing، Authorization، Controllers).

using MedCenter.Api.Data;
using MedCenter.Api.Repositories.Implementations;
using MedCenter.Api.Repositories.Interfaces;
using MedCenter.Api.Services.Implementations;
using MedCenter.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace MedCenter.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration config)
        {
            // DbContext
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(config.GetConnectionString("Default")));

            // Repositories + UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Services
            // تسجيل الخدمات المجالّية (Domain Services) بواجهاتها للحفاظ على قابلية الاستبدال والاختبار
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IFinanceService, FinanceService>();
            // تسجيل خدمة العلاجات
            services.AddScoped<ITreatmentService, TreatmentService>();
            // Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MedCenter API", Version = "v1" });
            });

            // Controllers
            services.AddControllers();

            return services;
        }


        public static IApplicationBuilder UseAppPipeline(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // تفعيل إعادة التوجيه إلى HTTPS لأمان الاتصال
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(e => e.MapControllers());

            return app;
        }
    }
}