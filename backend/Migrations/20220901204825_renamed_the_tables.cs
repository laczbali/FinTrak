using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fintrak.Migrations
{
    public partial class renamed_the_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TransactionCategory_CategoryId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionCategory",
                table: "TransactionCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "TransactionCategory",
                newName: "transaction_categories");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_CategoryId",
                table: "transactions",
                newName: "IX_transactions_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transaction_categories",
                table: "transaction_categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_transactions",
                table: "transactions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_transaction_categories_CategoryId",
                table: "transactions",
                column: "CategoryId",
                principalTable: "transaction_categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_transaction_categories_CategoryId",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transactions",
                table: "transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_transaction_categories",
                table: "transaction_categories");

            migrationBuilder.RenameTable(
                name: "transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "transaction_categories",
                newName: "TransactionCategory");

            migrationBuilder.RenameIndex(
                name: "IX_transactions_CategoryId",
                table: "Transaction",
                newName: "IX_Transaction_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionCategory",
                table: "TransactionCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TransactionCategory_CategoryId",
                table: "Transaction",
                column: "CategoryId",
                principalTable: "TransactionCategory",
                principalColumn: "Id");
        }
    }
}
