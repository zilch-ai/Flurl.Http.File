# Flurl.Http.Spec

`Flurl.Http.Spec` is a lightweight C# library for parsing and executing `.http` or `.rest` files with HTTP request definitions. The library is designed to integrate seamlessly with the Flurl.Http ecosystem and provides an easy-to-use programmatic interface.

## Features

TODO

## Installation

Flurl.Http.Spec will soon be available via NuGet:

```bash
dotnet add package Flurl.Http.Spec
```

## Usage

Given a .HTTP file below:

```http
### Get User Info
GET https://api.example.com/users/1
Authorization: Bearer {{token}}

### Create a New User
POST https://api.example.com/users
Content-Type: application/json

{
  "name": "John Doe",
  "email": "john.doe@example.com"
}
```

You can load and execute it with the library:

```csharp
var response = await @"requests.http"
    .LoadAsHttpRequestSpec()
    .ExecuteAsync("Get User Info", new { token = "..." });
```

## Roadmap

- v0.1.0
  - [ ] Setup github assets & project framing
  - [ ] Simplest HTTP GET request parsing & execution with code framing
  - [ ] Robust & flexible syntax parser for .HTTP files, supports all HTTP methods, headers, and body

- v0.1.1
  - [ ] Inline comments
  - [ ] Multiple requests combined in a single file accessed by named keys and indexes

- v0.1.2
  - [ ] Predefined inline variables (global) as execution context
  - [ ] Variables overriding in execution context

- v0.1.3
  - [ ] System environment variables as execution context
  - [ ] .env configuration file as execution context
  - [ ] Flatten environment configuration file

More features and improvements are planned. Please see our [BACKLOG.md](BACKLOG.md) for more details.

## Contributing

Contributions are welcome! Please see our [CONTRIBUTING.md](CONTRIBUTING.md) for more details.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.
