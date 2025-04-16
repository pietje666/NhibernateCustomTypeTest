
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NhibernateTest.ClassMaps;

namespace NhibernateTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(factory =>
            {
                return NHibernateHelper.SessionFactory.OpenSession();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

public static class NHibernateHelper
{
    private static ISessionFactory _sessionFactory;

    public static ISessionFactory SessionFactory =>
        _sessionFactory ??= Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString("Server=localhost,1439;Database=test;User Id=sa;Password=SuperSecret123;")
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductClassMap>())
            .BuildSessionFactory();
}
