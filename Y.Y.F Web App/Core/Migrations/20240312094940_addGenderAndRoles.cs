using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class addGenderAndRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CommonDropDowns VALUES ('Female',GETDATE(),1,0); INSERT INTO CommonDropDowns VALUES ('Male',GETDATE(),1,0);");

            migrationBuilder.Sql("INSERT INTO AspNetRoles VALUES (NewId(),'Admin','Admin',NEWID()); INSERT INTO AspNetRoles VALUES (NewId(),'User','User',NEWID());");
        }
    }
}
