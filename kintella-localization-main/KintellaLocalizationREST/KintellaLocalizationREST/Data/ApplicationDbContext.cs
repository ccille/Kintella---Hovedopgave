using KintellaLocalizationREST.Helpers;
using KintellaLocalizationREST.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Data
{
    public class ApplicationDbContext : DbContext
    {
        AppSettings appSettings = new();
        UserSeeder userSeeder = new(new PasswordHasher<User>());
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }
        public ApplicationDbContext()
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Text> Texts { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<SubModule> SubModules { get; set; }
        public DbSet<ComponentContent> ComponentContents { get; set; }
        public DbSet<TextChanges> TextChanges { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseNpgsql(appSettings.ConnectionString);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(appSettings.ConnectionString);
                optionsBuilder.EnableSensitiveDataLogging(true);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Users
            #region User Migration
            modelBuilder.Entity<User>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.UserID);

                // Properties
                entity.Property(e => e.UserID).HasColumnName("UserID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.Username).HasColumnName("Username").HasColumnType("text");
                entity.Property(e => e.Password).HasColumnName("Password").HasColumnType("text");

                // Seed Data
                entity.HasData(userSeeder.CreateUserSeed(1, "testuser", "1234"));
            });
            #endregion


            // Languages
            #region Language Migration
            modelBuilder.Entity<Language>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.LanguageID);

                // Properties
                entity.Property(e => e.LanguageID).HasColumnName("LanguageID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.LanguageName).HasColumnName("Name").HasColumnType("text");

                // Seed Data
                entity.HasData(
                    new Language { LanguageID = 1, LanguageName = "Dansk/Danish", LanguageLocale = "da-DK" },
                    new Language { LanguageID = 2, LanguageName = "Engelsk/English", LanguageLocale = "en-US" }
                    );
            });
            #endregion


            // Modules
            #region Modules Migration

            modelBuilder.Entity<Module>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.ModuleID);

                // Properties
                entity.Property(e => e.ModuleID).HasColumnName("ModuleID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.ModuleName).HasColumnName("ModuleName").HasColumnType("text");
                entity.Property(e => e.ModuleIndexText).HasColumnName("ModuleIndexText").HasColumnType("text");
                entity.Property(e => e.LanguageID).HasColumnName("LanguageID");

                // Seed Data
                #region Modules Seed Data
                entity.HasData(
                #region Danish Seed Data
                    new Module
                    {
                        ModuleID = 1,
                        ModuleName = "Nyheder",
                        ModuleIndexText = "Under dette modul vil der blive anvist nyheder.",
                        LanguageID = 1

                    },
                    new Module
                    {
                        ModuleID = 2,
                        ModuleName = "Aftaler",
                        ModuleIndexText = "Under dette modul vil der blive anvist aftaler.",
                        LanguageID = 1
                    },
                    new Module
                    {
                        ModuleID = 3,
                        ModuleName = "Kontakt",
                        ModuleIndexText = "Under dette modul vil der blive anvist kontaktoplysninger.",
                        LanguageID = 1
                    },
                #endregion
                #region English Seed Data
                    new Module
                    {
                        ModuleID = 4,
                        ModuleName = "News",
                        ModuleIndexText = "News will be presented under this module.",
                        LanguageID = 2
                    },
                    new Module
                    {
                        ModuleID = 5,
                        ModuleName = "Appointments",
                        ModuleIndexText = "Appointments will be presented under this module.",
                        LanguageID = 2
                    },
                    new Module
                    {
                        ModuleID = 6,
                        ModuleName = "Contact",
                        ModuleIndexText = "Contact information will be presented under this module.",
                        LanguageID = 2
                    }
                    #endregion
                    );
                #endregion
            });
            #endregion


            // SubModules
            #region SubModules Migration
            modelBuilder.Entity<SubModule>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.SubModuleID);

                // Foreign Key - assuming there is a Module table that SubModule relates to
                entity.HasOne<Module>().WithMany(sm => sm.SubModules).HasForeignKey(e => e.ModuleID);

                // Properties
                entity.Property(e => e.SubModuleID).HasColumnName("SubModuleID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.SubModuleName).HasColumnName("SubModuleName").HasColumnType("text");
                entity.Property(e => e.ModuleID).HasColumnName("ModuleID");
                entity.Property(e => e.LanguageID).HasColumnName("LanguageID");

                // Seed Data
                #region SubModules Seed Data
                entity.HasData(
                #region Danish Seed Data
                    new SubModule
                    {
                        SubModuleID = 1,
                        SubModuleName = "Dagens Aktiviteter",
                        ModuleID = 1,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 2,
                        SubModuleName = "Kommende Events",
                        ModuleID = 1,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 3,
                        SubModuleName = "Advarsler",
                        ModuleID = 1,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 4,
                        SubModuleName = "Ugeplan",
                        ModuleID = 2,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 5,
                        SubModuleName = "Møder",
                        ModuleID = 2,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 6,
                        SubModuleName = "Personer",
                        ModuleID = 2,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 7,
                        SubModuleName = "Mad",
                        ModuleID = 2,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 8,
                        SubModuleName = "Pårørende",
                        ModuleID = 3,
                        LanguageID = 1
                    },
                    new SubModule
                    {
                        SubModuleID = 9,
                        SubModuleName = "Personale Kontakt",
                        ModuleID = 3,
                        LanguageID = 1
                    },
                    #endregion
                #region English Seed Data
                    new SubModule
                    {
                        SubModuleID = 10,
                        SubModuleName = "The Day's Activities",
                        ModuleID = 1,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 11,
                        SubModuleName = "Coming Events",
                        ModuleID = 1,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 12,
                        SubModuleName = "Warnings",
                        ModuleID = 1,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 13,
                        SubModuleName = "Weekplan",
                        ModuleID = 2,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 14,
                        SubModuleName = "Meetings",
                        ModuleID = 2,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 15,
                        SubModuleName = "Persons",
                        ModuleID = 2,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 16,
                        SubModuleName = "Food",
                        ModuleID = 2,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 17,
                        SubModuleName = "Relatives",
                        ModuleID = 3,
                        LanguageID = 2
                    },
                    new SubModule
                    {
                        SubModuleID = 18,
                        SubModuleName = "Staff Contact",
                        ModuleID = 3,
                        LanguageID = 2
                    }
                    #endregion
                    );
                #endregion


            });
            #endregion


            // Texts
            #region Texts Migration
            modelBuilder.Entity<Text>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.TextID);

                // Foreign Key - assuming there is a SubModule table that Text relates to
                entity.HasOne<SubModule>().WithMany(sm => sm.ListOfSubModuleTexts).HasForeignKey(e => e.SubModuleID);

                // Properties
                entity.Property(e => e.TextID).HasColumnName("TextID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.SubModuleID).HasColumnName("SubModuleID").HasColumnType("integer");
                entity.Property(e => e.TextContent).HasColumnName("TextContent").HasColumnType("text");
                entity.Property(e => e.LanguageID).HasColumnName("LanguageID");
                entity.Property(e => e.DateCreated).HasColumnName("DateCreated").HasColumnType("timestamptz");
                entity.Property(e => e.DateModified).HasColumnName("DateModified").HasColumnType("timestamptz");
                entity.Property(e => e.IsProduction).HasColumnName("IsProduction").HasColumnType("boolean");
                entity.Property(e => e.ShouldDisplay).HasColumnName("ShouldDisplay").HasColumnType("boolean");

                // Seed Data
                #region Texts Seed Data
                entity.HasData(
                    new Text
                    {
                        TextID = 1,
                        SubModuleID = 1,
                        LanguageID = 1,
                        TextContent = "Dette er en test-tekst. Denne tekst er skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 2,
                        SubModuleID = 1,
                        LanguageID = 2,
                        TextContent = "This is a test text. This text is written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 3,
                        SubModuleID = 1,
                        LanguageID = 1,
                        TextContent = "Dette er en anden test-tekst. Denne tekst er også skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 4,
                        SubModuleID = 1,
                        LanguageID = 2,
                        TextContent = "This is another test text. This text is also written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 5,
                        SubModuleID = 2,
                        LanguageID = 1,
                        TextContent = "Dette er en test-tekst. Denne tekst er skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 6,
                        SubModuleID = 2,
                        LanguageID = 2,
                        TextContent = "This is a test text. This text is written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 7,
                        SubModuleID = 2,
                        LanguageID = 1,
                        TextContent = "Dette er en anden test-tekst. Denne tekst er også skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 8,
                        SubModuleID = 2,
                        LanguageID = 2,
                        TextContent = "This is another test text. This text is also written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 9,
                        SubModuleID = 3,
                        LanguageID = 1,
                        TextContent = "Dette er en test-tekst. Denne tekst er skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 10,
                        SubModuleID = 3,
                        LanguageID = 2,
                        TextContent = "This is a test text. This text is written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 11,
                        SubModuleID = 3,
                        LanguageID = 1,
                        TextContent = "Dette er en anden test-tekst. Denne tekst er også skrevet på Dansk, " +
                                      "hvilket betyder at teksten har et sprog id på ##### '1' #####, " +
                                      "og dertil vil kunne referes via dette ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    },
                    new Text
                    {
                        TextID = 12,
                        SubModuleID = 3,
                        LanguageID = 2,
                        TextContent = "This is another test text. This text is also written in English, " +
                                      "which means that the text has a language ID of ##### '2' ######, " +
                                      "and thus can be referred to via this ID.",
                        DateCreated = DateTime.UtcNow,
                        DateModified = DateTime.UtcNow,
                        IsProduction = false,
                        ShouldDisplay = true
                    }
                    );
                #endregion
            });
            #endregion


            // ComponentContents
            #region ComponentContents Migration
            modelBuilder.Entity<ComponentContent>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.ComponentContentID);

                // Foreign Key - assuming there is a Language table that ComponentContent relates to
                entity.HasOne<Language>().WithMany().HasForeignKey(e => e.LanguageID);

                // Properties
                entity.Property(e => e.ComponentContentID).HasColumnName("ComponentContentID").ValueGeneratedOnAdd().UseIdentityAlwaysColumn();
                entity.Property(e => e.LanguageID).HasColumnName("LanguageID");
                entity.Property(e => e.CategoryName).HasColumnName("CategoryName").HasColumnType("text");
                entity.Property(e => e.ComponentClassName).HasColumnName("ComponentClassName").HasColumnType("text");
                entity.Property(e => e.Content).HasColumnName("Content").HasColumnType("text");

                // Seed Data
                #region ComponentContents Seed Data
                entity.HasData(
                #region PageModuleHeading Seed Data
                    new ComponentContent
                    {
                        ComponentContentID = 1,
                        LanguageID = 1,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Moduler"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 2,
                        LanguageID = 2,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Modules"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 3,
                        LanguageID = 1,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Undermoduler"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 4,
                        LanguageID = 2,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Submodules"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 5,
                        LanguageID = 1,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Tekster"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 6,
                        LanguageID = 2,
                        CategoryName = "Title",
                        ComponentClassName = "PageModuleHeading",
                        Content = "Texts"
                    },
                #endregion
                #region PageModuleButton Seed Data
                    new ComponentContent
                    {
                        ComponentContentID = 7,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "PageModuleButton",
                        Content = "Opret Modul"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 8,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "PageModuleButton",
                        Content = "Create Module"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 9,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "PageSubModuleButton",
                        Content = "Opret Undermodul"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 10,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "PageSubModuleButton",
                        Content = "Create Submodule"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 11,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "AddTextButton",
                        Content = "Opret Tekst"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 12,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "AddTextButton",
                        Content = "Create Text"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 13,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "EditTextButton",
                        Content = "Rediger Tekst"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 14,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "EditTextButton",
                        Content = "Edit Text"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 15,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "DeleteTextButton",
                        Content = "Slet Tekst"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 16,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "DeleteTextButton",
                        Content = "Delete Text"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 17,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "AddLanguageButton",
                        Content = "Opret Sprog"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 18,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "AddLanguageButton",
                        Content = "Create Language"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 19,
                        LanguageID = 1,
                        CategoryName = "Button",
                        ComponentClassName = "DeleteLanguageButton",
                        Content = "Slet Sprog"
                    },
                    new ComponentContent
                    {
                        ComponentContentID = 20,
                        LanguageID = 2,
                        CategoryName = "Button",
                        ComponentClassName = "DeleteLanguageButton",
                        Content = "Delete Language"
                    }
                    );
                #endregion
                #endregion
            });

            #endregion
        }
    }
}