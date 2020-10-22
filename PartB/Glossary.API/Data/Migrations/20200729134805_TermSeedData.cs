using Microsoft.EntityFrameworkCore.Migrations;

namespace Glossary.API.Data.Migrations
{
    public partial class TermSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO GlossaryTerms(Term, Definition) VALUES ('abyssal plain','The ocean floor offshore from the continental margin, usually very flat with a slight slope.');
INSERT INTO GlossaryTerms(Term, Definition) VALUES ('accrete','To add terranes (small land masses or pieces of crust) to another, usually larger, land mass.')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
