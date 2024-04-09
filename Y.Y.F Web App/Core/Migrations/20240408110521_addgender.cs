using Core.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

#nullable disable

namespace Core.Migrations
{
    public partial class addgender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CommonDropDowns VALUES('Female',GETDATE(),1, 0); INSERT INTO CommonDropDowns VALUES('Male',GETDATE(),1, 0);");
        }

    }
}
