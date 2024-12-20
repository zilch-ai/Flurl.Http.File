# Flurl.Http.Spec

[![NuGet](https://img.shields.io/nuget/v/Flurl.Http.Spec.svg)](https://nuget.org/packages/Flurl.Http.Spec)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Flurl.Http.Spec.svg)](https://www.nuget.org/packages/Flurl.Http.Spec/)
[![CI](https://github.com/zilch-ai/Flurl.Http.Spec/actions/workflows/dotnet-ci.yml/badge.svg)](https://github.com/zilch-ai/Flurl.Http.Spec/actions/workflows/dotnet-ci.yml)
[![Codecov](https://codecov.io/gh/zilch-ai/Flurl.Http.Spec/branch/main/graph/badge.svg)](https://codecov.io/gh/zilch-ai/Flurl.Http.Spec)
[![Languages](https://img.shields.io/github/languages/top/zilch-ai/Flurl.Http.Spec.svg)](https://github.com/zilch-ai/Flurl.Http.Spec)
[![License](https://img.shields.io/github/license/zilch-ai/Flurl.Http.Spec)](https://github.com/zilch-ai/Flurl.Http.Spec/blob/main/LICENSE)

`Flurl.Http.Spec` is a lightweight C# library for parsing and executing `.http` or `.rest` files with HTTP request definitions. The library is designed to integrate seamlessly with the ecosystem of `Flurl.Http` & web dev tools, providing an easy-to-use programmatic interface.

- [Overview](#overview)
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Overview

`Flurl.Http.Spec` is a lightweight C# library designed for parsing and executing `.http` or `.rest` files that define HTTP requests. It integrates seamlessly with the `Flurl.Http` ecosystem & Web devevelopment tools with authoring `.http` or `.rest` files, providing an easy-to-use programmatic interface for developers to execute HTTP requests directly from these specification files. As part of Zilch.AI projects, LLM Agent will leverage this library to define and execute HTTP requests in a human-readable format, including web search agent and API function calls.

## Features

`Flurl.Http.Spec` provides a set of core features designed to make working with HTTP request definitions simple and efficient.

In the current release, the main features include:
- **Parse `.http` and `.rest` Files**: Easily parse `.http` or `.rest` files containing HTTP request definitions.
- **Flurl.Http Integration**: Seamless integration with the Flurl.Http ecosystem to execute HTTP requests directly.
- **Flexible HTTP Request Definitions**: Define HTTP methods, endpoint, headers & body in flexible syntax.
- **Programmatic Interface**: Interact with parsed requests using a programmatic interface in C#.
- **Asynchronous Request Execution**: Support for async execution of HTTP requests, ensuring non-blocking operations.
  
For a detailed list of features in backlog, please refer to the [FEATURES](FEATURES.md).

And if you want to know more about the latest release and the future release in roadmap, please refer to the [RELEASE](RELEASE.md).

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
var spec = await HttpRequestFile.LoadFromFile(@"requests.http");
var response = await spec.ExecuteAsync();
```

## Directories

Please refer to the [DIRS](DIRS.md) for the directory structure of the repository.

## Contributing

Contributions are welcome! Please see our [CONTRIBUTING](CONTRIBUTING.md) for more details.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.md) file for details.
