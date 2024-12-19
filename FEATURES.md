# Features

Here are the full feature list of the Flurl.Http.Spec library:
- [x] All features are implemented in latest official release as stable features
- [x] <sup>preview</sup> All features are implemented in latest official release as preview features
- [ ] <sup>vnext|vblue</sup> Outcoming features are planned for next few major releases
- [ ] Backlog features are planned for future releases

## Network Transporting

- Transport
  - [x] HTTP/1.0
  - [x] HTTP/1.1
  - [x] HTTP/2.0 
  - [ ] <sup>vblue</sup> HTTP/3 (QUIC)

- Proxy
  - [ ] <sup>vblue</sup> HTTP Proxy
  - [ ] SOCKS5 Proxy

- Protocol
  - [x] HTML, Web Pages
  - [x] JSON, RESTful Web APIs
  - [x] ODATA, RESTful Web APIs with Query
  - [x] GraphQL, GraphQL Web APIs
  - [ ] gRPC (over HTTP/2)
  - [ ] SOAP

## Specification Format

- Dev Tools Specification
  - .HTTP/.REST file format
    - [x] HTTP Verbs: GET, POST, PUT, PATCH, DELETE
    - [ ] HTTP Verbs: HEAD, OPTIONS, TRACE, CONNECT
    - [x] HTTP Endpoint: Absolute URI with Query Parameters
    - [ ] HTTP Endpoint: Relative Path with Query Parameters (unofficial syntax)
    - [x] HTTP Headers
    - [x] HTTP Request Body
    - [x] Variable Definition and References
    - [x] Inline Comments
    - [x] Request Separators with Tag
  - [ ] cURL request format
  - [ ] Postman collection file (.postman)

- REST API Specification
  - [ ] OpenAPI specification file (.swagger)
  - [ ] API Blueprint specification file (.apib)
  - [ ] RAML specification file (.raml)

- GraphQL API Specification
  - [ ] GraphQL specification file (.gql)

## Execution Context

- User Variables
  - [x] User variable references in HTTP endpoint, headers and body
  - [x] Recursive variable references: nested variables referencing in variable definitions
  - [x] User variables overwriting (external context per execution)

- Environment Variables
  - [ ] <sup>vnext</sup> Environment Variables: System Environment
  - [ ] <sup>vnext</sup> Environment Variables: .env file
  - [ ] <sup>vblue</sup> Secret Variables: User Secrets (settings, local development environment only)
  - [ ] Secret Variables: Docker secrets
  - [ ] Secret Variables: Azure Key Vault

- Command Variables
  - [ ] <sup>vnext</sup> GUID generator
  - [ ] <sup>vnext</sup> Random number generator
  - [ ] <sup>vblue</sup> Date/Time expression

- Workflow Variables
  - [ ] Output Cookie Variables in Api Workflow
  - [ ] Output Json Variables in Api Workflow

- Liquid Expression
  - [ ] Math expression (unoffical)
  - [ ] Conditional and Logical expression (unoffical)

## Authentication and Authorization

- Client Auth
  - [ ] <sup>vnext</sup> API Client Key and Secret 
  - [ ] OAuth2.0: Client Credentials Grant

- User Auth
  - [ ] Basic Auth (in HTTP header or query parameter)
  - [ ] OAuth2.0: Bearer Token (in HTTP header)
  - [ ] OAuth2.0: Authorization Code Grant (in HTTP header)
  - [ ] OpenID Connect: ID Token and Access Token (JWT), Authorization Code Grant in OAuth2.0
  - [ ] OpenID Connect: Implicit Grant in OAuth2.0

## Quality and Engineering System

- Documentation
  - [x] README, LICENSE, CONTRIBUTING, CODE_OF_CONDUCT
  - [x] Release Notes, Change Log, Features Backlog and Roadmap
  - [ ] <sup>vnext</sup> DIRS and Project Structure 
  - [ ] <sup>vnext</sup> Playground and Interactive Demo Tools
  - [ ] Tutorials and Guides
  - [ ] API Reference
  - [ ] Examples and Use Cases

- Github Integration
  - Github Onboarding
    - [x] Permission
    - [x] Boards
    - [x] Issues: Labels, Milestones and Issue Templates
    - [x] PR Template
  - Gitbub Actions: CI/CD, Code Analysis, Code Review, Code Quality, Code Security...
    - [x] CI/Gated Pipeline via Github Workflow
    - [x] CD/Release Pipeline via Github Workflow
    - [x] Labels Synchorization Workflow
    - [ ] Milestone Synchorization Workflow
  - Github Badges
    - [x] Nuget Version and Downloads
    - [x] Build Status
    - [x] Code Coverage and Reports
    - [x] Programming Languages
    - [x] License
    - [ ] Trending of Stars, Forks on Github

- Tests
  - Unit Tests
    - [x] Gatecd Tests
    - [x] Test Coverage
    - [x] Test Report
  - Integration Tests with Project Reference (Pre-Release)
    - [x] SERP Tests for Real World Scenarios
    - [x] HttpBin Tests for Functionality and Compatibility (Remote)
    - [ ] <sup>vblue</sup> HttpBin Tests for Functionality and Compatibility (Docker)
    - [ ] Smoke Tests before Integration Tests (Mock Tests for CI/CD Efficiency)
  - [ ] Integration Tests with Package References (Post-Release)

- Build Cops
  - [x] Roslyn Code Analysis
  - [x] Style Cops Analysis
  - [x] xUnit and Moq Analysis
  - [x] Code Complexity Metrics

- Performance
  - [ ] Benchmarking and Profiling
  - [ ] Hotspot Analysis and Optimization (Slowness)
  - [ ] <sup>vblue</sup> Stability and Reliability (Multithreading)
  - [ ] Load Testing and Scalability (Bottleneck)
  - [x] Fluid Template Caching
  - [ ] Shortcut to plain text if no template variable references
  - [ ] Memory footprint and GC optimization 
