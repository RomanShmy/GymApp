using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(202009050003)]
    public class Migration_202009050003 : Migration
    {
        public override void Down()
        {
            Delete.Column("register_date").FromTable("subscription");
        }

        public override void Up()
        {
            Alter.Table("subscription").AddColumn("register_date").AsDateTime().Nullable();
        }
    }
}