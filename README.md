# PubSub Worker Starter
A .NET Core project template optimized for handling GCP PubSub Events.



> Todo add documentation
## Installation

Install the template
```sh
dotnet new -i pubsubworker
```

Create a new project from the template
```sh
dotnet net pubsubworker -n myworker -o myfolder
```

## Credits
This is a port of [proudmonkey's](https://github.com/proudmonkey)  [ApiBoilerPlate](https://github.com/proudmonkey/ApiBoilerPlate) that focuses on [GCP PubSub](https://cloud.google.com/pubsub/docs/overview) Subscription workers, so you can focus more on the implementation of your Business Logic instead of wiring up PubSub.

## Todo add links

* Dapper (2.0.30)
* Google.Cloud.PubSub.V1 (1.0.0)
* Npgsql (4.1.1)
