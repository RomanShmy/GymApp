using FluentMigrator;

namespace GymApp.Migrations
{
    [Migration(20200903, "Add subscription table and add column to account")]
    public class Migration_20200903 : Migration
    {
        public override void Down()
        {
            Delete.Table("subscription");
            Delete.Column("subscription_id").FromTable("account");
        }

        public override void Up()
        {
            Create.Table("subscription")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey().Identity()
                .WithColumn("type").AsInt32()
                .WithColumn("expiration_date").AsDate();

            Alter.Table("account").AddColumn("subscription_id").AsInt64().ForeignKey("subscription", "id").Nullable();
        }
    }
}