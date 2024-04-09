using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class addroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO AspNetRoles VALUES (NEWID(),'Admin','Admin',NEWID()); INSERT INTO AspNetRoles VALUES (NEWID(),'User','User',NEWID());");
        }

    }
}
