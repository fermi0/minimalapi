This is a practice project to make a minimal API using .NET core.

To run this project, open the project in Visual Studio or VSCode.

Pre-requisites:

1\. NuGet manager

2\. Microsoft.EntityFrameworkCore.InMemory

3\. Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

4\. Postman

From Nuget manager Add those packages to the project, and start the application.

Since this project uses InMemoryDatabase, the datebase is empty in each use. In postman to send a HTTP POST request, go to the body tab and select raw. Since the content-type is set to json, select json from the dropdown menu.

type the following in the field:

{

"title": <"task to to">,

"isComplete":

}

The data is formatted in json, so replace title's value to a title of the todo task you want,and set the boolean of the isComplete to true or false, based on the task is completed or not. You can create as many tasks as you want by editing the title and isComplete.

You can use postman in the entire testing of the API. Or use browser to send HTTP GET request. In browser navigate to /items to send a GET request to see all items in the todo list. To see the completed tasks only navigate to /items/completed. /items/incomplete to see the incomplete tasks. To see a task by the id of the task, navigate to /items/, where is replaced by a number of the item you want to see. Use postman to send a HTTP PUT request to update/edit the task you like , where you can rename a title or set the task to be completed or not. Use postman to delete a task which has been completed or incomplete. You can play with all the HTTP GET methods to request the items of task in the list to see the updated titles or completeness of the task, or see the tasks that you deleted.

To publish the API to another machine for testing purposes and see if the API is production ready, in visual studio right-click on the root of the project i.e api and select "publish", and select "Folder". Browse where to publish the API, and click on finish, then close. Click on "Publish" to now publish as an app. You can now navigate to the folder that the API is deployed, and run it as an exe file. If you want to test it in another machine you can tranfer the published folder to the target machine, and install The .NET Core Hosting Bundle from https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer in target machine. Now run the api.exe to run the program. The program runs on https://localhost:<port\> where port is given in the console.

If you want to use Internet Information Services(IIS) to host the API in the local network, You can install ISS express from https://www.microsoft.com/en-us/download/details.aspx?id=48264, and enable the ISS manager by searching "Turn windows features on or off", and tick the Internet Information Services. When the IIS manager opens, select new site and give the hard path of the published API. Configure the site name, ports, and address of the site then start the server. You can now access the server from another computer and test the API.
