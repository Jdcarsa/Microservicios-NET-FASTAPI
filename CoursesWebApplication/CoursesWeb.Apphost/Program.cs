var builder = DistributedApplication.CreateBuilder(args);

var userService = builder.AddProject<Projects.User_Service>("apiservice-user");
var coursesService = builder.AddProject<Projects.Courses_Service>("apiservice-courses");


builder.Build().Run();
