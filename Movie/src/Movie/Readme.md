# ASP.NET Core Web API  Application for games global movie api

### Project Files ###
* aws-lambda-tools-defaults.json - default argument settings for use with IDE and command line deployment tools for AWS
* Program.cs - entry point to the application that contains all of the top level statements initializing the ASP.NET Core application.
The call to `AddAWSLambdaHosting` configures the application to work in Lambda when it detects Lambda is the executing environment. 
* Controllers\MovieController -  Web API controller

## Here are some steps to follow to get started from the command line:

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Deploy application
```
    cd "Movie/src/Movie"
    dotnet lambda deploy-serverless
```

Deploy application on docker
```
   Run docker file 
   app will be avaliable on http://localhost:8080/swagger/index.html
```


