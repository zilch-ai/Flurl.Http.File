# Features

Here are the full feature list of the Flurl.Http.Spec library, including all stable/preview features in the latest release, upcoming features in next few releases, and feature items in backlog (as a wish list).
- [x] All features are implemented in latest official release as stable features
- [x] <sup>Preview</sup> All features are implemented in latest official release as preview features
- [ ] <sup>vNext|vBlue</sup> Outcoming features are planned for next few major releases
- [ ] Backlog features are planned for future releases

## Network Transporting

- Transport
  - [x] HTTP/1.0
  - [x] HTTP/1.1
  - [x] HTTP/2.0 
  - [ ] HTTP/3 (QUIC)

- Proxy
  - [ ] HTTP Proxy
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
    - [x] Variable Definition & References
    - [x] Inline Comments
    - [x] Request Separators with Identifier
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
  - [x] User variable references in HTTP endpoint, header values & body
  - [x] Recursive variable references: nested variables referencing in variable definitions
  - [ ] Liquid Expression

- System Variables
  - [ ] Dynamic variables
  - [ ] System environment variables
  - [ ] Date/Time expression
  - [ ] GUID generator
  - [ ] Random number generator

- Secret Variables
  - [ ] Docker secrets
  - [ ] Azure Key Vault
  - [ ] User Secrets (in development environment only)

- Workflow Variables
  - [ ] Output Cookie Variables in Api Workflow
  - [ ] Output Json Variables in Api Workflow

## Authentication & Authorization

- Client Auth
  - [ ] API Key
  - [ ] OAuth 2.0 with Client Certificates

- User Auth
  - [ ] Microsoft Identity
  - [ ] OpenID Connect

## Quality & Engineering System

- Documentation
  - [x] README, LICENSE, CONTRIBUTING, CODE_OF_CONDUCT
  - [x] Release Notes, Change Log, Features Backlog & Roadmap 
  - [ ] Tutorials & Guides
  - [ ] API Reference
  - [ ] Examples & Use Cases
  - [ ] Playgound & Interactive Demo

- Github Integration
  - Github Onboarding
    - [x] Permission
    - [x] Boards
    - [x] Issues: Labels, Milestones & Issue Templates
    - [x] PR Template
  - Gitbub Actions: CI/CD, Code Analysis, Code Review, Code Quality, Code Security...
    - [x] CI/Gated Pipeline via Github Workflow
    - [x] CD/Release Pipeline via Github Workflow
    - [x] Labels Synchorization Workflow
    - [ ] Milestone Synchorization Workflow
  - Github Badges
    - [ ] Build Status
    - [ ] Test Status, Coverage & Reports
    - [ ] Nuget Release Version & Downloads
    - [ ] Stars & Forks Trending

- Tests
  - Unit Tests
    - [x] Gatecd Tests
    - [ ] Test Coverage
    - [ ] Test Report
  - Integration Tests with Project Reference (Pre-Release)
    - [ ] Smoke Tests before Integration Tests (Mock Tests for CI/CD Efficiency)
    - [x] HttpBin Tests for Functionality & Compatibility (Remote)
    - [ ] HttpBin Tests for Functionality & Compatibility (Docker)
    - [ ] SERP Tests for Real World Scenarios
  - [ ] Integration Tests with Package References (Post-Release)

- Build Cops
  - [x] Roslyn Code Analysis
  - [x] Style Cops Analysis
  - [x] xUnit & Moq Analysis
  - [x] Code Complexity Metrics

- Performance
  - [x] Fluid Template Caching
  - [ ] Benchmarking & Profiling
  - [ ] Stability & Reliability (Multithreading)
  - [ ] Hotspot Analysis & Optimization (Slowness)
  - [ ] Load Testing & Scalability (Bottleneck)
