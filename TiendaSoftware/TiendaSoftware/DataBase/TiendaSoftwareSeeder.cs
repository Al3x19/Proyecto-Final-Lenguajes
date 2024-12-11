using TiendaSoftware.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using TiendaSoftware.Constants;

namespace TiendaSoftware.DataBase
{
    public class TiendaSoftwareSeeder
    {
        public static async Task LoadDataAsync(
            TiendaSoftwareContext context,
              ILoggerFactory loggerFactory,
            UserManager<UserEntity> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            try
            {
                await LoadPublishersAsync(loggerFactory, context);
                await LoadRolesAndUsersAsync(userManager, roleManager, loggerFactory);
  
                await LoadSoftwaresAsync(loggerFactory, context);
                await LoadUserDownloadsAsync(loggerFactory, context);
                await LoadReviewsAsync(loggerFactory, context);
      
                await LoadSoftwareTagsAsync(loggerFactory, context);
                await LoadSoftwareListsAsync(loggerFactory, context);
            }

            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error inicializando la data del API");
            }
        }

        public static async Task LoadRolesAndUsersAsync(
           UserManager<UserEntity> userManager,
           RoleManager<IdentityRole> roleManager,
           ILoggerFactory loggerFactory
           )
        {
            try
            {
                if (!await roleManager.Roles.AnyAsync())
                {
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.ADMIN));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.PUBLISHER));
                    await roleManager.CreateAsync(new IdentityRole(RolesConstant.USER));
                }
                if (!await userManager.Users.AnyAsync())
                {
                    var userAdmin = new UserEntity
                    {
                        Email = "admin@blogunah.edu",
                        UserName = "admin@blogunah.edu",
                        Securitycode = "123456789"
                    };
                    var PublisherAccount = new UserEntity
                    {
                        Email = "author@blogunah.edu",
                        UserName = "author@blogunah.edu",
                        Securitycode = "123456789"

                    };
                    var normalUser = new UserEntity
                    {
                        Email = "user@blogunah.edu",
                        UserName = "user@blogunah.edu",
                        Securitycode = "123456789"
                    };
                    await userManager.CreateAsync(userAdmin, "Temporal01*");
                    await userManager.CreateAsync(PublisherAccount, "Temporal01*");
                    await userManager.CreateAsync(normalUser, "Temporal01*");
                    await userManager.AddToRoleAsync(userAdmin, RolesConstant.ADMIN);
                    await userManager.AddToRoleAsync(PublisherAccount, RolesConstant.PUBLISHER);
                    await userManager.AddToRoleAsync(normalUser, RolesConstant.USER);
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e.Message);
            }

        }

        public static async Task LoadPublishersAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Publishers.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var publishers = JsonConvert.DeserializeObject<List<PublisherEntity>>(jsonContent);

                if (!await context.Publishers.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();

                    for (int i = 0; i < publishers.Count; i++)
                    {
                        publishers[i].CreatedBy = user.Id;
                        publishers[i].CreatedDate = DateTime.Now;
                        publishers[i].UpdatedBy = user.Id;
                        publishers[i].UpdatedDate = DateTime.Now;
                      
                    }

                    context.AddRange(publishers);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de desarrolladores");
            }
        }
        public static async Task LoadUserDownloadsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/UserDownloads.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var userDownloads = JsonConvert.DeserializeObject<List<UserDownloadsEntity>>(jsonContent);

                if (!await context.UserDownloads.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < userDownloads.Count; i++)
                    {
                        userDownloads[i].CreatedBy = user.Id;
                        userDownloads[i].CreatedDate = DateTime.Now;
                        userDownloads[i].UpdatedBy = user.Id;
                        userDownloads[i].UpdatedDate = DateTime.Now;
                        
                    }

                    context.AddRange(userDownloads);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de userdownloads");
            }
        }

        public static async Task LoadSoftwareTagsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/SoftwareTags.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwareTags = JsonConvert.DeserializeObject<List<SoftwareTagsEntity>>(jsonContent);

                if (!await context.SoftwareTags.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < softwareTags.Count; i++)
                    {
                        softwareTags[i].CreatedBy = user.Id;
                        softwareTags[i].CreatedDate = DateTime.Now;
                        softwareTags[i].UpdatedBy = user.Id;
                        softwareTags[i].UpdatedDate = DateTime.Now;
                      
                    }

                    context.AddRange(softwareTags);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de softwaretagd");
            }
        }

        public static async Task LoadSoftwareListsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/SoftwareLists.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwareLists = JsonConvert.DeserializeObject<List<ListSoftwareEntity>>(jsonContent);

                if (!await context.SoftwareLists.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < softwareLists.Count; i++)
                    {
                        softwareLists[i].CreatedBy = user.Id;
                        softwareLists[i].CreatedDate = DateTime.Now;
                        softwareLists[i].UpdatedBy = user.Id;
                        softwareLists[i].UpdatedDate = DateTime.Now;
                 
                    }

                    context.AddRange(softwareLists);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de softwarelist");
            }
        }

    
        public static async Task LoadSoftwaresAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Softwares.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var softwares = JsonConvert.DeserializeObject<List<SoftwareEntity>>(jsonContent);

                if (!await context.Software.AnyAsync())
                {
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < softwares.Count; i++)
                    {
                        softwares[i].CreatedBy = user.Id;
                        softwares[i].CreatedDate = DateTime.Now;
                        softwares[i].UpdatedBy = user.Id;
                       
                    }

                    context.AddRange(softwares);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de software");
            }
        }

        public static async Task LoadReviewsAsync(ILoggerFactory loggerFactory, TiendaSoftwareContext context)
        {
            try
            {
                var jsonFilePath = "SeedData/Reviews.json";
                var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                var reviews = JsonConvert.DeserializeObject<List<ReviewEntity>>(jsonContent);
                

                if (!await context.Reviews.AnyAsync())
                {
                   
                    var user = await context.Users.FirstOrDefaultAsync();
                    for (int i = 0; i < reviews.Count; i++)
                    {
                        reviews[i].CreatedBy = user.Id;
                        reviews[i].CreatedDate = DateTime.Now;
                        reviews[i].UpdatedBy = user.Id;
                        reviews[i].UpdatedDate = DateTime.Now;
                        
                    }

                    context.AddRange(reviews);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TiendaSoftwareSeeder>();
                logger.LogError(e, "Error al ejecutar el Seed de reviews");
            }
        }

    }
}