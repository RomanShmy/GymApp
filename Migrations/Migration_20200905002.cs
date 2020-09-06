using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200905002)]
    public class Migration_20200905002 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("services").AllRows();
        }

        public override void Up()
        {
            Insert.IntoTable("services").Row(new {name = "GYM", price = 100});
            Insert.IntoTable("services").Row(new {name = "SWIMMING_POOL", price = 150});
            Insert.IntoTable("services").Row(new {name = "SPA", price = 200});
        }
    }
}