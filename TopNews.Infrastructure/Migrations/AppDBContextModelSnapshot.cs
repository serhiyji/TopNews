﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TopNews.Infrastructure.Context;

#nullable disable

namespace TopNews.Infrastructure.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TopNews.Core.Entities.NetworkAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IpAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("NetworkAddresses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IpAddress = "0.0.0.0"
                        },
                        new
                        {
                            Id = 2,
                            IpAddress = "::1"
                        });
                });

            modelBuilder.Entity("TopNews.Core.Entities.Site.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Політика"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Технології"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Наука"
                        });
                });

            modelBuilder.Entity("TopNews.Core.Entities.Site.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PublishDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Завтра у місті відбудуться вибори до місцевої ради. Громадяни вибиратимуть новий склад місцевих представників.",
                            FullText = "У понеділок, 15 серпня, у місті відбудуться вибори до місцевої ради. Це важливий подія для місцевої громади, оскільки громадяни визначатимуть склад обласних представників на наступні чотири роки. Кілька партій висунули своїх кандидатів, і змагання обіцяють бути напруженими. Громадяни закликаються взяти активну участь у голосуванні та виборах.",
                            ImagePath = "election.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Вибори до місцевої ради"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Президенти двох країн підписали угоду про співпрацю в галузі торгівлі та культурних відносин.",
                            FullText = "У вівторок, 25 липня, президенти країн Альфа та Бета підписали важливу двосторонню угоду про розширення співпраці. Згідно з угодою, країни зобов'язалися сприяти розвитку торгівлі між собою, а також спільно реалізовувати культурні проекти та обмін досвідом у галузі освіти. Це крок до зміцнення міжнародних відносин та покращення економічних зв'язків між країнами.",
                            ImagePath = "diplomacy.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Міжнародна дипломатія"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "Технологічна компанія X-Tech анонсувала випуск свого нового флагманського смартфона X-Phone з інноваційними функціями.",
                            FullText = " X-Tech, відомий гравець на ринку технологій, оголосив про випуск свого нового смартфона, який отримав назву X-Phone. Цей пристрій вражає своїми характеристиками: потужний процесор, вдосконалена камера зі здатністю запису відео в 8K, а також підтримка нових стандартів зв'язку. X-Phone також став першим смартфоном компанії, який використовує технологію розпізнавання обличчя для максимальної безпеки користувачів.",
                            ImagePath = "x-phone.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Випуск нового смартфона X-Phone"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "Компанія DurchTech представила свій новий продукт - DurchCloud, який обіцяє революціонізувати підхід до зберігання даних у хмарі.",
                            FullText = "DurchTech, інноваційна компанія в галузі хмарних технологій, впровадила свій останній досягнення - DurchCloud. Ця платформа надає користувачам можливість зберігати, обробляти та забезпечувати безпеку своїх даних у хмарному середовищі з використанням передових алгоритмів шифрування. Прор DurchCloud обіцяє високу продуктивність, зручний інтерфейс та гарантовану конфіденційність інформації користувачів.",
                            ImagePath = "durchcloud.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Прор DurchCloud - Прорив у хмарних технологіях"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Description = "Астрономи оголосили про відкриття нової планети, схожої на Землю, у галактиці Андромеда за допомогою потужного телескопа.",
                            FullText = "За допомогою найсучасніших телескопів, астрономи виявили нову планету, яка знаходиться у галактиці Андромеда, що знаходиться на відстані понад 2 мільйонів світлових років від Землі. Хоча планета має відмінності, вчені вбачають у ній потенційне місце для досліджень з пошуку позаземного життя через подібність умов до наших. Це відкриття може революціонізувати наше розуміння Всесвіту та можливості існування інших цивілізацій.",
                            ImagePath = "andromeda_planet.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Відкриття нової планети у галактиці Андромеда"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 3,
                            Description = "Вчені розробили і випробували новий метод лікування алергійних реакцій, який дозволяє зменшити загострення та покращити якість життя хворих.",
                            FullText = "Дослідники з медичного інституту представили новий метод, який спрямований на боротьбу зі загостреннями алергій. Цей метод базується на використанні імунотерапії та принципу поступового звикання організму до алергенів. Після успішних клінічних випробувань, пацієнти, які страждають від сильних алергійних реакцій, зазнали помітного покращення стану здоров'я та зменшення інтенсивності алергічних симптомів.",
                            ImagePath = "allergy_treatment.jpg",
                            PublishDate = "00.00.0000",
                            Title = "Новий метод боротьби зі загостреннями алергій"
                        });
                });

            modelBuilder.Entity("TopNews.Core.Entities.User.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TopNews.Core.Entities.Site.Post", b =>
                {
                    b.HasOne("TopNews.Core.Entities.Site.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
