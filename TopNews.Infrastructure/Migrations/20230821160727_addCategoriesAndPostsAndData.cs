using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TopNews.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addCategoriesAndPostsAndData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublicationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCategory = table.Column<int>(type: "int", nullable: false),
                    NameImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Політика" },
                    { 2, "Технології" },
                    { 3, "Наука" }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Description", "IdCategory", "NameImage", "PublicationDateTime", "Text", "Title" },
                values: new object[,]
                {
                    { 1, "Завтра у місті відбудуться вибори до місцевої ради. Громадяни вибиратимуть новий склад місцевих представників.", 1, "election.jpg", new DateTime(2023, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "У понеділок, 15 серпня, у місті відбудуться вибори до місцевої ради. Це важливий подія для місцевої громади, оскільки громадяни визначатимуть склад обласних представників на наступні чотири роки. Кілька партій висунули своїх кандидатів, і змагання обіцяють бути напруженими. Громадяни закликаються взяти активну участь у голосуванні та виборах.", "Вибори до місцевої ради" },
                    { 2, "Президенти двох країн підписали угоду про співпрацю в галузі торгівлі та культурних відносин.", 1, "diplomacy.jpg", new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "У вівторок, 25 липня, президенти країн Альфа та Бета підписали важливу двосторонню угоду про розширення співпраці. Згідно з угодою, країни зобов'язалися сприяти розвитку торгівлі між собою, а також спільно реалізовувати культурні проекти та обмін досвідом у галузі освіти. Це крок до зміцнення міжнародних відносин та покращення економічних зв'язків між країнами.", "Міжнародна дипломатія" },
                    { 3, "Технологічна компанія X-Tech анонсувала випуск свого нового флагманського смартфона X-Phone з інноваційними функціями.", 2, "x-phone.jpg", new DateTime(2023, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), " X-Tech, відомий гравець на ринку технологій, оголосив про випуск свого нового смартфона, який отримав назву X-Phone. Цей пристрій вражає своїми характеристиками: потужний процесор, вдосконалена камера зі здатністю запису відео в 8K, а також підтримка нових стандартів зв'язку. X-Phone також став першим смартфоном компанії, який використовує технологію розпізнавання обличчя для максимальної безпеки користувачів.", "Випуск нового смартфона X-Phone" },
                    { 4, "Компанія DurchTech представила свій новий продукт - DurchCloud, який обіцяє революціонізувати підхід до зберігання даних у хмарі.", 2, "durchcloud.jpg", new DateTime(2023, 7, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "DurchTech, інноваційна компанія в галузі хмарних технологій, впровадила свій останній досягнення - DurchCloud. Ця платформа надає користувачам можливість зберігати, обробляти та забезпечувати безпеку своїх даних у хмарному середовищі з використанням передових алгоритмів шифрування. Прор DurchCloud обіцяє високу продуктивність, зручний інтерфейс та гарантовану конфіденційність інформації користувачів.", "Прор DurchCloud - Прорив у хмарних технологіях" },
                    { 5, "Астрономи оголосили про відкриття нової планети, схожої на Землю, у галактиці Андромеда за допомогою потужного телескопа.", 3, "andromeda_planet.jpg", new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "За допомогою найсучасніших телескопів, астрономи виявили нову планету, яка знаходиться у галактиці Андромеда, що знаходиться на відстані понад 2 мільйонів світлових років від Землі. Хоча планета має відмінності, вчені вбачають у ній потенційне місце для досліджень з пошуку позаземного життя через подібність умов до наших. Це відкриття може революціонізувати наше розуміння Всесвіту та можливості існування інших цивілізацій.", "Відкриття нової планети у галактиці Андромеда" },
                    { 6, "Вчені розробили і випробували новий метод лікування алергійних реакцій, який дозволяє зменшити загострення та покращити якість життя хворих.", 3, "allergy_treatment.jpg", new DateTime(2023, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дослідники з медичного інституту представили новий метод, який спрямований на боротьбу зі загостреннями алергій. Цей метод базується на використанні імунотерапії та принципу поступового звикання організму до алергенів. Після успішних клінічних випробувань, пацієнти, які страждають від сильних алергійних реакцій, зазнали помітного покращення стану здоров'я та зменшення інтенсивності алергічних симптомів.", "Новий метод боротьби зі загостреннями алергій" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IdCategory",
                table: "Posts",
                column: "IdCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
