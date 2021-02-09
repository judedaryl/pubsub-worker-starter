# Deprecated

This template is no longer maintained. Will be providing new templates soon for NETCore3.1 and .NET5.

# PubSub Worker Starter
A .NET Core project template optimized for handling GCP PubSub Events. So you can focus more on the implementation of your core functionally instead of wiring up PubSub. This is open source, so feel free to contribute!

## Installation

Install the template
```sh
dotnet new -i pubsubworker
```

Create a new project from the template
```sh
dotnet new pubsubworker -n myworker -o myfolder
```

## Notes

Note that the credentials here are stored in the **Credentials** folder. Management of credentials are at the discretion of the user based on the needs and resources available. I advise to use a secure way to manage credentials [Vault by HashiCorp](https://www.vaultproject.io/), or storing these as a jenkins secret parameter.

## Credits
This is a port of [proudmonkey's](https://github.com/proudmonkey)  [ApiBoilerPlate](https://github.com/proudmonkey/ApiBoilerPlate) that focuses on [GCP PubSub](https://cloud.google.com/pubsub/docs/overview) Subscription workers. It uses the same project structure and naming conventions so that you get a consistent development experience all the way.

* Dapper (2.0.30)
* Google.Cloud.PubSub.V1 (1.0.0)
* Npgsql (4.1.1)
