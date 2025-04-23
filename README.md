# CMS Full-Stack Web App (with CMS Kit)

Demo: https://drive.google.com/file/d/1rbrJ-Bvs6y__Klh4Oi0l3Z8ozdX8Lc3s/view?usp=sharing

## Description

This is a simplified Content Management System (CMS) application written with the **ABP Framework**, using **ASP.NET MVC** and **Entity Framework Core** (EF Core).

Note that this project was built on top of [CMS Kit demo]([url](https://github.com/abpframework/cms-kit-demo/)). 
To run the project, follow the same instructions as mentioned in [CMS Kit demo]([url](https://github.com/abpframework/cms-kit-demo/))

## Technical decisions and tradeoffs 

The big decision here was using **CMS Kit**, and particularly their **Pages** and **Menu** system. 
The alternative would have been to implement these features from scratch (e.g. creating the Entity, AppService, DTO, CRUD logic and API endpoints, etc). This was actually started in this project.  
  
The tradeoff is that we have less fine-tuned control over how the page functionality looks and works. In theory, it may be possible to further customize CMS Kit functionality by extending/overriding any public classes and interfaces.  
In practice, I found it challenging to do so when I wanted to code up certain features like Create/Edit/Delete permissions or having the menu bar automatically update based on the existing pages (instead of manually changing that within the app).  

On the other hand, I ended up saving a lot time by not needing to “reinvent the wheel” (which is partly of the goal of the framework) and by being resourceful.
Since the ABP Framework already provides very similar functionality to what I needed, I made use of it.  
  
Another major decision made here was to work off of existing sample code rather than from a newly made startup template (including using **ASP.NET MVC** instead of **Blazor Server**).  
  
The main reason for this was that I had technical problems with being able to use CMS Kit from a fresh template.   
In particular, it seems the ABP CLI would crash or not finish when trying to run the `abp add-module` command.
![image](https://github.com/user-attachments/assets/dbbcba1f-0d02-4abf-b3a0-45b3576eb474)
![image](https://github.com/user-attachments/assets/66c06b3a-b5b9-4c02-b32d-3ec29f81d1ff)

I tried to work around that by manually making the changes that that command was supposed to make (e.g. installing using `dotnet add package`, updating the `.csproj` file, hooking it in the code), but then I'd start getting exceptions with the database, and even after meddling with the migrations, I wasn't able to get it working properly.
So instead, I worked off of the demo code which had CMS Kit already installed. Unfortunately, that restricted me to use ASP.NET MVC for the frontend.
Ideally, I would've just worked off of a startup template with CMS Kit and using Blazor Server. 

Note that this project uses the **Single-Layer Template**, which is intended more for smaller or temporary projects. 
If we wanted this app to be more of a longer-term proper project, using the **Multi-Layer Template** would be better. 

## Tools used

The following tools were used:
- Visual Studio
- ABP Framework and ABP CLI
- ABP Framework documentation + YouTube videos
- ChatGPT

## "Next Steps" section with: 

### What you would improve, refactor, or add 

Ideally, I would add any other features that I didn’t get around to implementing/couldn't figure out how to implement on top of CMS Kit, such as:
- Versioning/entity history
- Having the menu items auto-fill based on the existing pages that are already created
- Fine-tuned permissions for creating/editing/deleting CMS Kit pages
- etc


In terms of what I would improve:
- Not much, as CMS Kit already handles most of the functionality that I wanted and it does so well.
- Perhaps one small thing I would improve is better understanding the relationship between its Pages and Images functionality.  
   - Initially I only wanted to enable Pages and Menus from CMS Kit, but I found that then I wouldn't be able to successfully upload an image as part of a Page's content.


And in terms of refactoring:
- After realizing that not much is exposed for you to extend on top of CMS Kit, I decided to try implementing a simple CMS app without using CMS Kit with the time I had left.
- Essentially, I would consider refactoring the whole project to be more custom made (e.g. having my own Domain Entity, DTO, Application Service, UI etc).

### Thoughts about scalability, caching, modularity, or architectural improvements

#### Scalability
The current project is not that scalable, as even though it supports multi-tenancy and has support for using a distributed cache (which would help with periods of increased usage and usage from multiple tenants), it currently uses in-memory caching as distributed caching was not set up for it (although we could do so with Redis for example).  
  
Also, instead of solely relying on ABP Framework to help with creating and maintaining the database and data, we could also improve scalability by indexing, sharding and replicating the database. Although, we would also need to change the DB as currently it uses SQLite which doesn't support the aforementioned concepts.

#### Caching
Currently, the caching strategy for this project is simple, i.e. in-memory caching.
Ideally, especially for a multi-tenant content-hosting application like a CMS, it would be better to use a distributed cache, using a tool such as Redis. 
This would also help with scalability.

#### Modularity
Since ABP Framework was designed with modularity in mind, there are ways in which this project is already modular.  
For example, we are using the CMS Kit module to power the core functionality of the project.  
One way the project could be even more modular is by separating out the custom project logic from the framework boilerplate into its own plugin/module, and then using that similar to how we used CMS Kit.  

##### Architectural improvements
At the moment, the application is a monolith.   
Transitioning to more of a microservice architecture could be an improvement for all of the above (scalability, caching, modularity). 
Also, containerization of the current project (or microservices), for example, using Docker, could help with stability, maintainability, and scalability as well.

