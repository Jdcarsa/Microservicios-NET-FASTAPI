var builder = DistributedApplication.CreateBuilder(args);

var userService = builder.AddProject<Projects.User_Service>("apiservice-user");
var coursesService = builder.AddProject<Projects.Courses_Service>("apiservice-courses");

builder.AddProject<Projects.CoursesWebApplication>("frontend")
    .WithReference(coursesService).WithReference(userService);

builder.Build().Run();
