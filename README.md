# aspnet-core-microservice
Project Explanation:
QuizMaker is simple and small application that uses best practices to create microservice. It’s layout in different projects based on the purpose which are explained below.

1.	QuizMaker project is the main application shell.
It should only contain constructor and base code but no business logic. Business Logic should be implemented in the QuizMaker.Services project.
QuizMaker project includes reference to QuizMaker.Service project and QuizMaker.Abstrations.

2.	QuizMaker.Service: Contains business logic. All the services should be constructor injectable.
QuizMaker.Service project includes reference to QuizMaker.DataAccess project and QuizMaker.Abstrations.

3.	QuizMaker.Abstractions: Should contains Business Model classes, Entities, Options etc.

4.	QuizMaker.DataAccess: should contain repositories that connects to Postgres database.  It also uses light weight ORM Dapper to map results set into entities.
QuizMaker.Abstractions project includes reference to QuizMaker.Abstrations.

Connection string is stored into AWS secret manager which is read during start up of the application and available using IOptions options accessor.

5.	Docker. This application is containerized using Docker container.
Open PowerShell and go to root folder (QuizMaker).
Run below docker command to build image.
-f will provide file path to dockerfile.
docker build -f QuizMaker/Dockerfile -t quiz-maker:v1 .
Rub below command to start container.
docker run -it --rm -p 4501:80 quiz-maker:v1

6.	Infrastructure. This folder contains cloud formation template to create RDS instance in AWS.

7.	Database. This folder contains scripts related to database, like creating tables etc.
